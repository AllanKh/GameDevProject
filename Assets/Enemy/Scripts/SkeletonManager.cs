using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonManager : MonoBehaviour
{
    private float health; //Enemy health
    private float attackDamage; //Enemy attack damage
    private bool isDead; //Enemy state
    private bool playerDetected; //Player detecter
    private bool skeletonInvincible; //Invunerable directly after taking a hit

    void Start()
    {
        health = 100.0f;
        attackDamage = 25.0f;
        isDead = false;
        playerDetected = false;
    }

    //get and set skeleton health
    //Skeleton health is within 0 and 100
    public float SkeletonHealth
    {
        get { return health; }
        set { health = Mathf.Clamp(value, 0, 100); }
    }

    //get and set skeleton attack damage
    public float SkeletonAttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }

    //Apply damage to skeleton and reduce skeleton health
    public void DamageSkeleton(float amountOfDamage)
    {
        SkeletonHealth -= amountOfDamage;
    }

    //Check if player detected
    public bool SkeletonDetectPlayer
    {
        get { return playerDetected; }
        set { playerDetected = value; }
    }

    //Check if skeleten is invunerable
    public bool SkeletonInvincible
    {
        get { return skeletonInvincible; }
        set { skeletonInvincible = value; }
    }

    //Check if skeleton is dead
    public bool SkeletonIsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }
}
