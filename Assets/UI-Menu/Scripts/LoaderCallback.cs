using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderCallback : MonoBehaviour
{

    //This class just lets the loader know that the very first update of the loader has happened.

    private bool isFirstUpdate = true;

    public static float delayTimer = 12;
    
    private bool startTimer = false;

    private void Update()
    {
        startTimer = true;

        if (isFirstUpdate)
        if (startTimer)
        {
            delayTimer -= 1.0f * Time.deltaTime;

            if (delayTimer <= 0.0f)
            {

                //if (isFirstUpdate)
                //{
                    isFirstUpdate = false;

                    Loader.LoaderCallback();
                //}

                startTimer = false;
            }
        }
    }

}
