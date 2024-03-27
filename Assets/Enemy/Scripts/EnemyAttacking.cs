using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    private Animator enemyAnimator;
    private AnimatorStateInfo animStateInfo;
    private Collider2D attackCollider;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        attackCollider = transform.Find("AttackCollider").GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("AttackCollider") && other.CompareTag("Player"))
        {
            enemyAnimator.SetTrigger("Attack_Trigger");
            Debug.Log("Player hit");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
    }

    private void EnemyAttack()
    {
        
    }
}
