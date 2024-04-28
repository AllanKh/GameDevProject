using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossWalk : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private Animator animator;

    private Transform player;
    private Rigidbody2D rb;
    private bool startMoving = false;
    private bool isFlipped = false;


    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(WaitBeforeMoving());
    }

    private void Update()
    {
        if (startMoving == true)
        {
            Movement();
        }

    }

    //Hold boss idle the first 5 seconds
    IEnumerator WaitBeforeMoving()
    {
        yield return new WaitForSeconds(5);
        startMoving = true;
    }
    
    //Boss moves towars player position
    private void Movement()
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        animator.SetBool("moving", true);
        LookAtPlayer();
    }

    //When bossWalk is no longer enabled the boss stops moving
    private void OnDisable()
    {
        animator.SetBool("moving", false);
    }

    //makes the boss look at the player using the boss scale 
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    
}
