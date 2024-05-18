using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpell : MonoBehaviour
{
    private bool hit;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hit = true;
            Debug.Log("Player entered spell area");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hit = false;
            Debug.Log("Player exited spell area");
        }
    }

    private void PlayerHit()
    {
        if (hit)
        {
            PlayerManager.Instance.DamagePlayer(BossManager.Instance.AttackDamage);
        }
    }
    public void CallSpell()
    {
        anim.SetTrigger("Spell");

    }

}
