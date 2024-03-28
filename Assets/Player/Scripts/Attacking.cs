using System.Collections;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    private Animator playerAnimator;
    private Collider2D attackCollider;
    private float chargeTimer = 0f; // Add a timer to track the charging time
    private bool isCharging = false; // Track if currently charging
    private float heavyAttackChargeTime = 2.0f;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        attackCollider = transform.Find("AttackCollider").GetComponent<Collider2D>();
        attackCollider.enabled = false;
    }

    void Update()
    {
        AttackManager();

        // Update and check the charge timer if charging
        if (isCharging)
        {
            chargeTimer += Time.deltaTime;
            if (chargeTimer >= heavyAttackChargeTime)
            {
                TriggerHeavyAttack();
            }
        }
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
            isCharging = true;
            PlayerManager.Instance.IsChargingHeavyAttack = true;
            playerAnimator.SetBool("Heavy_Attack_Charge", true);
            chargeTimer = 0f; // Reset the charge attack timer whenever starting the charge
        }

        // Cancel the charge if the button is released before 3 seconds
        if (Input.GetMouseButtonUp(1))
        {
            if (PlayerManager.Instance.IsChargingHeavyAttack)
            {
                isCharging = false;
                PlayerManager.Instance.IsChargingHeavyAttack = false;
                playerAnimator.SetBool("Heavy_Attack_Charge", false);
                chargeTimer = 0f; // Reset the charge attack timer whenever cancelling the charge
            }
        }
    }

    private void TriggerHeavyAttack()
    {
        // Trigger the heavy attack and reset charge-related states
        playerAnimator.SetTrigger("Heavy_Attack_Trigger");
        playerAnimator.SetBool("Heavy_Attack_Charge", false);
        PlayerManager.Instance.IsChargingHeavyAttack = false;
        isCharging = false;
        chargeTimer = 0f;
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
