using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSkeletonLogic : MonoBehaviour
{
    private bool colliderDetect = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        colliderDetect = GetComponent<CircleCollider2D>().IsTouching(other);
        if (colliderDetect && other.CompareTag("Player"))
        {
            GetComponent<SpawnSkeleton>().SpawnEnemyObject();
            Destroy(transform.parent.gameObject);
        }
    }
}
