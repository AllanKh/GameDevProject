using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEyeAI : MonoBehaviour
{
    [SerializeField]private Transform playerTarget;
    private GameObject[] gameObjects;
    private GameObject attackColliderObject;
    private GameObject detectionColliderObject;
    private GameObject groundColliderObject;
    private float speed = 200f;
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
    public GameObject DetectionColliderObject { get { return detectionColliderObject; } set { detectionColliderObject = value; } }
    public GameObject GroundColliderObject { get { return groundColliderObject; } set { groundColliderObject = value; } }
    public float Speed { get { return speed; } set { speed = value; } }

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("FlyingEye");
        attackColliderObject = transform.Find("AttackCollider").gameObject;
        detectionColliderObject = transform.Find("DetectionCollider").gameObject;
        groundColliderObject = transform.Find("GroundCollider").gameObject;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("FlyingEye");
        foreach (GameObject g in gameObjects)
        {
            //Update pathfinding for flying eye if player is detected
            if (g.GetComponent<FlyingEyeManager>().FlyingEyeDetectPlayer)
            {
                //Check if the path is not found
                if (g.GetComponent<FlyingEyeAI>().path == null)
                {
                    return;
                }

                //Check if the flying eye has reached the end of the path
                if (g.GetComponent<FlyingEyeAI>().currentWaypoint >= g.GetComponent<FlyingEyeAI>().path.vectorPath.Count)
                {
                    g.GetComponent<FlyingEyeAI>().reachedEndOfPath = true;
                    return;
                }
                else
                {
                    g.GetComponent<FlyingEyeAI>().reachedEndOfPath = false;
                }

                //Apply force to flying eye for movement
                g.GetComponent<FlyingEyeAI>().direction = ((Vector2)g.GetComponent<FlyingEyeAI>().path.vectorPath[g.GetComponent<FlyingEyeAI>().currentWaypoint] - g.GetComponent<Rigidbody2D>().position).normalized;
                g.GetComponent<FlyingEyeAI>().force = g.GetComponent<FlyingEyeAI>().direction * speed * Time.deltaTime;
                g.GetComponent<Rigidbody2D>().AddForce(g.GetComponent<FlyingEyeAI>().force);

                //Stops movement while attacking
                if (g.GetComponent<FlyingEyeAttacking>().FlyingEyeIsAttacking)
                {
                    g.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }

                //Calculate current waypoint on path
                g.GetComponent<FlyingEyeAI>().distance = Vector2.Distance(g.GetComponent<Rigidbody2D>().position, g.GetComponent<FlyingEyeAI>().path.vectorPath[g.GetComponent<FlyingEyeAI>().currentWaypoint]);
                if (g.GetComponent<FlyingEyeAI>().distance < nextWaypointDistance)
                {
                    g.GetComponent<FlyingEyeAI>().currentWaypoint++;
                }

                //Flip sprite and colliders with the direction the flying eye is moving in
                if (g.GetComponent<FlyingEyeAI>().force.x < 0)
                {
                    g.GetComponent<SpriteRenderer>().flipX = true;
                    FlipAttackCollider(true);
                    FlipGroundCollider(true);
                    FlipHitboxCollider(true);
                }
                else if (g.GetComponent<FlyingEyeAI>().force.x > 0)
                {
                    g.GetComponent<SpriteRenderer>().flipX = false;
                    FlipAttackCollider(false);
                    FlipGroundCollider(false);
                    FlipHitboxCollider(false);
                }
            }
        }
    }

    private void UpdatePath()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("FlyingEye");
        foreach (GameObject g in gameObjects)
        {
            //Generates a path if flying eye is still alive and detects the player
            if (!g.GetComponent<FlyingEyeManager>().FlyingEyeIsDead && g.GetComponent<FlyingEyeManager>().FlyingEyeDetectPlayer)
            {
                //Check if current path is done or returned null
                if (g.GetComponent<Seeker>().IsDone())
                {
                    g.GetComponent<Seeker>().StartPath(g.GetComponent<Rigidbody2D>().position, playerTarget.position, OnPathComplete);
                }
            }
            //Stops the current path if flying eye is still alive and no longer detect the player
            else if (!g.GetComponent<FlyingEyeManager>().FlyingEyeIsDead && !g.GetComponent<FlyingEyeManager>().FlyingEyeDetectPlayer)
            {
                g.GetComponent<Seeker>().CancelCurrentPathRequest(true);
                g.GetComponent<FlyingEyeAI>().speed = 0f;
            }
        }
    }

    private void OnPathComplete(Path p)
    {
        gameObjects = GameObject.FindGameObjectsWithTag("FlyingEye");
        foreach (GameObject g in gameObjects)
        {
            //Mark current path as complete if not failed
            if (!p.error)
            {
                g.GetComponent<FlyingEyeAI>().path = p;
                g.GetComponent<FlyingEyeAI>().currentWaypoint = 0;
            }
        }
    }

    //Check if attack collider flips
    private void FlipAttackCollider(bool flip)
    {
        attackColliderObject.GetComponent<BoxCollider2D>().offset = new Vector2(flip ? -0.22f : 0.22f, 0);
    }
    //Check if ground collider flips
    private void FlipGroundCollider(bool flip)
    {
        groundColliderObject.GetComponent<BoxCollider2D>().offset = new Vector2(flip ? -0.05f : 0.05f, -0.2f);
    }
    //Check if hitbox colldider flips
    private void FlipHitboxCollider(bool flip)
    {
        gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(flip ? -0.04f : 0.04f, 0);
    }
}
