using System.Collections;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    private Animator playerAnimator;
    private Collider2D attackCollider;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        attackCollider = transform.Find("AttackCollider").GetComponent<Collider2D>();
        attackCollider.enabled = false;
    }

    void Update()
    {
        AttackManager();
    }

    private void AttackManager()
    {
        if (Input.GetMouseButtonDown(0) && !IsAttacking && !IsHeavyAttacking)
        {
            playerAnimator.SetTrigger("Attack_Trigger");
        }

        // Start charging the heavy attack
        if (Input.GetMouseButtonDown(1) && !IsAttacking && !IsHeavyAttacking && !PlayerManager.Instance.IsChargingHeavyAttack)
        {
            playerAnimator.SetBool("Heavy_Attack_Charge", true);
            PlayerManager.Instance.IsChargingHeavyAttack = true;
            StartCoroutine(HeavyAttackCharge());
        }

        // Release the charge if not charged long enough and cancel if the button is released
        if (Input.GetMouseButtonUp(1))
        {
            if (PlayerManager.Instance.IsChargingHeavyAttack)
            {
                playerAnimator.SetBool("Heavy_Attack_Charge", false);
                PlayerManager.Instance.IsChargingHeavyAttack = false;
            }
        }
    }

    private IEnumerator HeavyAttackCharge()
    {
        yield return new WaitForSeconds(3.0f);

        // ensure player is still charging before triggering the attack
        if (PlayerManager.Instance.IsChargingHeavyAttack)
        {
            playerAnimator.SetTrigger("Heavy_Attack_Trigger");
            playerAnimator.SetBool("Heavy_Attack_Charge", false);
            PlayerManager.Instance.IsChargingHeavyAttack = false;
        }
    }

    public bool IsAttacking
    {
        get
        {
            AnimatorStateInfo animationStateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
            return animationStateInfo.IsName("Attack_Trigger") && animationStateInfo.normalizedTime < 1.0f;
        }
    }

    public bool IsHeavyAttacking
    {
        get
        {
            AnimatorStateInfo animationStateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
            return animationStateInfo.IsName("Heavy_Attack_Trigger") && animationStateInfo.normalizedTime < 1.0f;
        }
    }

    private void EnableAttackCollider()
    {
        attackCollider.enabled = true;
    }

    private void DisableAttackCollider()
    {
        attackCollider.enabled = false;
    }
}
