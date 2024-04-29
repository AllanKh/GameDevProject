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
            DestroyParentGameObject();
            Debug.Log(PlayerManager.Instance.Health);
        }

        if (c2d.CompareTag("Player") && this.CompareTag("Key"))
        {
            // TODO: Add logic for Key Pickup in a player class or a game manager.
            PlayerManager.Instance.HasBossKey = true;
            Debug.Log(PlayerManager.Instance.HasBossKey);
            DestroyParentGameObject();
        }

        if (c2d.CompareTag("Player") && this.CompareTag("Coin"))
        {

            DestroyParentGameObject();
        }
    }

    public void DestroyParentGameObject()
    {
        Destroy(transform.parent.gameObject);

    }



}