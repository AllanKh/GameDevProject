using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickups : MonoBehaviour
{

    public List<GameObject> myPrefab;

    void SpawnObject(int i)
    {
         Instantiate(myPrefab[i],transform.parent.position, Quaternion.identity);
    }

    public void SpawnNow()
    {
        int i = Random.Range(0, myPrefab.Count - 1);
        SpawnObject(i);
    }
}
