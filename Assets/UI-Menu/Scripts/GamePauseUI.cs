using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{

    public static GamePauseUI Instance { get; private set; }

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;


    private void Awake()
    {
        Instance = this;

        resumeButton.onClick.AddListener(() => //When "resumeButton" is pressed, do this!
        {
            GameInput.Instance.TogglePauseGame(); //Unpause the game
            Hide();
        });
        mainMenuButton.onClick.AddListener(() => //When "mainMenuButton" is pressed, do this!
        {
            PlayerManager.Instance.Health = 100;
            PlayerManager.Instance.HasBossKey = false;
            Loader.Load(Loader.Scene.MainMenuScene); //Go back to the Main Menu Scene.
            BossManager.Instance.DestroyBoss();
            SoundManager.Instance.DestroySoundManager();
        });
        optionsButton.onClick.AddListener(() => 
        {
            OptionsUI.Instance.Show();
            Hide();
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

    public void Show()  //Show the pause screen.
    {
        gameObject.SetActive(true);
    }

    public void Hide()  //Hide the pause screen.
    {
        gameObject.SetActive(false);
    }



}
