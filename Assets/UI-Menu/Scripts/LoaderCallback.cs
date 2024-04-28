using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{

    //This class just lets the loader know that the very first update of the loader has happened.

    private bool isFirstUpdate = true;

    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;

            Loader.LoaderCallback();
        }
    }

}
