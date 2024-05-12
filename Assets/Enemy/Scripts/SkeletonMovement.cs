using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SkeletonMovement : MonoBehaviour
{
    [SerializeField]private Transform playerTarget;
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

    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
    public bool StartMoving { get { return startMoving; } set { startMoving = value; } }
    public GameObject AttackColliderObject { get { return attackColliderObject; } set { attackColliderObject = value; } }
    public GameObject DetectionColliderObject { get { return detectionColliderObject; } set { detectionColliderObject = value; } }
    public GameObject GroundColliderObject { get { return groundColliderObject; } set { groundColliderObject = value; } }

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Skeleton");
        skeletonManager = GetComponent<SkeletonManager>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        skeletonAnimator = GetComponent<Animator>();
        skeletonAnimator.SetInteger("Anim_State", 0);
        StartCoroutine(WaitBeforeMoving());
        skeletonAttacking = GetComponent<SkeletonAttacking>();
        attackColliderObject = transform.Find("AttackCollider").gameObject;
        detectionColliderObject = transform.Find("DetectionCollider").gameObject;
        groundColliderObject = transform.Find("GroundCollider").gameObject;
        InvokeRepeating("UpdatePath", 0f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        //Update movement for skeleton while alive
        if (startMoving && !skeletonManager.SkeletonIsDead)
        {
            //Move back and fourth if player is not detected
            if (!skeletonManager.SkeletonDetectPlayer)
            {
                moveCounter++;
                Movement();
            }
            gameObjects = GameObject.FindGameObjectsWithTag("Skeleton");
            foreach (GameObject g in gameObjects)
            {
                //Update pathfinding for skeleton if player is detected
                if (g.GetComponent<SkeletonManager>().SkeletonDetectPlayer)
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

                    //Flip sprite and colliders with the direction the skeleton is moving in
                    if (g.GetComponent<SkeletonMovement>().force.x < 0)
                    {
                        g.GetComponent<SpriteRenderer>().flipX = true;
                        FlipAttackCollider(true);
                        FlipGroundCollider(true);
                        FlipHitboxCollider(true);
                    }
                    else if (g.GetComponent<SkeletonMovement>().force.x > 0)
                    {
                        g.GetComponent<SpriteRenderer>().flipX = false;
                        FlipAttackCollider(false);
                        FlipGroundCollider(false);
                        FlipHitboxCollider(false);
                    }
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
        if (rb.velocity.x > 0 && moveCounter >= 1000)
        {
            movementSpeed = -1.5f;
            moveCounter = 0;
        }
        else if (rb.velocity.x < 0 && moveCounter >= 1000)
        {
            movementSpeed = 1.5f;
            moveCounter = 0;
        }

        //Flip asset according to direction
        if (rb.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            FlipAttackCollider(true);
            FlipGroundCollider(true);
            FlipHitboxCollider(true);
        }
        else if (rb.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            FlipAttackCollider(false);
            FlipGroundCollider(false);
            FlipHitboxCollider(false);
        }
    }

    //Check if attack collider flips
    private void FlipAttackCollider(bool flip)
    {
        attackColliderObject.GetComponent<BoxCollider2D>().offset = new Vector2(flip ? -0.3f : 0.3f, 0.16f);
    }
    //Check if ground collider flips
    private void FlipGroundCollider(bool flip)
    {
        groundColliderObject.GetComponent<BoxCollider2D>().offset = new Vector2(flip ? -0.03f : 0.03f, -0.26f);
    }
    //Check if hitbox collider flips
    private void FlipHitboxCollider(bool flip)
    {
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(flip ? -0.03f : 0.03f, 0);
    }
}
