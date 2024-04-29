using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthUI : MonoBehaviour
{

    [SerializeField] private Image barImage;


    private void Update()
    {
        barImage.fillAmount = PlayerManager.Instance.Health;
    }
}
