using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

    private BoxCollider2D doorCollider;

    void Awake()
    {
        doorCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AttackCollider"))
        {
            if (PlayerManager.Instance.HasBossKey)
            {
                Debug.Log("Player has key");
                OpenDoor();
            }
            else
            {
                Debug.Log("Player does not have key");
            }
        }
    }

    private void OpenDoor()
    {
        // Disable the collider to allow passage
        doorCollider.enabled = false;

        // Optionally, change the sprite or animation to show the door is open
        Debug.Log("Door opened!");
    }


}
