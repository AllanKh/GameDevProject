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
    private float speed;
    private float nextWaypointDistance = 3f;
    private Path path;
    private int curretnWaypoint = 0;
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
            g.GetComponent<FlyingEyeAI>().speed = Random.Range(80f, 200f);
            if (g.GetComponent<FlyingEyeAI>().path == null)
            {
                return;
            }

            if (g.GetComponent<FlyingEyeAI>().curretnWaypoint >= g.GetComponent<FlyingEyeAI>().path.vectorPath.Count)
            {
                g.GetComponent<FlyingEyeAI>().reachedEndOfPath = true;
                return;
            }
            else
            {
                g.GetComponent<FlyingEyeAI>().reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)g.GetComponent<FlyingEyeAI>().path.vectorPath[g.GetComponent<FlyingEyeAI>().curretnWaypoint] - g.GetComponent<Rigidbody2D>().position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;
            g.GetComponent<Rigidbody2D>().AddForce(force);

            float distance = Vector2.Distance(g.GetComponent<Rigidbody2D>().position, g.GetComponent<FlyingEyeAI>().path.vectorPath[g.GetComponent<FlyingEyeAI>().curretnWaypoint]);
            if (distance < nextWaypointDistance)
            {
                g.GetComponent<FlyingEyeAI>().curretnWaypoint++;
            }
            
            if (force.x < 0)
            {
                g.GetComponent<SpriteRenderer>().flipX = true;
                FlipAttackCollider(true);
                FlipGroundCollider(true);
                FlipHitboxCollider(true);
            }
            else if (force.x > 0)
            {
                g.GetComponent<SpriteRenderer>().flipX = false;
                FlipAttackCollider(false);
                FlipGroundCollider(false);
                FlipHitboxCollider(false);
            }

            if (g.GetComponent<FlyingEyeManager>().FlyingEyeDetectGround)
            {
                g.GetComponent<Rigidbody2D>().AddForce(new Vector2(2, 2), ForceMode2D.Force);
            }
        }
    }

    private void UpdatePath()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("FlyingEye");
        foreach (GameObject g in gameObjects)
        {
            if (!g.GetComponent<FlyingEyeManager>().FlyingEyeIsDead && g.GetComponent<FlyingEyeManager>().FlyingEyeDetectPlayer)
            {
                if (g.GetComponent<Seeker>().IsDone())
                {
                    g.GetComponent<Seeker>().StartPath(g.GetComponent<Rigidbody2D>().position, playerTarget.position, OnPathComplete);
                }
            }
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
            if (!p.error)
            {
                g.GetComponent<FlyingEyeAI>().path = p;
                g.GetComponent<FlyingEyeAI>().curretnWaypoint = 0;
            }
        }
    }

    private void FlipAttackCollider(bool flip)
    {
        //attackColliderObject.transform.localPosition = new Vector2(flip ? -0.51f : -0.8f, 0);
        attackColliderObject.GetComponent<BoxCollider2D>().offset = new Vector2(flip ? -0.22f : 0.22f, 0);
    }

    private void FlipGroundCollider(bool flip)
    {
        groundColliderObject.GetComponent<BoxCollider2D>().offset = new Vector2(flip ? -0.05f : 0.05f, -0.15f);
    }

    private void FlipHitboxCollider(bool flip)
    {
        gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(flip ? -0.04f : 0.04f, 0);
    }
}
