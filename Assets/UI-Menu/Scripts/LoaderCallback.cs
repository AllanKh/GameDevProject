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
    [SerializeField] private TextMeshProUGUI loadingText;

    private bool isFirstUpdate = true;

    public static float delayTimer = 12;
    
    private bool startTimer = false;

    private void Awake()
    {
        continueText.gameObject.SetActive(true);
        loadingText.gameObject.SetActive(false);
    }

    private void Update()
    {
        startTimer = true;

        if (isFirstUpdate)
        {
                
            if (Input.GetKeyDown("space"))
            {

                if (isFirstUpdate)
                {
                    isFirstUpdate = false;
                    continueText.gameObject.SetActive(false);
                    loadingText.gameObject.SetActive(true);
                    Loader.LoaderCallback();
                }

                
            }
        }
    }

}
