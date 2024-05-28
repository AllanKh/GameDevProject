using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerColliders : MonoBehaviour
{

    public static event EventHandler OnAnySkeletonAttacked; //An event to track when the player hits any Skeleton
    public static event EventHandler OnAnyFlyingEyeAttacked; //An event to track when the player hits any Flying Eye

    // colliderCound checks mount of colliders currently intersecting with player
    private GameObject[] skeletonGameObjects;
    private GameObject[] flyingEyeGameObjects;
    private GameObject[] detectionColliders;
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

        if (other.CompareTag("DetectionCollider") || other.CompareTag("Potion") || other.CompareTag("BarrelCollider"))
        {
            return;
        }
        
        colliderCount++;
        // Check if AttackCollider is touching a skeleton and register a hit
        skeletonGameObjects = GameObject.FindGameObjectsWithTag("Skeleton");
        foreach (GameObject g in skeletonGameObjects)
        {
            if (gameObject.CompareTag("AttackCollider") && g.GetComponent<Collider2D>() == other)
            {
                OnAnySkeletonAttacked?.Invoke(this, EventArgs.Empty); //Activate this event for all listeners
                g.GetComponent<SkeletonManager>().DamageSkeleton(PlayerManager.Instance.AttackDamage);
            }
        }
        //Check if AttackCollider is touching a flying eye and register a hit
        flyingEyeGameObjects = GameObject.FindGameObjectsWithTag("FlyingEye");
        foreach (GameObject g in flyingEyeGameObjects)
        {
            if (gameObject.CompareTag("AttackCollider") && g.GetComponent<Collider2D>() == other)
            {
                OnAnyFlyingEyeAttacked?.Invoke(this, EventArgs.Empty); //Activate this event for all listeners
                g.GetComponent<FlyingEyeManager>().DamageFlyingEye(PlayerManager.Instance.AttackDamage);
            }
        }

        if (gameObject.CompareTag("AttackCollider") && other.CompareTag("Boss"))
        {
            BossDamageDieHandler.Instance.DamageBoss(PlayerManager.Instance.AttackDamage);
        }

    }


    private void OnTriggerExit2D(Collider2D other)
    {

        //Debug.Log("Exit");
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
