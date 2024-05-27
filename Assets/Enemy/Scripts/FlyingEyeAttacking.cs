using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeAttacking : MonoBehaviour
{
    private Animator flyingEyeAnimator;
    private AnimatorStateInfo animStateInfo;
    private bool flyingEyeIsAttacking = false;
    private float cooldown = 1.5f;
    private float lastAttackAt = -9999f;

    public bool FlyingEyeIsAttacking { get { return flyingEyeIsAttacking; } set { flyingEyeIsAttacking = value; } }

    // Start is called before the first frame update
    void Start()
    {
        flyingEyeAnimator = GetComponent<Animator>();
    }

    //Trigger the attack animation if not already playing
    public void StartAttack()
    {
        if (!AttackAnimationActive)
        {
            //Rate-limits the attack animation
            if (Time.time > lastAttackAt + cooldown)
            {
                flyingEyeAnimator.SetTrigger("Attack_Trigger");
                lastAttackAt = Time.time;
            }
        }
    }

    //getter to check if flying eye is attacking the player,
    //by checking if the attack animation is playing
    public bool AttackAnimationActive
    {
        get
        {
            animStateInfo = flyingEyeAnimator.GetCurrentAnimatorStateInfo(0);
            return animStateInfo.IsName("FlyingEye_Attack") && animStateInfo.normalizedTime < 1.0f;
        }
    }
}
