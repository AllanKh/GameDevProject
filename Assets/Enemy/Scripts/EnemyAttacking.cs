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

    public void StartAttack()
    {
        EnemyColliders enemyColliders = GetComponent<EnemyColliders>();

        if (!EnemyIsAttacking)
        {
            enemyAnimator.SetTrigger("Attack_Trigger");
        }
    }

    public bool EnemyIsAttacking
    {
        get
        {
            animStateInfo = enemyAnimator.GetCurrentAnimatorStateInfo(0);
            return animStateInfo.IsName("Attack_Trigger") && animStateInfo.normalizedTime < 1.0f;
        } 
    }
}
