using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliders : MonoBehaviour
{
    // colliderCound checks mount of colliders currently intersecting with player
    private int colliderCount = 0;
    private float disableTimer = 0f;

    // Checks if collison sensor is colliding with anything
    public bool IsEnabledAndColliding()
    {
        // if disableTimer <= 0, sensor is enabled
        return disableTimer <= 0 && colliderCount > 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        colliderCount++;

        // Check if AttackCollider is touching an enemy and register a hit
        if (gameObject.CompareTag("AttackCollider") && other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit");
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

}
