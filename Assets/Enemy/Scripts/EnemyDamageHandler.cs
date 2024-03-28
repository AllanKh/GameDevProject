using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageHandler : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        CheckIfEnemyDead();
    }

    //Trigger the death animation if on or below 0 health
    private void CheckIfEnemyDead()
    {
        Animator enemyAnimator = GetComponent<Animator>();

        if (EnemyManager.Instance.Health <= 0)
        {
            enemyAnimator.SetBool("IsDead", true);
        }
    }
}
