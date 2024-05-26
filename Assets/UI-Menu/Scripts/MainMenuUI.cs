using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    public static event EventHandler PlayButtonPress;

    //Remember to add all scenes into the Loader!


    private void Awake()
    {
        playButton.onClick.AddListener(() =>  //When play is pressed, do this
        {
            SoundManager.Instance.DestroySoundManager();
            Loader.Load(Loader.Scene.Allan); //Tell the loader which scene it should load after hitting the PlayButton!
            Time.timeScale = 1;
        });

        
        quitButton.onClick.AddListener(() => //When quit is pressed, do this
        {
            Application.Quit();  //Quit the game (Won't "work" inside the editor, but it does, trust me) 
        });

        Time.timeScale = 1.0f;



    }
}
