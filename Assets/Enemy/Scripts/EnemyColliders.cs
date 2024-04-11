using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliders : MonoBehaviour
{
    //Checks the amount of colliders currently intersecting with enemy
    private int colliderCount = 0;
    private float disableTimer = 0f;
    private GameObject enemyGameObject;

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
        enemyGameObject = GameObject.FindWithTag("Enemy");
        EnemyMovement enemyMovement = enemyGameObject.GetComponent<EnemyMovement>();
        bool attackColliderDetected = enemyMovement.AttackColliderObject.GetComponent<Collider2D>().IsTouching(other);
        bool detectionColliderDetected = enemyMovement.DetectionColliderObject.GetComponent<Collider2D>().IsTouching(other);
        //Check if any collider collides with the player
        if (other.CompareTag("Player"))
        {
            EnemyManager.Instance.PlayerDetected = true;
            TriggerDetection();
            Debug.Log("Player detected");
        }
        //Check if AttackCollider collides with player and register a hit
        if (attackColliderDetected && other.CompareTag("Player"))
        {
            TriggerAttack();
            Debug.Log("Player hit");
        }
        //Check if any collider no longer collides with player
        if (!other.CompareTag("Player"))
        {
            EnemyManager.Instance.PlayerDetected = false;
            Debug.Log("Player no longer detected");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (colliderCount > 0)
        {
            colliderCount--;
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
            Debug.Log("Trigger");
            enemyAttacking.StartAttack();
        }
    }

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
