using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;


    private void Awake()
    {
        playButton.onClick.AddListener(() =>  //When play is pressed, do this
        {
            SceneManager.LoadScene(1);
        });

        quitButton.onClick.AddListener(() => //When quit is pressed, do this
        {
            Application.Quit();  //Quit the game (Won't "work" inside the editor, but it does, trust me) 
        });
    }
}
