using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{

    public static PickupManager Instance { get; private set; }

    [SerializeField] static int healthRegen = 10;
    [SerializeField] private int scoreMultiplier = 10;
    private int totalCoins;
    private string objectTag;

    public void PickupEvent(bool condition)
    {

        if (condition && objectTag == "Potion")
        {
            PlayerManager.Instance.Health += healthRegen;
        }
        
    }

    public int GetHealthRegen()
    {
        return healthRegen;
    }

}
