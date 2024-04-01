using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        GameProjectManager.Instance.OnStateChanged += GameProjectManager_OnStateChanged; //Listen in on the "OnStateChanged" event 
        Hide(); //Hide by default
    }

    private void GameProjectManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameProjectManager.Instance.IsGameOver()) //If the Game is Over
        {
            Show();

            scoreText.text = "100";

        }
        else
        {
            Hide(); 
        }
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
