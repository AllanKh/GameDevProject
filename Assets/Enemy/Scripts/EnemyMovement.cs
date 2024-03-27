using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1.5f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    //Handle horizontal movement
    private void Movement()
    {
        //Moves enemy
        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);

        //Moves enemy back and forth
        if (rb.position.x >= -1)
        {
            movementSpeed = -1.5f;
        }
        else if (rb.position.x <= -4)
        {
            movementSpeed = 1.5f;
        }

        //Flip asset according to direction
        if (rb.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
