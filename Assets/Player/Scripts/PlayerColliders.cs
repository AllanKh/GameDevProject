using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliders : MonoBehaviour
{
    // colliderCound checks mount of colliders currently intersecting with player
    private GameObject[] skeletonGameObjects;
    private GameObject[] flyingEyeGameObjects;
    private int colliderCount = 0;
    private float disableTimer = 0f;

    // Checks if collison sensor is colliding with anything
    public bool IsEnabledAndColliding()
    {
        // if disableTimer <= 0, sensor is enabled
        return disableTimer <= 0 && colliderCount > 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        colliderCount++;
        // Check if AttackCollider is touching a skeleton and register a hit
        skeletonGameObjects = GameObject.FindGameObjectsWithTag("Skeleton");
        foreach (GameObject g in skeletonGameObjects)
        {
            if (gameObject.CompareTag("AttackCollider") && g.GetComponent<Collider2D>() == other)
            {
                Debug.Log("Skeleton Hit");
                g.GetComponent<SkeletonManager>().DamageSkeleton(PlayerManager.Instance.AttackDamage);
            }
        }
        //Check if AttackCollider is touching a flying eye and register a hit
        flyingEyeGameObjects = GameObject.FindGameObjectsWithTag("FlyingEye");
        foreach (GameObject g in flyingEyeGameObjects)
        {
            if (gameObject.CompareTag("AttackCollider") && g.GetComponent<Collider2D>() == other)
            {
                Debug.Log("Flying Eye Hit");
                g.GetComponent<FlyingEyeManager>().DamageFlyingEye(PlayerManager.Instance.AttackDamage);
            }
        }

        if (gameObject.CompareTag("AttackCollider") && other.CompareTag("Boss"))
        {
            BossDamageDieHandler.Instance.DamageBoss(PlayerManager.Instance.AttackDamage);
            Debug.Log(BossManager.Instance.Health);
        }

    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (colliderCount > 0)
        {
            colliderCount--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (disableTimer > 0)
        {
            disableTimer -= Time.deltaTime;
        }
    }

}
