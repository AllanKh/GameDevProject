using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SkeletonMovement : MonoBehaviour
{
    private Transform playerTarget;
    private GameObject[] gameObjects;
    private SkeletonManager skeletonManager;
    private Animator skeletonAnimator;
    private SkeletonAttacking skeletonAttacking;
    private GameObject attackColliderObject;
    private float speed = 0f;
    private float nextWaypointDistance = 3f;
    private float distance;
    private Vector2 direction;
    private Vector2 force;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private Seeker seeker;
    private Rigidbody2D rb;

    public GameObject AttackColliderObject { get { return attackColliderObject; } set { attackColliderObject = value; } }

    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        gameObjects = GameObject.FindGameObjectsWithTag("Skeleton");
        skeletonManager = GetComponent<SkeletonManager>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        skeletonAnimator = GetComponent<Animator>();
        StartCoroutine(WaitForSpawn());
        skeletonAttacking = GetComponent<SkeletonAttacking>();
        attackColliderObject = transform.Find("AttackCollider").gameObject;
        InvokeRepeating("UpdatePath", 0f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        //Update movement for skeleton while alive
        if (!skeletonManager.SkeletonIsDead)
        {
            if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon && !skeletonAttacking.SkeletonIsAttacking)
            {
                //Start walking animation
                skeletonAnimator.SetInteger("Anim_State", 1);
            }
            else
            {
                //Start idle animation
                skeletonAnimator.SetInteger("Anim_State", 0);
            }

            //Check if the path is not found
            if (path == null)
            {
                return;
            }

            //Check if skeleton has reached the end of the path
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            //Apply force to skeleton for movement
            direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            force = direction * speed * Time.deltaTime;
            rb.AddForce(force);

            //Stops movement while attacking
            if (skeletonAttacking.SkeletonIsAttacking)
            {
                rb.velocity = Vector2.zero;
            }

            //Calculate current waypoint on path
            distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            //Flip sprite and colliders with skeleton direction
            if (force.x < 0)
            {
                FlipSkeletonTrue();
            }
            else if (force.x > 0)
            {
                Debug.Log(force.x);
                FlipSkeletonFalse();
            }
        }
    }

    private void UpdatePath()
    {
        if (!skeletonManager.SkeletonIsDead)
        {
            //Check if current path is done or returned null
            if (seeker.IsDone())
            {
                seeker.StartPath(rb.position, playerTarget.position, OnPathComplete);
            }
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    IEnumerator WaitForSpawn()
    {
        yield return new WaitForSeconds(1.2f);
        skeletonAnimator.SetTrigger("Spawn_Trigger");
        speed = 800f;
    }

    //Check if attack collider flips
    public void FlipAttackCollider(bool flip)
    {
        attackColliderObject.GetComponent<BoxCollider2D>().offset = new Vector2(flip ? -0.3f : 0.3f, 0.16f);
    }
    //Check if hitbox collider flips
    public void FlipHitboxCollider(bool flip)
    {
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(flip ? -0.03f : 0.03f, 0);
    }
    //Flip skeleton returning true
    public void FlipSkeletonTrue()
    {
        GetComponent<SpriteRenderer>().flipX = true;
        FlipAttackCollider(true);
        FlipHitboxCollider(true);
    }
    //Flip skeleton returning false
    public void FlipSkeletonFalse()
    {
        GetComponent<SpriteRenderer>().flipX = false;
        FlipAttackCollider(false);
        FlipHitboxCollider(false);
    }
}
