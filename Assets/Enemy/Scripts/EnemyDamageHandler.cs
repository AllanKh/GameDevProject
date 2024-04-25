using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageHandler : MonoBehaviour
{
    private EnemyManager enemyManager;
    private Animator enemyAnimator;

    private void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfEnemyDead();
    }

    //Destroy gameObject after death animation
    IEnumerator WaitBeforeDestroy()
    {
        enemyAnimator.SetTrigger("Death_Trigger");
        enemyManager.Instance.IsDead = true;

        yield return new WaitForSeconds(0.7f);        

        Destroy(gameObject);
    }

    //Animation event
    //Apply damage to player when an attack have succesfully landed
    private void ApplyDamageToPlayer()
    {
        EnemyAttacking enemyAttacking = GetComponent<EnemyAttacking>();

        if (!PlayerManager.Instance.Invincible && enemyAttacking.EnemyIsAttacking)
        {
            PlayerManager.Instance.DamagePlayer(enemyManager.AttackDamage);
            Debug.Log($"Player health: {PlayerManager.Instance.Health}");
        }
    }

    //Trigger the death animation if on or below 0 health
    private void CheckIfEnemyDead()
    {
        if (enemyManager.Health <= 0)
        {
            StartCoroutine(WaitBeforeDestroy());
        }
    }
}
