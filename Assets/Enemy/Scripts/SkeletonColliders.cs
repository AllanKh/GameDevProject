using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonColliders : MonoBehaviour
{
    //Checks the amount of colliders currently intersecting with enemy
    private GameObject[] gameObjects;
    private int colliderCount = 0;
    private float disableTimer = 0f;
    private bool detectionColliderDetected;
    private bool attackColliderDetected;
    private bool groundColliderDetected;
    private bool platformColliderDetected;

    public bool AttackColliderDetected { get; set; }

    public bool SensorEnabledAndColliding()
    {
        //Sensor is enabled if disableTimer <= 0
        return disableTimer <= 0 && colliderCount > 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        colliderCount++;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Skeleton");
        foreach (GameObject g in gameObjects)
        {
            if (!g.GetComponent<SkeletonManager>().SkeletonIsDead)
            {
                if (g == null)
                {
                    Debug.LogError("No GameObject with tag 'Skeleton' found");
                }
                else
                {
                    SkeletonMovement movement = g.GetComponent<SkeletonMovement>();
                    if (movement == null)
                    {
                        Debug.LogError("SkeletonMovement component not found on GameObject with tag 'Skeleton'");
                    }
                }

                SkeletonMovement skeletonMovement = g.GetComponent<SkeletonMovement>();
                SkeletonAttacking skeletonAttacking = g.GetComponent<SkeletonAttacking>();

                if (skeletonMovement != null && skeletonMovement.AttackColliderObject != null)
                {
                    Collider2D collider = skeletonMovement.AttackColliderObject.GetComponent<Collider2D>();
                    if (collider != null)
                    {
                        attackColliderDetected = collider.IsTouching(other);
                    }
                    else
                    {
                        Debug.LogError("Collider2D component not found on AttackColliderObject");
                    }
                }
                else
                {
                    if (skeletonMovement == null)
                    {
                        Debug.LogError("skeletonMovement is null");
                    }
                    if (skeletonMovement != null && skeletonMovement.AttackColliderObject == null)
                    {
                        Debug.LogError("AttackColliderObject is null");
                    }
                }

                detectionColliderDetected = skeletonMovement.DetectionColliderObject.GetComponent<CircleCollider2D>().IsTouching(other);
                groundColliderDetected = skeletonMovement.GroundColliderObject.GetComponent<Collider2D>().IsTouching(other);
                platformColliderDetected = skeletonMovement.PlatformColliderObject.GetComponent<Collider2D>().IsTouching(other);

                //Check if DetectionCollider collides with the player
                if (detectionColliderDetected && other.CompareTag("Player"))
                {
                    g.GetComponent<SkeletonManager>().SkeletonDetectPlayer = true;
                }
                //Check if AttackCollider collides with player and register a hit
                if (attackColliderDetected && other.CompareTag("Player"))
                {
                    g.GetComponent<SkeletonAttacking>().SkeletonIsAttacking = true;
                    TriggerAttack(g);
                }
                //Check if hitbox collides with the ground
                if (groundColliderDetected && other.CompareTag("Ground"))
                {
                    g.GetComponent<Rigidbody2D>().gravityScale = 0;
                }
                //Check if Platform collider collides with the ground
                if (platformColliderDetected && other.CompareTag("PlatformEdge"))
                {
                    g.GetComponent<SkeletonManager>().SkeletonDetectPlatform = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Skeleton");
        foreach (GameObject g in gameObjects)
        {
            if (!g.GetComponent<SkeletonManager>().SkeletonIsDead)
            {
                if (g != null)
                {
                    SkeletonMovement skeletonMovement = g.GetComponent<SkeletonMovement>();
                    SkeletonAttacking skeletonAttacking = g.GetComponent<SkeletonAttacking>();

                    if (skeletonMovement != null && skeletonAttacking != null)
                    {
                        if (skeletonMovement.DetectionColliderObject && skeletonMovement.AttackColliderObject && skeletonMovement.GroundColliderObject && skeletonMovement.PlatformColliderObject)
                        {
                            detectionColliderDetected = skeletonMovement.DetectionColliderObject.GetComponent<CircleCollider2D>().IsTouching(other);
                            attackColliderDetected = skeletonMovement.AttackColliderObject.GetComponent<Collider2D>().IsTouching(other);
                            groundColliderDetected = skeletonMovement.GroundColliderObject.GetComponent<Collider2D>().IsTouching(other);
                            platformColliderDetected = skeletonMovement.PlatformColliderObject.GetComponent<Collider2D>().IsTouching(other);
                        }

                        if (colliderCount > 0)
                        {
                            colliderCount--;
                        }

                        //Check if DetectionCollider no longer collides with player
                        if (!detectionColliderDetected && other.CompareTag("Player"))
                        {
                            g.GetComponent<SkeletonManager>().SkeletonDetectPlayer = false;
                        }
                        //Check if AttackCollider no longer collides with player
                        if (!attackColliderDetected && other.CompareTag("Player"))
                        {
                            g.GetComponent<SkeletonAttacking>().SkeletonIsAttacking = false;
                        }
                        //Check if hitbox no longer collides with the ground
                        if (!groundColliderDetected && other.CompareTag("Ground"))
                        {
                            g.GetComponent<Rigidbody2D>().gravityScale = 1;
                        }
                        //Check if Platform collider no longer collides with the ground
                        if (!platformColliderDetected && other.CompareTag("PlatformEdge"))
                        {
                            g.GetComponent<SkeletonManager>().SkeletonDetectPlatform = false;
                        }
                    }
                    else
                    {
                        Debug.LogError("SkeletonMovement or SkeletonAttacking component not found on the Skeleton GameObject");
                    }
                }
                else
                {
                    Debug.LogError("No GameObject with tag 'Skeleton' found");
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (disableTimer > 0)
        {
            disableTimer -= Time.deltaTime;
        }
    }

    //Trigger the attack on player
    public void TriggerAttack(GameObject g)
    {
        SkeletonAttacking skeletonAttacking = g.GetComponent<SkeletonAttacking>();
        if (skeletonAttacking != null)
        {
            skeletonAttacking.StartAttack();
        }
    }
}
