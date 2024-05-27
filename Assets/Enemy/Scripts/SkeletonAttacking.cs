using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttacking : MonoBehaviour
{
    private Animator skeletonAnimator;
    private AnimatorStateInfo animStateInfo;
    private bool skeletonIsAttacking = false;
    private float cooldown = 2f;
    private float lastAttackAt = -9999f;

    public bool SkeletonIsAttacking { get { return skeletonIsAttacking; } set { skeletonIsAttacking = value; } }

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimator = GetComponent<Animator>();
    }

    //Trigger the attack animation if not already playing
    public void StartAttack()
    {
        if (!AttackAnimationActive)
        {
            //Rate-limits the attack animation
            if (Time.time > lastAttackAt + cooldown)
            {
                skeletonAnimator.SetTrigger("Attack_Trigger");
                lastAttackAt = Time.time;
            }
        }
    }

    //getter to check if skeleton is attacking the player,
    //by checking if the attack animation is playing
    public bool AttackAnimationActive
    {
        get
        {
            animStateInfo = skeletonAnimator.GetCurrentAnimatorStateInfo(0);
            return animStateInfo.IsName("Skeleton_Attack") && animStateInfo.normalizedTime < 1.0f;
        } 
    }
}
