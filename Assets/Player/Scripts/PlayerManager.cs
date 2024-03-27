using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton to manage player stats globally across the game
// Ensures there is only one instance of PlayerManager throughout game lifecyle
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField] private float stamina = 100.0f; // Players stamina
    [SerializeField] private float health = 100.0f; // Players health
    [SerializeField] private float attackDamage = 10.0f; // Players attack damage
    [SerializeField] private float damagePlayer = 0.0f;
    [SerializeField] private bool isInvincible;
    private bool isChargingHeavyAttack = false;

    // Called when instance is loaded and ensures there is only one instance of PlayerManager
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destorys GameObject script is attatched to if there is a duplicate
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevents PlayerManager from being destroyed when changing scenes
        }
    }

    // get and set player stamina
    // Keeps stamina between 0 and 100
    public float Stamina
    {
        get { return stamina; }
        set
        {
            stamina = Mathf.Clamp(value, 0, 100);
        }
    }

    // get and set player health
    // Keeps health between 0 and 100
    public float Health
    {
        get { return health; }
        set
        {
            health = Mathf.Clamp(value, 0, 100);
        }
    }

    // get and set player damage
    public float AttackDamage
    {
        get { return attackDamage; }
        set
        {
            attackDamage = value;
        }
    }

    // Apply damage to player and reduce health
    public void DamagePlayer(float damageAmount)
    {
        Health -= damageAmount;
    }

    public bool Invincible
    {
        get { return isInvincible; }
        set
        {
            isInvincible = value;
        }
    }

    public bool IsChargingHeavyAttack
    {
        get { return isChargingHeavyAttack; }
        set
        {
            isChargingHeavyAttack = value;
        }

    }
}
