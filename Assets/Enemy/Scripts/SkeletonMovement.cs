using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SkeletonMovement : MonoBehaviour
{
    private Transform playerTarget;
    private GameObject[] gameObjects;
    private SkeletonManager skeletonManager;
    private float movementSpeed = 1.5f;
    private int moveCounter = 0;
    private Animator skeletonAnimator;
    private bool startMoving = false;
    private SkeletonAttacking skeletonAttacking;
    private GameObject attackColliderObject;
    private GameObject detectionColliderObject;
    private GameObject groundColliderObject;
    private GameObject platformColliderObject;
    private float speed = 30f;
    private float nextWaypointDistance = 3f;
    private float distance;
    private Vector2 direction;
    private Vector2 force;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private Seeker seeker;
    private Rigidbody2D rb;

    public Transform PlayerTarget { get { return playerTarget; } set { playerTarget = value; } }
    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
    public bool StartMoving { get { return startMoving; } set { startMoving = value; } }
    public GameObject AttackColliderObject { get { return attackColliderObject; } set { attackColliderObject = value; } }
    public GameObject DetectionColliderObject { get { return detectionColliderObject; } set { detectionColliderObject = value; } }
    public GameObject GroundColliderObject { get { return groundColliderObject; } set { groundColliderObject = value; } }
    public GameObject PlatformColliderObject { get { return platformColliderObject; } set { platformColliderObject = value; } }

    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        gameObjects = GameObject.FindGameObjectsWithTag("Skeleton");
        skeletonManager = GetComponent<SkeletonManager>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        skeletonAnimator = GetComponent<Animator>();
        skeletonAnimator.SetTrigger("Spawn_Trigger");
        StartCoroutine(WaitForSpawn());
        skeletonAttacking = GetComponent<SkeletonAttacking>();
        attackColliderObject = transform.Find("AttackCollider").gameObject;
        detectionColliderObject = transform.Find("DetectionCollider").gameObject;
        groundColliderObject = transform.Find("GroundCollider").gameObject;
        platformColliderObject = transform.Find("PlatformCollider").gameObject;
        InvokeRepeating("UpdatePath", 0f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        //Update movement for skeleton while alive
        if (!skeletonManager.SkeletonIsDead)
        {
            gameObjects = GameObject.FindGameObjectsWithTag("Skeleton");
            foreach (GameObject g in gameObjects)
            {
                if (Mathf.Abs(g.GetComponent<Rigidbody2D>().velocity.x) > Mathf.Epsilon && !g.GetComponent<SkeletonAttacking>().SkeletonIsAttacking)
                {
                    //Start walking animation
                    g.GetComponent<SkeletonMovement>().skeletonAnimator.SetInteger("Anim_State", 1);
                }
                else
                {
                    //Start idle animation
                    g.GetComponent<SkeletonMovement>().skeletonAnimator.SetInteger("Anim_State", 0);
                }

                //Check if the path is not found
                if (g.GetComponent<SkeletonMovement>().path == null)
                {
                    return;
                }

                //Check if skeleton has reached the end of the path
                if (g.GetComponent<SkeletonMovement>().currentWaypoint >= g.GetComponent<SkeletonMovement>().path.vectorPath.Count)
                {
                    g.GetComponent<SkeletonMovement>().reachedEndOfPath = true;
                    return;
                }
                else
                {
                    g.GetComponent<SkeletonMovement>().reachedEndOfPath = false;
                }

                //Apply force to skeleton for movement
                g.GetComponent<SkeletonMovement>().direction = ((Vector2)g.GetComponent<SkeletonMovement>().path.vectorPath[g.GetComponent<SkeletonMovement>().currentWaypoint] - g.GetComponent<Rigidbody2D>().position).normalized;
                g.GetComponent<SkeletonMovement>().force = g.GetComponent<SkeletonMovement>().direction * speed * Time.deltaTime;
                g.GetComponent<Rigidbody2D>().AddForce(g.GetComponent<SkeletonMovement>().force);

                //Stops movement while attacking
                if (g.GetComponent<SkeletonAttacking>().SkeletonIsAttacking)
                {
                    g.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }

                //Calculate current waypoint on path
                g.GetComponent<SkeletonMovement>().distance = Vector2.Distance(g.GetComponent<Rigidbody2D>().position, g.GetComponent<SkeletonMovement>().path.vectorPath[g.GetComponent<SkeletonMovement>().currentWaypoint]);
                if (g.GetComponent<SkeletonMovement>().distance < nextWaypointDistance)
                {
                    g.GetComponent<SkeletonMovement>().currentWaypoint++;
                }

                //Flip sprite and colliders with skeleton direction
                if (g.GetComponent<SkeletonMovement>().force.x > 0)
                {
                    g.GetComponent<SkeletonMovement>().FlipSkeletonTrue();
                }
                else if (g.GetComponent<SkeletonMovement>().force.x < 0)
                {
                    g.GetComponent<SkeletonMovement>().FlipSkeletonFalse();
                }
            }
        }
    }

    private void UpdatePath()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Skeleton");
        foreach (GameObject g in gameObjects)
        {
            //Generates a path if skeleton is still alive and detects the player
            if (!g.GetComponent<SkeletonManager>().SkeletonIsDead && g.GetComponent<SkeletonManager>().SkeletonDetectPlayer)
            {
                //Check if current path is done or returned null
                if (g.GetComponent<Seeker>().IsDone())
                {
                    g.GetComponent<Seeker>().StartPath(g.GetComponent<Rigidbody2D>().position, playerTarget.position, OnPathComplete);
                }
            }
        }
    }

    private void OnPathComplete(Path p)
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Skeleton");
        foreach (GameObject g in gameObjects)
        {
            //Mark current path as complete if not failed
            if (!p.error)
            {
                g.GetComponent<SkeletonMovement>().path = p;
                g.GetComponent<SkeletonMovement>().currentWaypoint = 0;
            }
        }
    }

    //Hold skeleton idle the first 5 seconds
    IEnumerator WaitBeforeMoving()
    {
        yield return new WaitForSeconds(5);
        startMoving = true;
    }

    IEnumerator WaitForSpawn()
    {
        yield return new WaitForSeconds(1.2f);
        skeletonAnimator.SetInteger("Anim_State", 0);
    }
    
    //Handle movement for skeleton
    private void StandByMovement()
    {
        if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon && startMoving)
        {
            //Start skeleton walk animation
            skeletonAnimator.SetInteger("Anim_State", 1);
        }
        else
        {
            //Stops skeleton walk animation
            skeletonAnimator.SetInteger("Anim_State", 0);
        }

        //Moves skeleton
        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);

        //Moves skeleton back and forth
        if (rb.velocity.x > 0 && moveCounter >= 1000 && !skeletonManager.SkeletonDetectPlatform)
        {
            movementSpeed = -1.5f;
            moveCounter = 0;
        }
        else if (rb.velocity.x < 0 && moveCounter >= 1000 && !skeletonManager.SkeletonDetectPlatform)
        {
            movementSpeed = 1.5f;
            moveCounter = 0;
        }

        //Check if skeleton is about to walk of the platform
        if (skeletonManager.SkeletonDetectPlatform)
        {
            rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
            moveCounter = 0;
        }

        //Flip sprite and colliders with skeleton direction
        if (rb.velocity.x > 0)
        {
            FlipSkeletonTrue();
        }
        else if (rb.velocity.x < 0)
        {
            FlipSkeletonFalse();
        }
    }

    //Check if attack collider flips
    public void FlipAttackCollider(bool flip)
    {
        attackColliderObject.GetComponent<BoxCollider2D>().offset = new Vector2(flip ? 0.3f : -0.3f, 0.16f);
    }
    //Check if ground collider flips
    public void FlipGroundCollider(bool flip)
    {
        groundColliderObject.GetComponent<BoxCollider2D>().offset = new Vector2(flip ? 0.03f : -0.03f, -0.26f);
    }
    //Check if hitbox collider flips
    public void FlipHitboxCollider(bool flip)
    {
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(flip ? 0.03f : -0.03f, 0);
    }
    //Check if platform collider flips
    public void FlipPlatformCollider(bool flip)
    {
        platformColliderObject.GetComponent<BoxCollider2D>().offset = new Vector2(flip ? -0.13f : 0.13f, -0.3f);
    }
    //Flip skeleton returning true
    public void FlipSkeletonTrue()
    {
        GetComponent<SpriteRenderer>().flipX = true;
        FlipAttackCollider(true);
        FlipGroundCollider(true);
        FlipHitboxCollider(true);
        FlipPlatformCollider(true);
    }
    //Flip skeleton returning false
    public void FlipSkeletonFalse()
    {
        GetComponent<SpriteRenderer>().flipX = false;
        FlipAttackCollider(false);
        FlipGroundCollider(false);
        FlipHitboxCollider(false);
        FlipPlatformCollider(false);
    }
}
