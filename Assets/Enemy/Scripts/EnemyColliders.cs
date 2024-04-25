using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliders : MonoBehaviour
{
    //Checks the amount of colliders currently intersecting with enemy
    private EnemyManager enemyManager;
    private int colliderCount = 0;
    private float disableTimer = 0f;
    private GameObject enemyGameObject;
    private bool detectionColliderDetected;
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
        enemyManager = GetComponent<EnemyManager>();
        if (!enemyManager.Instance.IsDead)
        {
            enemyGameObject = GameObject.FindWithTag("Enemy");

            if (enemyGameObject == null)
            {
                Debug.LogError("No GameObject with tag 'Enemy' found");
            }
            else
            {
                EnemyMovement movement = enemyGameObject.GetComponent<EnemyMovement>();
                if (movement == null)
                {
                    Debug.LogError("EnemyMovement component not found on GameObject with tag 'Enemy'");
                }
            }

            EnemyMovement enemyMovement = enemyGameObject.GetComponent<EnemyMovement>();
            EnemyAttacking enemyAttacking = enemyGameObject.GetComponent<EnemyAttacking>();

            if (enemyMovement != null && enemyMovement.AttackColliderObject != null)
            {
                Collider2D collider = enemyMovement.AttackColliderObject.GetComponent<Collider2D>();
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
                if (enemyMovement == null)
                {
                    Debug.LogError("enemyMovement is null");
                }
                if (enemyMovement != null && enemyMovement.AttackColliderObject == null)
                {
                    Debug.LogError("AttackColliderObject is null");
                }
            }

            detectionColliderDetected = enemyMovement.DetectionColliderObject.GetComponent<Collider2D>().IsTouching(other);

            //Check if DetectionCollider collides with the player
            if (detectionColliderDetected && other.CompareTag("Player"))
            {
                enemyManager.Instance.PlayerDetected = true;
                TriggerDetection();
            }
            //Check if AttackCollider collides with player and register a hit
            if (attackColliderDetected && other.CompareTag("Player"))
            {
                enemyAttacking.EnemyIsAttacking = true;
                TriggerAttack();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        enemyManager = GetComponent<EnemyManager>();
        if (!enemyManager.Instance.IsDead)
        {
            enemyGameObject = GameObject.FindWithTag("Enemy");
            if (enemyGameObject != null)
            {
                EnemyMovement enemyMovement = enemyGameObject.GetComponent<EnemyMovement>();
                EnemyAttacking enemyAttacking = enemyGameObject.GetComponent<EnemyAttacking>();

                if (enemyMovement != null && enemyAttacking != null)
                {
                    if (enemyMovement.DetectionColliderObject && enemyMovement.AttackColliderObject)
                    {
                        detectionColliderDetected = enemyMovement.DetectionColliderObject.GetComponent<Collider2D>().IsTouching(other);
                        attackColliderDetected = enemyMovement.AttackColliderObject.GetComponent<Collider2D>().IsTouching(other);
                    }

                    if (colliderCount > 0)
                    {
                        colliderCount--;
                    }

                    //Check if DetectionCollider no longer collides with player
                    if (!detectionColliderDetected && other.CompareTag("Player"))
                    {
                        enemyManager.Instance.PlayerDetected = false;
                    }
                    //Check if AttackCollider no longer collides with player
                    if (!attackColliderDetected && other.CompareTag("Player"))
                    {
                        enemyAttacking.EnemyIsAttacking = false;
                    }
                }
                else
                {
                    Debug.LogError("EnemyMovement or EnemyAttacking component not found on the Enemy GameObject");
                }
            }
            else
            {
                Debug.LogError("No GameObject with tag 'Enemy' found");
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

    //Trigger the attack
    public void TriggerAttack()
    {
        enemyGameObject = GameObject.FindWithTag("Enemy");
        EnemyAttacking enemyAttacking = enemyGameObject.GetComponent<EnemyAttacking>();
        if (enemyAttacking != null)
        {
            enemyAttacking.StartAttack();
        }
    }
    //Trigger the detection
    public void TriggerDetection()
    {
        enemyGameObject = GameObject.FindWithTag("Enemy");
        EnemyMovement enemyMovement = enemyGameObject.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.PlayerDetected();
        }
    }
}
