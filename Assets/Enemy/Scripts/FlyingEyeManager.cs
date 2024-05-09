using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeManager : MonoBehaviour
{
    private float health; //Enemy health
    private float attackDamage; //Enemy damage
    private bool isDead; //Enemy state
    private bool playerDetected; //Player detecter
    private bool groundDetected;

    // Start is called before the first frame update
    void Start()
    {
        health = 100.0f;
        attackDamage = 15.0f;
        isDead = false;
        playerDetected = false;
        groundDetected = false;
    }

    //get and set Flying Eye health
    //Flying Eye health is within 0 and 100
    public float FlyingEyeHealth
    {
        get { return health; }
        set { health = Mathf.Clamp(value, 0, 100.0f); }
    }

    //get and set Flying Eye attack damage
    public float FlyingEyeAttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }

    //Apply damage to Flying Eye and reduce health
    public void DamageFlyingEye(float amountOfDamage)
    {
        FlyingEyeHealth -= amountOfDamage;
    }

    //Check if Flying Eye is dead
    public bool FlyingEyeIsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    //Check if Flying Eye detect player
    public bool FlyingEyeDetectPlayer
    {
        get { return playerDetected; }
        set { playerDetected = value; }
    }

    public bool FlyingEyeDetectGround
    {
        get { return groundDetected; }
        set { groundDetected = value; }
    }
}
