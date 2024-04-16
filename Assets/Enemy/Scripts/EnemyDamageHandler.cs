using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageHandler : MonoBehaviour
{

    private Animator enemyAnimator;

    private void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfEnemyDead();
        ApplyDamageToPlayer();
    }

    IEnumerator WaitBeforeDestroy()
    {
        enemyAnimator.SetTrigger("Death_Trigger");
        EnemyManager.Instance.IsDead = true;

        yield return new WaitForSeconds(0.7f);        

        Destroy(gameObject);
    }

    private void ApplyDamageToPlayer()
    {
        EnemyAttacking enemyAttacking = GetComponent<EnemyAttacking>();

        if (enemyAttacking.EnemyIsAttacking && !enemyAttacking.AttackAnimationActive)
        {
            if (!PlayerManager.Instance.Invincible)
            {
                PlayerManager.Instance.DamagePlayer(EnemyManager.Instance.AttackDamage);
                Debug.Log($"Player health: {PlayerManager.Instance.Health}");
            }
        }
    }

    //Trigger the death animation if on or below 0 health
    private void CheckIfEnemyDead()
    {

        if (EnemyManager.Instance.Health <= 0)
        {
            StartCoroutine(WaitBeforeDestroy());
        }
    }
}
