using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelLogic : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }


    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("AttackCollider") && this.CompareTag("BarrelCollider"))
        {
            GetComponent<SpawnPickups>().SpawnNow();
            DestroyParentGameObject();
        }
    }


    public void DestroyParentGameObject()
    {
            Destroy(transform.parent.gameObject);

    }
}
