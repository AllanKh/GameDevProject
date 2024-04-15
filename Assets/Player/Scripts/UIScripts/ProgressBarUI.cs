using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressbarUI : MonoBehaviour
{

    [SerializeField] private Image barImage;

    private void Start()
    {
        
    }

    private void Update()
    {
        float test = PlayerManager.Instance.Health;
        barImage.fillAmount = test;
    }
}
