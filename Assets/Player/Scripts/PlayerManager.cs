using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton class to manage player stats globally across the game
// Ensures there is only one instance of PlayerManager throughout game lifecyle
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField] private float stamina = 100.0f; // Players stamina
    [SerializeField] private float health = 100.0f; // Players health
    [SerializeField] private float damage = 10.0f; // Players attack damage

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
    public float Damage
    {
        get { return damage; }
        set
        {
            damage = value;
        }
    }

}
