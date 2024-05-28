using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSkeletonLogic : MonoBehaviour
{
    private bool colliderDetect = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        colliderDetect = GetComponent<CircleCollider2D>().IsTouching(other);    //Will return true if detection collider collides with a certain collider

        //Check if detection collider collides with player
        if (colliderDetect && other.CompareTag("Player"))
        {
            GetComponent<SpawnSkeleton>().SpawnEnemyObject();   //Spawn skeleton
            Destroy(transform.parent.gameObject);               //Destroy spawn object
        }
    }
}
