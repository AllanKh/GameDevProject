using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI countdownText;


    private void Start()
    {
        GameProjectManager.Instance.OnStateChanged += GameProjectManager_OnStateChanged; //Listen in on the "OnStateChanged" event 
        Hide(); //Hide by default
    }

    private void GameProjectManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameProjectManager.Instance.IsCountdownToStartActive()) //If the countdown to start the game is active
        {
            Show(); //Show the letters
        }
        else
        {
            Hide(); //Hide the letters
        }
    }

    private void Update()
    {
        //countdownText.text = GameProjectManager.Instance.GetCountdownToStartTimer().ToString(); //Update the timer to a new letter
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
