using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton class to manage player stats globally
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField] private float stamina = 100.0f;
    [SerializeField] private float health = 100.0f;
    [SerializeField] private float damage = 10.0f;

    // Called when instance is loaded and ensures there is only one instance of PlayerManager
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // Gets rid of duplicates
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Get stamina
    public float Stamina
    {
        get { return stamina; }
        set
        {
            // Keeps stamina between 0 and 100
            stamina = Mathf.Clamp(value, 0, 100);
        }
    }

    // Get health
    public float Health
    {
        get { return health; }
        set
        {
            // Keeps health between 0 and 100
            health = Mathf.Clamp(value, 0, 100);
        }
    }

    // Get damage
    public float Damage
    {
        get { return damage; }
        set
        {
            damage = value;
        }
    }

}
