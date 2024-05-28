using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWinUI : MonoBehaviour
{



    [SerializeField] private Button mainMenuButton;

    public static GameWinUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        Hide();
        mainMenuButton.onClick.AddListener(() => //When "mainMenuButton" is pressed, do this!
        {
            PlayerManager.Instance.Health = 100;
            Loader.Load(Loader.Scene.MainMenuScene); //Go back to the Main Menu Scene.
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
