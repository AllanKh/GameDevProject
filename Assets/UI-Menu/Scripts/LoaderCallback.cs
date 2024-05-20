using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LoaderCallback : MonoBehaviour
{

    //This class just lets the loader know that the very first update of the loader has happened.


    [SerializeField] private TextMeshProUGUI continueText;

    private bool isFirstUpdate = true;

    public static float delayTimer = 12;
    
    private bool startTimer = false;

    private void Awake()
    {
        continueText.gameObject.SetActive(false);
    }

    private void Update()
    {
        startTimer = true;

        if (isFirstUpdate)
        if (startTimer)
        {
            delayTimer -= 1.0f * Time.deltaTime;

                if (delayTimer <= 9.0f)
                {
                    continueText.gameObject.SetActive(true);
                }
    

            if (delayTimer <= 0.0f || Input.GetKeyDown("space"))
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
