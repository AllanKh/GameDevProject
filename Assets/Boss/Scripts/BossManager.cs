using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
   public static BossManager Instance {  get; private set; }

    [SerializeField] private float health = 100.0f; // Boss Health
    [SerializeField] private float attackDamage = 10.0f; // Boss damage on player
    [SerializeField] private float damageEnemy = 0.0f; // Boss damage from player
 
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); //Destroys GameObject script it is attatched to if there is a duplicate
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Prevents BossManager from being destroyed when changing scenes
        }
    }

    //get and set boss health
    //Boss health is within 0 and 100
    public float Health
    {
        get { return health; }
        set { health = Mathf.Clamp(value, 0, 100); }
    }

    //get and set boss attack damage
    public float AttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }


    
}
