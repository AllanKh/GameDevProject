using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float walkSpeed = 3.0f;
    [SerializeField] float runSpeed = 10.0f;
    [SerializeField] float jumpForce = 8.0f;

    private Rigidbody2D player_body;
    private bool isOnGround = false;
    private PlayerColliders groundCollider;

    // Start is called before the first frame update
    void Start()
    {
        player_body = GetComponent<Rigidbody2D>();
        groundCollider = transform.Find("GroundCollider").GetComponent<PlayerColliders>();
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
        float movementSpeed = isRunning ? runSpeed : walkSpeed;
        // Check which horizontal movement direction key is pressed
        float inputXAxis = Input.GetAxis("Horizontal") * movementSpeed;

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
