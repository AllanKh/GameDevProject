using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDamageHandler : MonoBehaviour
{
    private SkeletonManager skeletonManager;
    private Animator skeletonAnimator;

    void Start()
    {
        skeletonManager = GetComponent<SkeletonManager>();
        skeletonAnimator = GetComponent<Animator>();
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

        Destroy(gameObject);
    }

    //Animation event
    //Apply damage to player when an attack have succesfully landed
    private void ApplyDamageToPlayer()
    {
        SkeletonAttacking skeletonAttacking = GetComponent<SkeletonAttacking>();

        if (!PlayerManager.Instance.Invincible && skeletonAttacking.SkeletonIsAttacking)
        {
            PlayerManager.Instance.DamagePlayer(skeletonManager.SkeletonAttackDamage);
            Debug.Log($"Player health: {PlayerManager.Instance.Health}");
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
}
