using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class FireTrap : MonoBehaviour
{
    //variables
    [SerializeField] private float damage;

    //timers
    [SerializeField] private float activationDelay;
    [SerializeField] private float activationTime;
    [SerializeField] private float intervalTime;

    //references
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    //bools
    private bool active;
    private bool hit;
    private bool playerInTrap;



    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        active = false;
        hit = false;
        playerInTrap = false;
        StartCoroutine(ActivateFireTrap());
    }

    private void Update()
    {
        DamagePlayerWithTrap();
    }

    private void OnTriggerEnter2D(Collider2D collision) //check for collision between player and trap
    {
        if (collision.CompareTag("Player"))
        {
            playerInTrap = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //check for player exiting the trap collider
    {
        if (collision.CompareTag("Player"))
        {
            playerInTrap = false;
        }
    }

    private void DamagePlayerWithTrap() //damage player when in trap and trap is active
    {
        if (active && playerInTrap && !hit) 
        {
            PlayerManager.Instance.DamagePlayer(damage);
            hit = true;
        }
        else if(!playerInTrap)
        {
            hit = false;
        }
    }

    private IEnumerator ActivateFireTrap()
    {
        while(true)
        {
            //turn the sprite red to notify the player and trigger the trap
            spriteRenderer.color = Color.red;

            //Wait for delay, activate trap, turn on animation, return color back to normal
            yield return new WaitForSeconds(activationDelay);
            spriteRenderer.color = Color.white; //turn the sprite back to its initial color
            active = true;
            animator.SetBool("activated", true);

            //Wait until X seconds, deactivate trap and reset all variables and animator
            yield return new WaitForSeconds(activationTime);
            active = false;
            animator.SetBool("activated", false);
            hit = false;

            yield return new WaitForSeconds(intervalTime); // start the trap again after the interval time
        }
    }

}
