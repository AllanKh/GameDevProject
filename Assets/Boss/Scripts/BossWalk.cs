using System;
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

    //Boss phase 2
    private bool phase2Activated = false;
    private float phase2SpeedMulti = 2f;
    private float phase2Threshold = 300f;

    public static event EventHandler secondPhase;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        StartBoss();
        
    }

    private void Update()
    {
        if (startMoving == true)
        {
            Movement();
        }

        if (!phase2Activated && BossManager.Instance.Health <= phase2Threshold)
        {
            Phase2();
        }

    }

    public void StartBoss()
    {
        StartCoroutine(WaitBeforeMoving());
        Debug.Log("start boss");

    }

    //Hold boss idle the first 5 seconds
    IEnumerator WaitBeforeMoving()
    {
        yield return new WaitForSeconds(5);
        startMoving = true;

        //ADDED BY DENNIS, this activates the HP Bar to show on the screen.

        BossHealthUI.instance.Show();
    }
    
    //Boss moves towars player position
    private void Movement()
    {
        if (player == null)
        {
            Debug.Log("player is null");
            return;
        }

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

    private void Phase2()
    {
        phase2Activated = true;
        speed *= phase2SpeedMulti;
        secondPhase?.Invoke(this, EventArgs.Empty);
    }
    
}
