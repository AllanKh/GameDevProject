using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class PlayerDamageHandler : MonoBehaviour
{

  
    // Update is called once per frame
    void Update()
    {
        CheckIfDead();
    }

    private void CheckIfDead()
    {
        if (PlayerManager.Instance.Health <= 0)
        {
            UnityEngine.Debug.Log("I think you died!");
            //UnityEditor.EditorApplication.isPlaying = false;

            Time.timeScale = 0f;
            GameOverUI.Instance.Show();

        }
    }
}
