using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSkeleton : MonoBehaviour
{
    public GameObject myPrefab;
    

    public void SpawnEnemyObject()
    {
        Instantiate(myPrefab, transform.parent.position, Quaternion.identity);
        
    }
}
