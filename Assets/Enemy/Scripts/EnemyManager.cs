using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //[SerializeField]private GameObject skeletonPrefab; //Enemy prefabs
    //Skeleton
    private float skeletonHealth; //Enemy health
    private float skeletonAttackDamage; //Enemy attack damage
    private bool skeletonIsDead; //Enemy state
    private bool skeletonDetectPlayer; //Player detecter
    //Flying Eye
    //private float flyingEyeHealth; //Enemy health
    //private float flyingEyeAttackDamage; //Enemy attack damage
    //private bool flyingEyeIsDead; //Enemy state
    //private bool flyingEyeDetectPlayer; //Player detecter

    void Start()
    {
        skeletonHealth = 100.0f;
        skeletonAttackDamage = 25.0f;
        skeletonIsDead = false;
        skeletonDetectPlayer = false;

        //flyingEyeHealth = 100.0f;
        //flyingEyeAttackDamage = 15.0f;
        //flyingEyeIsDead = false;
        //flyingEyeDetectPlayer = false;
    }

    //get and set skeleton health
    //Skeleton health is within 0 and 100
    public float SkeletonHealth
    {
        get { return skeletonHealth; }
        set { skeletonHealth = Mathf.Clamp(value, 0, 100); }
    }

    //get and set skeleton attack damage
    public float SkeletonAttackDamage
    {
        get { return skeletonAttackDamage; }
        set { skeletonAttackDamage = value; }
    }

    //Apply damage to skeleton and reduce skeleton health
    public void DamageSkeleton(float amountOfDamage)
    {
        SkeletonHealth -= amountOfDamage;
    }

    //Check if player detected
    public bool SkeletonDetectPlayer
    {
        get { return skeletonDetectPlayer; }
        set { skeletonDetectPlayer = value; }
    }

    //Check if skeleton is dead
    public bool SkeletonIsDead
    {
        get { return skeletonIsDead; }
        set { skeletonIsDead = value; }
    }

    //get and set flying eye health
    //Flying Eye health is within 0 and 100
    //public float FlyingEyeHealth
    //{
    //    get { return flyingEyeHealth; }
    //    set { flyingEyeHealth = Mathf.Clamp(value, 0, 100); }
    //}

    ////get and set flying eye attack damage
    //public float FlyingEyeAttackDamage
    //{
    //    get { return flyingEyeAttackDamage; }
    //    set { flyingEyeAttackDamage = value; }
    //}

    ////Apply damage to flying eye and reduce flying eye health
    //public void DamageFlyingEye(float amountOfDamage)
    //{
    //    FlyingEyeHealth -= amountOfDamage;
    //}

    ////Check if player detected
    //public bool FlyingEyeDetectPlayer
    //{
    //    get { return flyingEyeDetectPlayer; }
    //    set { flyingEyeDetectPlayer = value; }
    //}

    ////Check if flying eye is dead
    //public bool FlyingEyeIsDead
    //{
    //    get { return flyingEyeIsDead; }
    //    set { flyingEyeIsDead = value; }
    //}
}
