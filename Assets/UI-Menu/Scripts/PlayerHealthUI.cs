using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthUI : MonoBehaviour
{

    [SerializeField] private Image healthBarImage;
    [SerializeField] private Image staminaBarImage;
    [SerializeField] private Image manaBarImage;


    private void Update()
    {
        healthBarImage.fillAmount = PlayerManager.Instance.Health / 100;
        staminaBarImage.fillAmount = PlayerManager.Instance.Stamina / 100;
        //then mana?
    }
}
