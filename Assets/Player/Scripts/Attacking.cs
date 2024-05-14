using System.Collections;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    private Animator playerAnimator;
    private Collider2D attackCollider;
    private float chargeTimer = 0f; // Add a timer to track the charging time
    private bool isCharging = false; // Track if currently charging
    private float heavyAttackChargeTime = 2.0f;
    private float blockStamina = 25.0f;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        attackCollider = transform.Find("AttackCollider").GetComponent<Collider2D>();
        attackCollider.enabled = false;
    }

    void Update()
    {
        BlockManager();
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

    private void BlockManager()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !IsAttacking && !IsHeavyAttacking && !isCharging && PlayerManager.Instance.Stamina > blockStamina)
        {
            playerAnimator.SetBool("Player_Block", true);
            PlayerManager.Instance.Invincible = true;
            PlayerManager.Instance.Blocking = true;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            playerAnimator.SetBool("Player_Block", false);
            PlayerManager.Instance.Invincible = false;
            PlayerManager.Instance.Blocking = false;
        }
    }

    private void AttackManager()
    {

        if (Input.GetMouseButtonDown(0) && !IsAttacking && !IsHeavyAttacking && !PlayerManager.Instance.Blocking)
        {
            playerAnimator.SetTrigger("Attack_Trigger");
            PlayerManager.Instance.AttackDamage = 25;
        }

        // Start charging the heavy attack
        if (Input.GetMouseButtonDown(1) && !IsAttacking && !IsHeavyAttacking && !PlayerManager.Instance.IsChargingHeavyAttack && !PlayerManager.Instance.Blocking)
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
        PlayerManager.Instance.AttackDamage = 100;
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
