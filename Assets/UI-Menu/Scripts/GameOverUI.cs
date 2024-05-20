using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] private Button mainMenuButton;

    public static GameOverUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        Hide();
        mainMenuButton.onClick.AddListener(() => //When "mainMenuButton" is pressed, do this!
        {
            Loader.Load(Loader.Scene.MainMenuScene); //Go back to the Main Menu Scene.
            BossManager.Instance.DestroyBoss();
            PlayerManager.Instance.Health = 100;
            Hide();
        });

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
