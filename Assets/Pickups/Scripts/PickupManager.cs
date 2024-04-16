using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField] private int healthRegen = 10;
    [SerializeField] private int scoreMultiplier = 10;
    private int totalCoins;

    public void PotionPickupEvent()
    {
        PlayerManager.Instance.Health += healthRegen;

    }
    



}
