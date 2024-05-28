using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
   public static BossManager Instance {  get; private set; }

    //Variables
    [SerializeField] private float health = 500.0f; // Boss Health
    [SerializeField] private float attackDamage = 10.0f; // Boss damage on player
 
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); //Destroys GameObject script it is attatched to if there is a duplicate
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Prevents this script from being destroyed when changing scenes
        }
    }

    //get and set boss health
    //Boss health is within 0 and 500
    public float Health
    {
        get { return health; }
        set { health = Mathf.Clamp(value, 0, 500); }
    }

    //get and set boss attack damage
    public float AttackDamage
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }

    //call this to destroy boss
    public void DestroyBoss()
    {
        Destroy(gameObject);
    }
   
}
