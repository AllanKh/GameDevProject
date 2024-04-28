using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEyeAI : MonoBehaviour
{
    [SerializeField]private Transform target;
    private FlyingEyeAttacking flyingEyeAttacking;
    private GameObject[] gameObjects;
    private GameObject attackColliderObject;
    private GameObject detectionColliderObject;
    private float speed = 300f;
    private float nextWaypointDistance = 3f;
    private Path path;
    private int curretnWaypoint = 0;
    private bool reachedEndOfPath = false;
    private Seeker seeker;
    private Rigidbody2D rb;

    public GameObject AttackColliderObject { get { return attackColliderObject; } set { attackColliderObject = value; } }
    public GameObject DetectionColliderObject { get { return detectionColliderObject; } set { detectionColliderObject = value; } }

    // Start is called before the first frame update
    void Start()
    {
        flyingEyeAttacking = GetComponent<FlyingEyeAttacking>();
        gameObjects = GameObject.FindGameObjectsWithTag("FlyingEye");
        attackColliderObject = transform.Find("AttackCollider").gameObject;
        detectionColliderObject = transform.Find("DetectionCollider").gameObject;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null)
        {
            return;
        }

        if (curretnWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[curretnWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        if (flyingEyeAttacking.FlyingEyeIsAttacking)
        {
            speed = 0;
        }
        else if (!flyingEyeAttacking.FlyingEyeIsAttacking)
        {
            speed = 300.0f;
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[curretnWaypoint]);
        if (distance < nextWaypointDistance)
        {
            curretnWaypoint++;
        }

        if (rb.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            FlipAttackCollider(true);
        }
        else if (rb.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            FlipAttackCollider(false);
        }
    }

    private void UpdatePath()
    {
        foreach (GameObject g in gameObjects)
        {
            if (!g.GetComponent<FlyingEyeManager>().FlyingEyeIsDead)
            {
                if (seeker.IsDone())
                {
                    seeker.StartPath(rb.position, target.position, OnPathComplete);
                }
            }
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            curretnWaypoint = 0;
        }
    }

    private void FlipAttackCollider(bool flip)
    {
        attackColliderObject.transform.localPosition = new Vector2(flip ? -0.4f : 0f, 0);
    }
}
