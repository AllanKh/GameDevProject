using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeColliders : MonoBehaviour
{
    private GameObject[] gameObjects;
    private int colliderCount = 0;
    private float disableTimer = 0f;
    private bool attackColliderDetected;
    private bool detectionColliderDetected;
    private bool groundColliderDetected;

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
        gameObjects = GameObject.FindGameObjectsWithTag("FlyingEye");   //Find all active flying eye gameObjects and apply collider logic
        foreach (GameObject g in gameObjects)
        {
            if (!g.GetComponent<FlyingEyeManager>().FlyingEyeIsDead)
            {
                if (g == null)
                {
                    Debug.LogError("No GameObject with tag 'Flying Eye found");
                }
                else
                {
                    FlyingEyeAI flyEyeAI = g.GetComponent<FlyingEyeAI>();
                    if (flyEyeAI == null)
                    {
                        Debug.LogError("FlyingEyeAI component not found on GameObject with tag 'Flying Eye'");
                    }
                }

                FlyingEyeAI flyingEyeAI = g.GetComponent<FlyingEyeAI>();

                if (flyingEyeAI != null && flyingEyeAI.AttackColliderObject != null)
                {
                    Collider2D collider = flyingEyeAI.AttackColliderObject.GetComponent<BoxCollider2D>();
                    if (collider != null)
                    {
                        attackColliderDetected = collider.IsTouching(other);    //Will return true if attack collider collides with a certain collider
                    }
                    else
                    {
                        Debug.LogError("Collider2D component not found on AttackColliderObject");
                    }
                }
                else
                {
                    if (flyingEyeAI == null)
                    {
                        Debug.LogError("flyingEyeAI is null");
                    }
                    if (flyingEyeAI != null && flyingEyeAI.AttackColliderObject == null)
                    {
                        Debug.LogError("AttackColliderObject is null");
                    }
                }

                detectionColliderDetected = flyingEyeAI.DetectionColliderObject.GetComponent<CircleCollider2D>().IsTouching(other);     //Will return true if detection collider collides with a certain collider
                groundColliderDetected = flyingEyeAI.GroundColliderObject.GetComponent<BoxCollider2D>().IsTouching(other);      //Will return true if ground collider collides with a certain collider
                
                //Check if DetectionCollider collides with player
                if (detectionColliderDetected && other.CompareTag("Player"))
                {
                    g.GetComponent<FlyingEyeManager>().FlyingEyeDetectPlayer = true;
                }
                //Check if AttackCollider collides with player and register a hit
                if (attackColliderDetected && other.CompareTag("Player"))
                {
                    g.GetComponent<FlyingEyeAttacking>().FlyingEyeIsAttacking = true;
                    TriggerAttack(g);
                }
                //Check if GroundCollider collides with the ground
                if (groundColliderDetected && other.CompareTag("Ground"))
                {
                    TriggerGroundCollision(g);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        gameObjects = GameObject.FindGameObjectsWithTag("FlyingEye");   //Find all active flying eye gameObjects and apply collider logic
        foreach (GameObject g in gameObjects)
        {
            if (!g.GetComponent<FlyingEyeManager>().FlyingEyeIsDead)
            {
                if (g != null)
                {
                    FlyingEyeAI flyingEyeAI = g.GetComponent<FlyingEyeAI>();

                    if (flyingEyeAI != null)
                    {
                        if (flyingEyeAI.DetectionColliderObject && flyingEyeAI.AttackColliderObject && flyingEyeAI.GroundColliderObject)
                        {
                            detectionColliderDetected = flyingEyeAI.DetectionColliderObject.GetComponent<Collider2D>().IsTouching(other);   //Will return true if detection collider collides with a certain collider
                            attackColliderDetected = flyingEyeAI.AttackColliderObject.GetComponent<Collider2D>().IsTouching(other);     //Will return true if attack collider collides with a certain collider
                        }

                        if (colliderCount > 0)
                        {
                            colliderCount--;
                        }

                        //Check if DetectionCollider no longer collides with player
                        if (!detectionColliderDetected && other.CompareTag("Player"))
                        {
                            g.GetComponent<FlyingEyeManager>().FlyingEyeDetectPlayer = false;
                        }
                        //Check if AttackCollider no longer collides with player
                        if (!attackColliderDetected && other.CompareTag("Player"))
                        {
                            g.GetComponent<FlyingEyeAttacking>().FlyingEyeIsAttacking = false;
                        }
                    }
                    else
                    {
                        Debug.LogError("FlyingEyeAI or FlyingEyeAttacking component not found on the Flying Eye GameObject");
                    }
                }
                else
                {
                    Debug.LogError("No GameObject with tag 'Flying Eye' found");
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
        FlyingEyeAttacking flyingEyeAttacking = g.GetComponent<FlyingEyeAttacking>();
        if (flyingEyeAttacking != null)
        {
            flyingEyeAttacking.StartAttack();
        }
    }
    //Trigger the detection on player
    public void TriggerDetection(GameObject g)
    {
        FlyingEyeAI flyingEyeAI = g.GetComponent<FlyingEyeAI>();
        if (flyingEyeAI.Speed == 0f)
        {
            flyingEyeAI.Speed = 100f;
        }
    }
    //Trigger the detection on ground
    public void TriggerGroundCollision(GameObject g)
    {
        FlyingEyeManager flyingEyeManager = g.GetComponent<FlyingEyeManager>();
        if (flyingEyeManager != null)
        {
            g.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1));
        }
    }
}
