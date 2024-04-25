using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //Instance of the class to get the properties
    public EnemyManager Instance { get; private set; }

    [SerializeField]private GameObject prefab;
    private float health; //Enemy health
    private float attackDamage; //Enemy attack damage
    private bool isDead;
    private bool playerDetected;

    //private void Awake()
    //{
    //    if (Instance != null && Instance != this)
    //    {
    //        Destroy(gameObject); //Destroys GameObject script it is attatched to if there is a duplicate
    //    }
    //    else
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject); //Prevents EnemyManager from being destroyed when changing scenes
    //    }
    //}

    void Start()
    {
        health = 100.0f;
        attackDamage = 25.0f;
        isDead = false;
        playerDetected = false;
    }

    //get and set enemy health
    //Enemy health is within 0 and 40
    public float Health
    {
        get { return health; }
        set { health = Mathf.Clamp(value, 0, 100); }
    }

    //get and set enemy attack damage
    public float AttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }

    //Apply damage to enemy and reduce enemy health
    public void DamageEnemy(float amountOfDamage)
    {
        Health -= amountOfDamage;
    }

    public bool PlayerDetected
    {
        get { return playerDetected; }
        set { playerDetected = value; }
    }

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }
}
