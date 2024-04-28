using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMainScript : MonoBehaviour
{

    //THIS CLASS DOES NOTHING AT THIS TIME

    private void Start()
    {
        
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction; //Listen to this event 
        Debug.Log("Game -Paused-");
    }

    private void GameInput_OnPauseAction(object sender, System.EventArgs e) //When the even activates, do this!
    {
        PauseGame();
    }


    private void PauseGame() //Pause the game, somehow
    {
        //Time.timeScale = 0f;
        Debug.Log("Game -Paused-");
    }



}
