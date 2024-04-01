using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    private Animator enemyAnimator;
    private AnimatorStateInfo animStateInfo;
    private Collider2D attackCollider;
    private GameObject attackColliderObject;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        attackCollider = transform.Find("AttackCollider").GetComponent<Collider2D>();
        attackColliderObject = transform.Find("AttackCollider").gameObject;
    }

    //Trigger the attack animation if not already playing
    public void StartAttack()
    {
        if (!EnemyIsAttacking)
        {
            Debug.Log("StartAttack");
            enemyAnimator.SetTrigger("Attack_Trigger");
        }
    }

    //getter to check if enemy is attacking the player,
    //bt checking if the attack animation is playing
    public bool EnemyIsAttacking
    {
        get
        {
            animStateInfo = enemyAnimator.GetCurrentAnimatorStateInfo(0);
            return animStateInfo.IsName("Enemy_Attack") && animStateInfo.normalizedTime < 1.0f;
        } 
    }
}
