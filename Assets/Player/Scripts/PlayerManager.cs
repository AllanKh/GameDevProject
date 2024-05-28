using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton to manage player stats globally across the game
// Ensures there is only one instance of PlayerManager throughout game lifecyle
public class PlayerManager : MonoBehaviour
{
    public static event EventHandler OnPlayerDamageTaken;
    public static PlayerManager Instance { get; private set; }

    private float stamina = 100.0f; // Players stamina
    private float health = 40.0f; // Players health
    private float attackDamage; // Players attack damage
    private float damagePlayer = 0.0f;
    private bool isInvincible;
    private bool isBlocking;
    private bool isChargingHeavyAttack = false;
    private bool hasBossKey = true;
    private int heldPotions = 0;

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
        health = 100.0f;
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
        OnPlayerDamageTaken?.Invoke(this, EventArgs.Empty);
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

    public bool Blocking
    {
        get { return isBlocking; }
        set
        {
            isBlocking = value;
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

    // Get and set if player has boss key
    public bool HasBossKey
    {
        get { return hasBossKey; }
        set
        {
            hasBossKey = value;
        }
    }

    public int HeldPotions
    {
        get { return heldPotions; }
        set
        {
            heldPotions = Mathf.Clamp(value, 0, 10);
        }
    }
}
