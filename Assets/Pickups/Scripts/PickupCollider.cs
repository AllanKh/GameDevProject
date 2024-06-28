using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupCollider : MonoBehaviour
{
    
    public static event EventHandler OnAnyPotionPickUp; //An event to track when the player picks up any Potion
    public static event EventHandler OnAnyCoinPickUp; //An event to track when the player picks up any Coin

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
            OnAnyPotionPickUp?.Invoke(this, EventArgs.Empty); //Activate this event for all listeners.
            if (PlayerManager.Instance.HeldPotions < 3)
            {
                PlayerManager.Instance.HeldPotions += 1;
            }
            ScoreManager.instance.AddPoint(10);
            DestroyParentGameObject();
            Debug.Log(PlayerManager.Instance.Health);
        }

        if (c2d.CompareTag("Player") && this.CompareTag("Key"))
        {
            // TODO: Add logic for Key Pickup in a player class or a game manager.
            PlayerManager.Instance.HasBossKey = true;
            Debug.Log(PlayerManager.Instance.HasBossKey);
            ScoreManager.instance.AddPoint(45);
            DestroyParentGameObject();
        }

        if (c2d.CompareTag("Player") && this.CompareTag("Coin"))
        {
            OnAnyCoinPickUp?.Invoke(this, EventArgs.Empty); //Activate this event for all listeners.
            ScoreManager.instance.AddPoint(100);
            DestroyParentGameObject();
        }

        if (c2d.CompareTag("Player") && this.CompareTag("ChestKey"))
        {
            PlayerManager.Instance.HasChestKey = true;
            ScoreManager.instance.AddPoint(10);
            DestroyParentGameObject();
        }
    }

    public void DestroyParentGameObject()
    {
        Destroy(transform.parent.gameObject);

    }



}