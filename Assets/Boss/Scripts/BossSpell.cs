using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpell : MonoBehaviour
{
    //Variables
    private bool hit;

    //References
    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        hit = false;
        CallSpell();
    }

    //Check collision between spell and player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hit = true;
            Debug.Log("Player entered spell area");
        }
    }

    //Check when player exits spell collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hit = false;
            Debug.Log("Player exited spell area");
        }
    }

    //hit player if hit is true
    private void PlayerHit()
    {
        if (hit)
        {
            PlayerManager.Instance.DamagePlayer(BossManager.Instance.AttackDamage);
        }
    }

    //Method to call spell
    public void CallSpell()
    {
        anim.SetTrigger("Spell");

    }

}
