using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Walk/Run variables
    private float walkSpeed = 4.0f;
    private float runSpeed = 9.0f;
    private float currentSpeed;
    private float floatSpeed;

    // Jump vairables
    private float jumpForce = 8.0f;
    private bool isOnGround = false;
    private bool hasJumped = false;

    // Dodge variables
    private float dodgeSpeed = 5.0f;
    private float dodgeLength;
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



    // Handle horizontal movement
    private void MovementManagement()
    {
        if (!PlayerManager.Instance.Blocking)
        {
            // Check if player is running
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            // Set movement speed to running speed if player is running
            currentSpeed = isRunning && !attacking.IsAttacking ? runSpeed : walkSpeed;
            // Check which horizontal movement direction key is pressed
            float inputXAxis = Input.GetAxis("Horizontal") * currentSpeed;

            // Checks for slight movement on X axis to start walk animation
            if (Mathf.Abs(inputXAxis) > Mathf.Epsilon)
            {
                // Starts the walking animation
                if (isRunning)
                {
                    playerAnimator.SetInteger("Anim_State", 2);
                }
                else
                {
                    playerAnimator.SetInteger("Anim_State", 1);
                }
            }
            else
            {
                // Stops the walking animation
                playerAnimator.SetInteger("Anim_State", 0);
            }

            // Handle inputs and asset flipping
            if (inputXAxis < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                FlipAttackCollider(true);
            }
            else if (inputXAxis > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                FlipAttackCollider(false);
            }

            // Moves player accordingly if player is not charging a heavy attack
            if (!PlayerManager.Instance.IsChargingHeavyAttack)
            {
                player_body.velocity = new Vector2(inputXAxis, player_body.velocity.y);
            }
        }
    }

    // Handle jumping
    private void JumpManagement()
    {
        if (Input.GetKeyDown("space") && isOnGround && !PlayerManager.Instance.Blocking)
        {
            playerAnimator.SetTrigger("Player_Jump");
            player_body.velocity = new Vector2(currentSpeed * Input.GetAxis("Horizontal"), jumpForce);
            isOnGround = false;
            hasJumped = true;
        }
    }

    // Handle dodging
    private void DodgeManagement()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isDodging && isOnGround && !PlayerManager.Instance.IsChargingHeavyAttack)
        {
            // Starts the coroutine
            StartCoroutine(TriggerDodge());
        }
    }

    // Coroutine to handle handle dodge mechanic
    private IEnumerator TriggerDodge()
    {
        isDodging = true;
        // Determine dodge direction based on where the sprite is facing
        float dodgeDirection = GetComponent<SpriteRenderer>().flipX ? -1 : 1;

        playerAnimator.SetTrigger("Player_Dodge");
        // Sets dodgeLength to length of the dodge animation
        dodgeLength = playerAnimator.GetCurrentAnimatorStateInfo(0).length;
        PlayerManager.Instance.Invincible = true;
        player_body.velocity = new Vector2((dodgeSpeed * 10.0f) * dodgeDirection, player_body.velocity.y);


        // Pausing the coroutine by waiting for duration of dodgeLength before moving to next line
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
            if (!hasJumped) // Calculate fall damage only if the fall wasn't initiated by a jump
            {
                float fallDistance = lastGroundedYPos - transform.position.y;
                if (fallDistance > minimumFallDistance)
                {
                    ApplyFallDamage(fallDistance);
                }
            }
            isOnGround = true;
            playerAnimator.SetBool("On_Ground", isOnGround);
            hasJumped = false;  // Reset jump status
            lastGroundedYPos = transform.position.y; // Reset fall distance calculation base
        }

        if ((!isOnGround && wallColliderLeft.IsEnabledAndColliding()) || (!isOnGround && wallColliderRight.IsEnabledAndColliding()))
        {
            playerAnimator.SetTrigger("Player_Falling");
            isOnGround = false;
            playerAnimator.SetBool("On_Ground", isOnGround);
            player_body.velocity = new Vector2(Input.GetAxis("Horizontal"), -4);
        }

        if (isOnGround && !groundCollider.IsEnabledAndColliding())
        {
            lastGroundedYPos = transform.position.y;
            playerAnimator.SetTrigger("Player_Falling");
            isOnGround = false;
            hasJumped = false;  // Ensure jumped is false when genuinely falling
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
        attackColliderObject.transform.localPosition = new Vector2(flip ? -1 : 1 , 1);
    }
}
