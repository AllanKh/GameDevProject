using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonColliders : MonoBehaviour
{
    //Checks the amount of colliders currently intersecting with enemy
    private GameObject[] gameObjects;
    private int colliderCount = 0;
    private float disableTimer = 0f;
    private bool attackColliderDetected;

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

                //Check if AttackCollider collides with player and register a hit
                if (attackColliderDetected && other.CompareTag("Player"))
                {
                    g.GetComponent<SkeletonAttacking>().SkeletonIsAttacking = true;
                    TriggerAttack(g);
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

                    if (skeletonMovement != null)
                    {
                        if (skeletonMovement.AttackColliderObject)
                        {
                            attackColliderDetected = skeletonMovement.AttackColliderObject.GetComponent<Collider2D>().IsTouching(other);
                        }

                        if (colliderCount > 0)
                        {
                            colliderCount--;
                        }

                        //Check if AttackCollider no longer collides with player
                        if (!attackColliderDetected && other.CompareTag("Player"))
                        {
                            g.GetComponent<SkeletonAttacking>().SkeletonIsAttacking = false;
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
