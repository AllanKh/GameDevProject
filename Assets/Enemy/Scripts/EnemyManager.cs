using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //Singleton to manage enemy stats globally across the game
    //Ensures there is only one instance of EnemyManager throughout game lifecycle
    public static EnemyManager Instance { get; private set; }

    [SerializeField] private float health = 40.0f; //Enemy health
    [SerializeField] private float attackDamage = 5.0f; //Enemy attack damage
    [SerializeField] private float damageEnemy = 0.0f; //Damage taken from player


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); //Destroys GameObject script it is attatched to if there is a duplicate
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Prevents EnemyManager from being destroyed when changing scenes
        }
    }

    //get and set enemy health
    //Enemy health is within 0 and 40
    public float Health
    {
        get { return health; }
        set { health = Mathf.Clamp(value, 0, 40); }
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
}
