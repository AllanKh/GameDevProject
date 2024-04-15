using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageHandler : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        CheckIfEnemyDead();
        ApplyDamageToPlayer();
    }

    IEnumerator WaitBeforeDestroy()
    {
        yield return new WaitForSeconds(3);
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
        Animator enemyAnimator = GetComponent<Animator>();

        if (EnemyManager.Instance.Health <= 0)
        {
            EnemyManager.Instance.IsDead = true;
            enemyAnimator.SetBool("IsDead", true);
            WaitBeforeDestroy();
            Destroy(gameObject);
        }
    }
}
