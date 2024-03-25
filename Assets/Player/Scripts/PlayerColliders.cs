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
