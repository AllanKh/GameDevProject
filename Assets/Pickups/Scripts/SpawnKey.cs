using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKey : MonoBehaviour
{

    GameObject go;
    GameObject go2;
    GameObject go3;
    bool keyHasSpawned;

    private void Awake()
    {
        keyHasSpawned = false;
    }

    private void FixedUpdate()
    {
        go = GameObject.FindWithTag("Skeleton");
        go2 = GameObject.FindWithTag("FlyingEye");
        go3 = GameObject.FindWithTag("SkeletonSpawner");
        if (go == null && keyHasSpawned == false && go2 == null && go3 == null)
        {
            GetComponent<SpawnPickups>().SpawnKeyEvent();
            keyHasSpawned = true;
        }
    }









}
