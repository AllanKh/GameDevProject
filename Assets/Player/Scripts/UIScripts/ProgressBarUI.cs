using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressbarUI : MonoBehaviour
{
    private Image barImage;

    private void Start()
    {
        // Find the 'Bar' child GameObject
        GameObject barGameObject = transform.Find("Bar").gameObject;

        // Check if the 'Bar' GameObject was found
        if (barGameObject != null)
        {
            // Get the Image component from the 'Bar' GameObject
            barImage = barGameObject.GetComponent<Image>();

            // Check if the Image component was successfully retrieved
            if (barImage == null)
            {
                Debug.LogError("No Image component found on 'Bar' GameObject.");
            }
        }
        else
        {
            Debug.LogError("No child GameObject named 'Bar' found.");
        }
    }

    private void Update()
    {
        // Assuming PlayerManager and its Health property exist and are correctly set up
        if (barImage != null && PlayerManager.Instance != null)
        {
            barImage.fillAmount = (PlayerManager.Instance.Health / 100);
        }
    }
}
