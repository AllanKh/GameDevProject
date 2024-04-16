using System.Collections;
using System.Collections.Generic;
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
            
            Destroy(gameObject);
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



}