using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private Image barImage;


    private void Update()
    {
        barImage.fillAmount = BossManager.Instance.Health / 100;
    }


}
