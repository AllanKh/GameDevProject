using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelLogic : MonoBehaviour
{
    public static event EventHandler OnAnyBarrelBreak; //An event to track when any barrel breaks
    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("AttackCollider") && this.CompareTag("BarrelCollider"))
        {
            GetComponent<SpawnPickups>().SpawnNow();
            OnAnyBarrelBreak?.Invoke(this, EventArgs.Empty); //Activate this event for all listeners.
            DestroyParentGameObject();
        }
    }
    public void DestroyParentGameObject()
    {
            Destroy(transform.parent.gameObject);

    }
}
