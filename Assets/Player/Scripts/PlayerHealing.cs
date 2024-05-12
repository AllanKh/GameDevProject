using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealing : MonoBehaviour
{
    public ParticleSystem healingParticles;

    private void Start()
    {
        healingParticles = transform.Find("HealingParticles").GetComponent<ParticleSystem>();
    }

    void Update()
    {
        UseHealthPotion();
    }

    private void UseHealthPotion()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (PlayerManager.Instance.HeldPotions > 0)
            {
                PlayerManager.Instance.HeldPotions--;
                PlayerManager.Instance.Health += 10;

                if (healingParticles != null)
                {
                    healingParticles.Play();
                }

            }
        }
    }
}
