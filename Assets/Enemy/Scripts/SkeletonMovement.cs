using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    private SkeletonManager skeletonManager;
    private float movementSpeed = 1.5f;
    private int moveCounter = 0;
    private Rigidbody2D rb;
    private Animator skeletonAnimator;
    private bool startMoving = false;
    private SkeletonAttacking skeletonAttacking;
    private GameObject attackColliderObject;
    private GameObject detectionColliderObject;

    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
    public bool StartMoving { get { return startMoving; } set { startMoving = value; } }
    public GameObject AttackColliderObject { get { return attackColliderObject; } set { attackColliderObject = value; } }
    public GameObject DetectionColliderObject { get { return detectionColliderObject; } set { detectionColliderObject = value; } }

    // Start is called before the first frame update
    void Start()
    {
        skeletonManager = GetComponent<SkeletonManager>();
        rb = GetComponent<Rigidbody2D>();
        skeletonAnimator = GetComponent<Animator>();
        skeletonAnimator.SetInteger("Anim_State", 0);
        StartCoroutine(WaitBeforeMoving());
        skeletonAttacking = GetComponent<SkeletonAttacking>();
        attackColliderObject = transform.Find("AttackCollider").gameObject;
        detectionColliderObject = transform.Find("DetectionCollider").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (startMoving && !skeletonAttacking.AttackAnimationActive && !skeletonManager.SkeletonIsDead)
        {
            moveCounter++;
            Movement();
        }
    }

    //Hold enemy idle the first 5 seconds
    IEnumerator WaitBeforeMoving()
    {
        yield return new WaitForSeconds(5);
        startMoving = true;
    }
    
    //Handle horizontal movement
    private void Movement()
    {
        if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon && startMoving)
        {
            //Start walking animation
            skeletonAnimator.SetInteger("Anim_State", 1);
        }
        else
        {
            //Stops walking animation
            skeletonAnimator.SetInteger("Anim_State", 0);
        }

        //Moves enemy
        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);

        //Moves enemy back and forth
        if (rb.velocity.x > 0 && moveCounter >= 1000 && !skeletonManager.SkeletonDetectPlayer)
        {
            movementSpeed = -1.5f;
            moveCounter = 0;
        }
        else if (rb.velocity.x < 0 && moveCounter >= 1000 && !skeletonManager.SkeletonDetectPlayer)
        {
            movementSpeed = 1.5f;
            moveCounter = 0;
        }

        //Stops moving when attacking
        if (skeletonAttacking.SkeletonIsAttacking)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //Flip asset according to direction
        if (rb.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            FlipAttackCollider(true);
            FlipDetectionCollider(true);
        }
        else if (rb.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            FlipAttackCollider(false);
            FlipDetectionCollider(false);
        }
    }

    //Increase the movementspeed when detecting the player
    public void PlayerDetected()
    {
        movementSpeed = (movementSpeed > 0) ? 2f : -2f;
    }

    //Check if attack collider flips
    private void FlipAttackCollider(bool flip)
    {
        attackColliderObject.transform.localPosition = new Vector2(flip ? -0.6f : 0, 0);
    }

    //Check if detection collider flips
    private void FlipDetectionCollider(bool flip)
    {
        detectionColliderObject.transform.localPosition = new Vector2(flip ? -1.55f : 1.55f, 0);
    }
}
