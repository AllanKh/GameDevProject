using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{


    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;


    private void Awake()
    {
        resumeButton.onClick.AddListener(() => //When "resumeButton" is pressed, do this!
        {
            GameInput.Instance.TogglePauseGame(); //Unpause the game
        });
        mainMenuButton.onClick.AddListener(() => //When "mainMenuButton" is pressed, do this!
        {
            Loader.Load(Loader.Scene.MainMenuScene); //Go back to the Main Menu Scene.
        });
    }

    private void Start()
    {
        //Start Listening to the events
        GameInput.Instance.OnGamePaused += GameInput_OnGamePaused;
        GameInput.Instance.OnGameUnpaused += GameInput_OnGameUnpaused;

        Hide();
    }

    private void GameInput_OnGameUnpaused(object sender, System.EventArgs e) //If this event activates, do this!
    {
        Hide(); 
    }

    private void GameInput_OnGamePaused(object sender, System.EventArgs e) //If this event activates, do this!
    {
        Show();
    }

    private void Show()  //Show the pause screen.
    {
        gameObject.SetActive(true);
    }

    private void Hide()  //Hide the pause screen.
    {
        gameObject.SetActive(false);
    }



}
