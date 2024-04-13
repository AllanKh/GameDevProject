using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    private Animator enemyAnimator;
    private AnimatorStateInfo animStateInfo;
    private bool enemyIsAttacking = false;
    private Collider2D attackCollider;
    private GameObject attackColliderObject;

    public bool EnemyIsAttacking { get { return enemyIsAttacking; } set { enemyIsAttacking = value; } }

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
        if (!AttackAnimationActive)
        {
            Debug.Log("StartAttack");
            enemyAnimator.SetTrigger("Attack_Trigger");
        }
    }

    //getter to check if enemy is attacking the player,
    //bt checking if the attack animation is playing
    public bool AttackAnimationActive
    {
        get
        {
            animStateInfo = enemyAnimator.GetCurrentAnimatorStateInfo(0);
            return animStateInfo.IsName("Enemy_Attack") && animStateInfo.normalizedTime < 1.0f;
        } 
    }
}
