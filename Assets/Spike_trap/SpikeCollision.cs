using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCollision : MonoBehaviour
{

    private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        

    }



    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            playerManager.DamagePlayer(100);

    
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
        
    //}
}
