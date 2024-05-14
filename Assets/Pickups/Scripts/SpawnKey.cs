using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKey : MonoBehaviour
{

    GameObject go;
    bool keyHasSpawned;

    private void Awake()
    {
        keyHasSpawned = false;
    }

    private void Update()
    {
        go = GameObject.FindWithTag("Skeleton");
        if (go == null && keyHasSpawned == false)
        {
            GetComponent<SpawnPickups>().SpawnKeyEvent();
            keyHasSpawned = true;
        }
    }









}
