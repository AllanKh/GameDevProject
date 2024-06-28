using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickups : MonoBehaviour
{

    public List<GameObject> myPrefab;
    bool hasSpawnedItems = false;

    void SpawnObject(int i)
    {
         Instantiate(myPrefab[i],transform.parent.position, Quaternion.identity);
    }

    void SpawnObjectEnemy(int i)
    {
        Instantiate(myPrefab[i], transform.position, Quaternion.identity);
    }

    public void SpawnNow()
    {
        int i = Random.Range(0, myPrefab.Count);
        SpawnObject(i);
        Debug.Log(i);
    }
    public void SpawnNowEnemy()
    {
        int i = Random.Range(0, myPrefab.Count);
        SpawnObjectEnemy(i);
        Debug.Log(i);
        Debug.Log("ENEMY CHECK");
    }
    public void SpawnKeyEvent()
    {
            Instantiate(myPrefab[0],transform.position, Quaternion.identity);
            Debug.Log("Key has been dropped!");
    }

    public void SpawnChestEvent()
    {
        if (hasSpawnedItems == false)
        {
            for (int i = 0; i < 4; i++)
            {
                int j = Random.Range(0, myPrefab.Count);
                Instantiate(myPrefab[j], transform.position, Quaternion.identity);
            }
            hasSpawnedItems = true;
        }
      
    }
}
