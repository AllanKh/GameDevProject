using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliders : MonoBehaviour
{
    //Checks the amount of colliders currently intersecting with enemy
    private int colliderCount = 0;
    private float disableTimer = 0f;

    public bool SensorEnabledAndColliding()
    {
        //Sensor is enabled if disableTimer <= 0
        return disableTimer <= 0 && colliderCount > 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        colliderCount++;
        
        //Check if AttackCollider intersects with the player and register a hit
        if (other.CompareTag("Player"))
        {
            TriggerAttack();
            Debug.Log("Player hit");
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

    public void TriggerAttack()
    {
        if (colliderCount > 0)
        {
            GetComponent<EnemyAttacking>().StartAttack();
        }
    }
}
