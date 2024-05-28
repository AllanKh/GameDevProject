using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static event EventHandler OnWalking; //An event to know when the player is walking.

    // Walk/Run variables
    private float walkSpeed = 6.0f;
    private float runSpeed = 9.0f;
    private float currentSpeed;
    private float runStamCost = 0.5f;
    private bool facingRight = true;
    private bool isRunning;

    // Jump variables
    private float jumpForce = 12.5f;
    private int maxJumps = 2; // Maximum number of jumps
    private int jumpCount = 0; // Number of jumps performed
    private bool isOnGround = false;

    // Dodge variables
    private float dodgeSpeed = 5.0f;
    private float dodgeLength;
    private float dodgeStamCost = 25.0f;
    private bool isDodging = false;

    // Fall damage variables
    private float lastGroundedYPos;
    private const float minimumFallDistance = 6.0f; // Minimum distance to fall before applying damage
    private const float safeFallDistance = 5.0f; // Fall distance at which player starts taking damage
    private const float damageMultiplier = 5.0f; // Multiplier for fall damage calculation

    private Rigidbody2D player_body;
    private PlayerColliders groundCollider;
    private PlayerColliders wallColliderLeft;
    private PlayerColliders wallColliderRight;
    private GameObject attackColliderObject;
    private PolygonCollider2D playerCollider;
    private Attacking attacking;
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        player_body = GetComponent<Rigidbody2D>();
        lastGroundedYPos = transform.position.y;

        groundCollider = transform.Find("GroundCollider").GetComponent<PlayerColliders>();
        wallColliderLeft = transform.Find("WallCollider_Left").GetComponent<PlayerColliders>();
        wallColliderRight = transform.Find("WallCollider_Right").GetComponent<PlayerColliders>();

        attackColliderObject = transform.Find("AttackCollider").gameObject;
        playerCollider = GetComponent<PolygonCollider2D>();
        attacking = GetComponent<Attacking>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfOnGround();
        if (!isDodging || isOnGround) // Prevent movement and jumping while dodging
        {
            MovementManagement();
            JumpManagement();
        }
        if (!PlayerManager.Instance.Blocking)
        {
            DodgeManagement();
        }

        if (!isOnGround)
        {
            currentSpeed -= 100.5f * Time.deltaTime;
        }

        if (currentSpeed < walkSpeed)
        {
            currentSpeed = walkSpeed;
        }
    }

    private void FixedUpdate()
    {
        if (isRunning)
        {
            PlayerManager.Instance.Stamina -= runStamCost;
        }
        else if (PlayerManager.Instance.Stamina < 100)
        {
            PlayerManager.Instance.Stamina += 0.35f;
        }
    }

    // Handle horizontal movement
    private void MovementManagement()
    {
        if (!PlayerManager.Instance.Blocking)
        {
            isRunning = Input.GetKey(KeyCode.LeftShift);
            currentSpeed = isRunning && !attacking.IsAttacking && PlayerManager.Instance.Stamina > runStamCost ? runSpeed : walkSpeed;
            float inputXAxis = Input.GetAxis("Horizontal") * currentSpeed;

            if (Mathf.Abs(inputXAxis) > Mathf.Epsilon)
            {
                playerAnimator.SetInteger("Anim_State", isRunning ? 2 : 1);
                ActivateFootSteps();
            }
            else
            {
                playerAnimator.SetInteger("Anim_State", 0);
            }

            if (inputXAxis < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                FlipAttackCollider(true);
                if (facingRight)
                {
                    MirrorPolygonCollider();
                }
                facingRight = false;
            }
            else if (inputXAxis > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                FlipAttackCollider(false);
                if (!facingRight)
                {
                    MirrorPolygonCollider();
                }
                facingRight = true;
            }

            if (!PlayerManager.Instance.IsChargingHeavyAttack)
            {
                player_body.velocity = new Vector2(inputXAxis, player_body.velocity.y);
            }
        }
    }

    // Handle jumping
    private void JumpManagement()
    {
        if (Input.GetKeyDown("space") && jumpCount < maxJumps && !PlayerManager.Instance.Blocking)
        {
            playerAnimator.SetTrigger("Player_Jump");
            player_body.velocity = new Vector2(player_body.velocity.x, jumpForce);
            isOnGround = false;
            jumpCount++;
        }
    }

    // Handle dodging
    private void DodgeManagement()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isDodging && isOnGround && !PlayerManager.Instance.IsChargingHeavyAttack && PlayerManager.Instance.Stamina >= dodgeStamCost)
        {
            StartCoroutine(TriggerDodge());
        }
    }

    // Coroutine to handle dodge mechanic
    private IEnumerator TriggerDodge()
    {
        PlayerManager.Instance.Stamina -= dodgeStamCost;
        isDodging = true;
        float dodgeDirection = GetComponent<SpriteRenderer>().flipX ? -1 : 1;

        playerAnimator.SetTrigger("Player_Dodge");
        dodgeLength = playerAnimator.GetCurrentAnimatorStateInfo(0).length;
        PlayerManager.Instance.Invincible = true;
        player_body.velocity = new Vector2((dodgeSpeed * 10.0f) * dodgeDirection, player_body.velocity.y);

        yield return new WaitForSeconds(dodgeLength);

        PlayerManager.Instance.Invincible = false;
        isDodging = false;
    }

    // Checks if player is on the ground or falling
    private void CheckIfOnGround()
    {
        playerAnimator.SetFloat("Velocity_Y", player_body.velocity.y);

        if (!isOnGround && groundCollider.IsEnabledAndColliding())
        {
            if (jumpCount == 0) // Calculate fall damage only if the fall wasn't initiated by a jump
            {
                float fallDistance = lastGroundedYPos - transform.position.y;
                if (fallDistance > minimumFallDistance)
                {
                    ApplyFallDamage(fallDistance);
                }
            }
            isOnGround = true;
            playerAnimator.SetBool("On_Ground", isOnGround);
            jumpCount = 0;  // Reset jump count
            lastGroundedYPos = transform.position.y; // Reset fall distance calculation base
        }
    }

    private void ApplyFallDamage(float fallDistance)
    {
        float damage = (fallDistance - safeFallDistance) * damageMultiplier; // Calculate damage
        if (damage > 0)
        {
            PlayerManager.Instance.DamagePlayer(damage);
        }
    }

    // Method to flip the attack collider when changing directions
    private void FlipAttackCollider(bool flip)
    {
        attackColliderObject.transform.localPosition = new Vector2(flip ? -1 : 1, 1);
    }

    private void MirrorPolygonCollider()
    {
        Vector2[] points = playerCollider.points;
        Vector2[] mirroredPoints = new Vector2[points.Length];

        for (int i = 0; i < points.Length; i++)
        {
            mirroredPoints[i] = new Vector2(-points[i].x, points[i].y);
        }

        playerCollider.points = mirroredPoints;
    }

    public void ActivateFootSteps()
    {
        //Logic to prevent the walking sound to be spammed.

        //OnWalking?.Invoke(this, EventArgs.Empty); //Activate event for all listeners.
    }
}