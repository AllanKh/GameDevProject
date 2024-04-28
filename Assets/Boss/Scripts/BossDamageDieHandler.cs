using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossDamageDieHandler : MonoBehaviour
{
    public static BossDamageDieHandler Instance { get; private set; }

    [SerializeField] private Behaviour[] components;

    private Animator anim;
    private bool dead;


    private void Awake()
    {
        anim = GetComponent<Animator>();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); //Destroys GameObject script it is attatched to if there is a duplicate
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Prevents BossDamageDieHandler from being destroyed when changing scenes
        }
    }

    //Apply damage to boss and reduce boss health
    public void DamageBoss(float amountOfDamage)
    {
        BossManager.Instance.Health -= amountOfDamage;

        if (BossManager.Instance.Health > 0)
        {
            anim.SetTrigger("hurt");
        }
        else if (BossManager.Instance.Health <= 0)
        {
            if (!dead)
            {
                anim.SetTrigger("die");

                //Deactivate all attached components classes to boss
                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }
                dead = true;
            }
        }
    }
}
