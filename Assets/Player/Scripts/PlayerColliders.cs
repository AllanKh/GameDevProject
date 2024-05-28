using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliders : MonoBehaviour
{
    public static event EventHandler OnAnySkeletonAttacked; // An event to track when the player hits any Skeleton
    public static event EventHandler OnAnyFlyingEyeAttacked; // An event to track when the player hits any Flying Eye

    // colliderCount checks amount of colliders currently intersecting with player
    private int colliderCount = 0;
    private float disableTimer = 0f;

    // Checks if collision sensor is colliding with anything
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
        if (gameObject.CompareTag("AttackCollider"))
        {
            if (other.CompareTag("Skeleton"))
            {
                OnAnySkeletonAttacked?.Invoke(this, EventArgs.Empty); // Activate this event for all listeners
                other.GetComponent<SkeletonManager>().DamageSkeleton(PlayerManager.Instance.AttackDamage);
            }
            else if (other.CompareTag("FlyingEye"))
            {
                OnAnyFlyingEyeAttacked?.Invoke(this, EventArgs.Empty); // Activate this event for all listeners
                other.GetComponent<FlyingEyeManager>().DamageFlyingEye(PlayerManager.Instance.AttackDamage);
            }
            else if (other.CompareTag("Boss"))
            {
                BossDamageDieHandler.Instance.DamageBoss(PlayerManager.Instance.AttackDamage);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (colliderCount > 0)
        {
            colliderCount--;
        }
    }

    private void Update()
    {
        if (disableTimer > 0)
        {
            disableTimer -= Time.deltaTime;
        }
    }
}
