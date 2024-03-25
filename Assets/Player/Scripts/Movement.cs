using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float walkSpeed = 3.0f;
    [SerializeField] float runSpeed = 8.0f;
    [SerializeField] float jumpForce = 8.0f;

    private Rigidbody2D player_body;
    private bool isOnGround = false;
    private PlayerColliders groundCollider;
    private Attacking attacking;
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        player_body = GetComponent<Rigidbody2D>();
        groundCollider = transform.Find("GroundCollider").GetComponent<PlayerColliders>();
        attacking = GetComponent<Attacking>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfOnGround();
        MovementManagement();
        JumpManagement();

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
        }
        else if (inputXAxis > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
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
}
