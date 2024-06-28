using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDamageHandler : MonoBehaviour
{
    public static event EventHandler OnPlayerBlockedAttack; //An event to tell when the player has blocked an attack.

    private SkeletonManager skeletonManager;
    private Animator skeletonAnimator;
    private SkeletonMovement skeletonMovement;

    void Start()
    {
        skeletonManager = GetComponent<SkeletonManager>();
        skeletonAnimator = GetComponent<Animator>();
        skeletonMovement = GetComponent<SkeletonMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfSkeletonDead();
    }

    //Destroy gameObject after death animation
    IEnumerator WaitBeforeDestroy()
    {
        skeletonAnimator.SetTrigger("Death_Trigger");
        skeletonManager.SkeletonIsDead = true;

        yield return new WaitForSeconds(0.7f);
        GetComponent<SpawnPickups>().SpawnNowEnemy();
        ScoreManager.instance.AddPoint(10);
        Destroy(gameObject);
    }

    //Play take hit animation and make skeleton invunerable for a certain amount of time
    IEnumerator SkeletonTakeHit()
    {
        skeletonAnimator.SetTrigger("TakeHit_Trigger");
        skeletonManager.SkeletonInvincible = true;
        skeletonMovement.Rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1.5f);
        skeletonManager.SkeletonInvincible = false;
    }

    //Animation event
    //Apply damage to player when an attack have successfully landed
    private void ApplyDamageToPlayer()
    {
        SkeletonAttacking skeletonAttacking = GetComponent<SkeletonAttacking>();

        if (!PlayerManager.Instance.Invincible && skeletonAttacking.SkeletonIsAttacking)
        {
            PlayerManager.Instance.DamagePlayer(skeletonManager.SkeletonAttackDamage);
            Debug.Log($"Player health: {PlayerManager.Instance.Health}");
        }
        else if (PlayerManager.Instance.Invincible && skeletonAttacking.SkeletonIsAttacking)
        {
            OnPlayerBlockedAttack?.Invoke(this, EventArgs.Empty);
        }
    }

    //Trigger the death animation if on or below 0 health
    private void CheckIfSkeletonDead()
    {
        if (skeletonManager.SkeletonHealth <= 0)
        {
            StartCoroutine(WaitBeforeDestroy());
        }
    }

    //Play take hit and make skeleton invunerable
    public void PlayTakeHit()
    {
        StartCoroutine(SkeletonTakeHit());
    }
}
