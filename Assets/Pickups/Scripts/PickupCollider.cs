using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupCollider : MonoBehaviour

{
    void Awake()
    {
        // Make Collider2D as a trigger
        GetComponent<Collider2D>().isTrigger = true;
    }


    void OnTriggerEnter2D(Collider2D c2d)
    {
        //Destroy Potion if picked up.
        if (c2d.CompareTag("Player") && this.CompareTag("Potion"))
        {
            PlayerManager.Instance.Health += 15;
            Destroy(gameObject);
            Debug.Log(PlayerManager.Instance.Health);
        }

        if (c2d.CompareTag("Player") && this.CompareTag("Key"))
        {

            Destroy(gameObject);
        }

        if (c2d.CompareTag("Player") && this.CompareTag("Coin"))
        {

            Destroy(gameObject);
        }
    }

    void ObjectCollision()
    {
    }



}