using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
