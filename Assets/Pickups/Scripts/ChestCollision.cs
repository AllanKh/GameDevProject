using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCollision : MonoBehaviour
{
    //public Sprite closedChest;
    public Sprite openChest;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerManager.Instance.HasChestKey == true && collision.CompareTag("AttackCollider") && this.CompareTag("ChestCollider"))
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = openChest;

        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
