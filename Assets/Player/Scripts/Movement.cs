using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Walk/Run variables
    [SerializeField] float walkSpeed = 3.0f;
    [SerializeField] float runSpeed = 8.0f;

    // Jump vairables
    [SerializeField] float jumpForce = 7.0f;
    private bool isOnGround = false;

    // Dodge variables
    [SerializeField] float dodgeSpeed = 5.0f;
    private float dodgeLength;
    private bool isDodging = false;

    private Rigidbody2D player_body;
    private PlayerColliders groundCollider;
    private GameObject attackColliderObject;
    private Attacking attacking;
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        player_body = GetComponent<Rigidbody2D>();
        groundCollider = transform.Find("GroundCollider").GetComponent<PlayerColliders>();
        attackColliderObject = transform.Find("AttackCollider").gameObject;
        attacking = GetComponent<Attacking>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfOnGround();
        if (!isDodging) // Prevent movement and jumping while dodging
        {
            MovementManagement();
            JumpManagement();
        }
        DodgeManagement();

    }

    // Handle horizontal movement
    private void MovementManagement()
    {
        // Check if player is running
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        // Set movement speed to running speed if player is running
        float movementSpeed = isRunning && isOnGround && !attacking.IsAttacking ? runSpeed : walkSpeed;
        // Check which horizontal movement direction key is pressed
        float inputXAxis = Input.GetAxis("Horizontal") * movementSpeed;

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

        // Moves player accordingly
        player_body.velocity = new Vector2(inputXAxis, player_body.velocity.y);
    }

    // Handle jumping
    private void JumpManagement()
    {
        if (Input.GetKeyDown("space") && isOnGround)
        {
            player_body.velocity = new Vector2(player_body.velocity.x, jumpForce);
            isOnGround = false;
        }
    }

    // Handle dodging
    private void DodgeManagement()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isDodging && isOnGround)
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
        player_body.velocity = new Vector2(dodgeSpeed * dodgeDirection, player_body.velocity.y);


        // Pausing the coroutine by waiting for duration of dodgeLength before moving to next line
        yield return new WaitForSeconds(dodgeLength);

        PlayerManager.Instance.Invincible = false;
        isDodging = false;
    }

    // Checks if player is on the ground or falling
    private void CheckIfOnGround()
    {
        if (!isOnGround && groundCollider.IsEnabledAndColliding())
        {
            isOnGround = true;
        }

        if (isOnGround && !groundCollider.IsEnabledAndColliding())
        {
            isOnGround = false;
        }

    }

    // Method to flip the attack collider when changing directions
    private void FlipAttackCollider(bool flip)
    {
        attackColliderObject.transform.localPosition = new Vector2(flip ? -1 : 1 , 1);
    }
}
