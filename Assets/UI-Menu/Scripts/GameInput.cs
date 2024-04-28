using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    //For now this class will handle a lot of things.


    public static GameInput Instance { get; private set; }


    public event EventHandler OnPauseAction;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private PlayerInputActions playerInputActions;


    private bool isGamePause = false;


    private void Awake()
    {
        Instance = this; 
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Pause.performed += Pause_performed; //Listen for this input

    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);  //When the Pause button is pressed, activate this event!
        TogglePauseGame();
    }


    private void TogglePauseGame() //Pause the game! I need to figure out this with the others.
    {
        isGamePause = !isGamePause;

        if (isGamePause)
        {
            Debug.Log("Game -Paused-, almost");
            Time.timeScale = 0f;  //If all movement, animations and countdowns were "int * Time.deltaTime; this would work.
            OnGamePaused?.Invoke(this, EventArgs.Empty);  //Activate the Pause Event.
        }
        else
        {
            Debug.Log("Game -UnPaused-, almost");
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty); //Activate the Unpause Event.
        }


    }


}
