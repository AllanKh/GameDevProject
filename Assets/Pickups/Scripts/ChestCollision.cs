using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCollision : MonoBehaviour
{
    public Sprite openChest;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerManager.Instance.HasChestKey == true && collision.CompareTag("AttackCollider") && this.CompareTag("ChestCollider"))
        {
            StartCoroutine(WaitBeforeDestroy());
        }
    }


    IEnumerator WaitBeforeDestroy()
    {

        this.gameObject.GetComponent<SpriteRenderer>().sprite = openChest;
        GetComponent<SpawnPickups>().SpawnChestEvent();
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }

}
