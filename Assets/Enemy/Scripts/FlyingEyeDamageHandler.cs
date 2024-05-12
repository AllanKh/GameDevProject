using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeDamageHandler : MonoBehaviour
{
    private FlyingEyeManager flyingEyeManager;
    private Animator flyingEyeAnimator;

    // Start is called before the first frame update
    void Start()
    {
        flyingEyeManager = GetComponent<FlyingEyeManager>();
        flyingEyeAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfFlyingEyeDead();
    }

    IEnumerator WaitBeforeDestroy()
    {
        flyingEyeAnimator.SetTrigger("Death_Trigger");
        flyingEyeManager.FlyingEyeIsDead = true;

        yield return new WaitForSeconds(0.9f);

        Destroy(gameObject);
    }

    private void ApplyDamageToPlayer()
    {
        FlyingEyeAttacking flyingEyeAttacking = GetComponent<FlyingEyeAttacking>();

        if (!PlayerManager.Instance.Invincible && flyingEyeAttacking.FlyingEyeIsAttacking)
        {
            PlayerManager.Instance.DamagePlayer(flyingEyeManager.FlyingEyeAttackDamage);
            Debug.Log($"Player health: {PlayerManager.Instance.Health}");
        }
    }

    private void CheckIfFlyingEyeDead()
    {
        if (flyingEyeManager.FlyingEyeHealth <= 0)
        {
            StartCoroutine(WaitBeforeDestroy());
        }
    }
}
