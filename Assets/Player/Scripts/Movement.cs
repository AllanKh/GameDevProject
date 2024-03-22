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

    // Start is called before the first frame update
    void Start()
    {
        player_body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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

        if (inputXAxis < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (inputXAxis > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        player_body.velocity = new Vector2(inputXAxis, player_body.velocity.y);
    }

    // Handle jumping
    private void JumpManagement()
    {
        if (Input.GetKeyDown("space") && isOnGround)
        {
            player_body.velocity = new Vector2(player_body.velocity.x, jumpForce);
        }
    }

    // Collision detection 
    void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOnGround = false;
    }

}
