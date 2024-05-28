using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeDamageHandler : MonoBehaviour
{
    private FlyingEyeManager flyingEyeManager;
    private Animator flyingEyeAnimator;
    private FlyingEyeAI flyingEyeAI;


    public static event EventHandler PlayerBlockedEyeAttack;

    // Start is called before the first frame update
    void Start()
    {
        flyingEyeManager = GetComponent<FlyingEyeManager>();
        flyingEyeAnimator = GetComponent<Animator>();
        flyingEyeAI = GetComponent<FlyingEyeAI>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfFlyingEyeDead();
    }

    //Destroy gameObject after death animation
    IEnumerator WaitBeforeDestroy()
    {
        flyingEyeAnimator.SetTrigger("Death_Trigger");
        flyingEyeManager.FlyingEyeIsDead = true;
        yield return new WaitForSeconds(0.51f);

        Destroy(gameObject);
    }

    //Play take hit animation and make flying eye invunerable for a certain amount of time
    IEnumerator FlyingEyeTakeHit()
    {
        flyingEyeAnimator.SetTrigger("TakeHit_Trigger");
        flyingEyeManager.FlyingEyeInvincible = true;
        flyingEyeAI.Rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1.4f);
        flyingEyeManager.FlyingEyeInvincible = false;
    }

    //Animation event
    //Apply damage to player when an attack have successfully landed 
    private void ApplyDamageToPlayer()
    {
        FlyingEyeAttacking flyingEyeAttacking = GetComponent<FlyingEyeAttacking>();

        if (!PlayerManager.Instance.Invincible && flyingEyeAttacking.FlyingEyeIsAttacking)
        {
            PlayerManager.Instance.DamagePlayer(flyingEyeManager.FlyingEyeAttackDamage);
            Debug.Log($"Player health: {PlayerManager.Instance.Health}");
        }
        else if (PlayerManager.Instance.Invincible && flyingEyeAttacking.FlyingEyeIsAttacking)
        {
            PlayerBlockedEyeAttack?.Invoke(this, EventArgs.Empty);
        }
    }

    //Trigger the death animation if on or below 0 health
    private void CheckIfFlyingEyeDead()
    {
        if (flyingEyeManager.FlyingEyeHealth <= 0)
        {
            StartCoroutine(WaitBeforeDestroy());
        }
    }

    //Play take hit and make flying eye invunerable
    public void PlayTakeHit()
    {
        StartCoroutine(FlyingEyeTakeHit());
    }
}
