using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    private Animator playerAnimator;
    private AnimatorStateInfo animationStateInfo;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        // Set the speed of the attack animation
    }

    // Update is called once per frame
    void Update()
    {
        AttackManager();
    }

    private void AttackManager()
    {   
        if (Input.GetMouseButtonDown(0) && !IsAttacking)
        {
            playerAnimator.SetTrigger("Attack_Trigger");
        }
    }

    // Read-Only property to check if the player is attacking 
    // by checking if the attack animation is playing
    public bool IsAttacking
    {
        get
        {
            animationStateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
            return animationStateInfo.IsName("Attack_Trigger") && animationStateInfo.normalizedTime < 1.0f;
        }
    }
}
