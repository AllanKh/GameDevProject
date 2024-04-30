using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

    private BoxCollider2D doorCollider;
    public Sprite openDoorSprite;
    private SpriteRenderer spriteRenderer;
    private BossWalk bossWalk;

    void Awake()
    {
        doorCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //bossWalk = GetComponent<BossWalk>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AttackCollider"))
        {
            if (PlayerManager.Instance.HasBossKey)
            {
                Debug.Log("Player has key");
                OpenDoor();
                //StartCoroutine(bossWalk.WaitBeforeMoving());
            }
            else
            {
                Debug.Log("Player does not have key");
            }
        }
    }

    private void OpenDoor()
    {
        doorCollider.enabled = false;
        spriteRenderer.sprite = openDoorSprite;
    }


}
