using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthUI : MonoBehaviour
{

    [SerializeField] private Image healthBarImage;
    [SerializeField] private Image staminaBarImage;
    [SerializeField] private Image manaBarImage;
    //Potions;
    [SerializeField] private Image HPPotionOne;
    [SerializeField] private Image HPPotionTwo;
    [SerializeField] private Image HPPotionThree;
    [SerializeField] private Image HPPotionFour;
    [SerializeField] private Image HPPotionFive;
    [SerializeField] private TextMeshProUGUI potionPlus;



    private void Awake()
    {
        HPPotionOne.gameObject.SetActive(false);
        HPPotionTwo.gameObject.SetActive(false);
        HPPotionThree.gameObject.SetActive(false);
        HPPotionFour.gameObject.SetActive(false);
        HPPotionFive.gameObject.SetActive(false);
        potionPlus.gameObject.SetActive(false);
    }

    private void Update()
    {
        healthBarImage.fillAmount = PlayerManager.Instance.Health / 100;
        staminaBarImage.fillAmount = PlayerManager.Instance.Stamina / 100;
        
        if (PlayerManager.Instance.HeldPotions > 5)
        {
            HPPotionOne.gameObject.SetActive(true);
            HPPotionTwo.gameObject.SetActive(true);
            HPPotionThree.gameObject.SetActive(true);
            HPPotionFour.gameObject.SetActive(true);
            HPPotionFive.gameObject.SetActive(true);
            potionPlus.gameObject.SetActive(true);
        }
        else if (PlayerManager.Instance.HeldPotions == 5)
        {
            HPPotionOne.gameObject.SetActive(true);
            HPPotionTwo.gameObject.SetActive(true);
            HPPotionThree.gameObject.SetActive(true);
            HPPotionFour.gameObject.SetActive(true);
            HPPotionFive.gameObject.SetActive(true);
            potionPlus.gameObject.SetActive(false);
        }
        else if (PlayerManager.Instance.HeldPotions == 4)
        {
            HPPotionOne.gameObject.SetActive(true);
            HPPotionTwo.gameObject.SetActive(true);
            HPPotionThree.gameObject.SetActive(true);
            HPPotionFour.gameObject.SetActive(true);
            HPPotionFive.gameObject.SetActive(false);
            potionPlus.gameObject.SetActive(false);
        }
        else if (PlayerManager.Instance.HeldPotions == 3)
        {
            HPPotionOne.gameObject.SetActive(true);
            HPPotionTwo.gameObject.SetActive(true);
            HPPotionThree.gameObject.SetActive(true);
            HPPotionFour.gameObject.SetActive(false);
            HPPotionFive.gameObject.SetActive(false);
            potionPlus.gameObject.SetActive(false);
        }
        else if (PlayerManager.Instance.HeldPotions == 2)
        {
            HPPotionOne.gameObject.SetActive(true);
            HPPotionTwo.gameObject.SetActive(true);
            HPPotionThree.gameObject.SetActive(false);
            HPPotionFour.gameObject.SetActive(false);
            HPPotionFive.gameObject.SetActive(false);
            potionPlus.gameObject.SetActive(false);
        }
        else if (PlayerManager.Instance.HeldPotions == 1)
        {
            HPPotionOne.gameObject.SetActive(true);
            HPPotionTwo.gameObject.SetActive(false);
            HPPotionThree.gameObject.SetActive(false);
            HPPotionFour.gameObject.SetActive(false);
            HPPotionFive.gameObject.SetActive(false);
            potionPlus.gameObject.SetActive(false);
        }
        else if (PlayerManager.Instance.HeldPotions <= 0)
        {
            HPPotionOne.gameObject.SetActive(false);
            HPPotionTwo.gameObject.SetActive(false);
            HPPotionThree.gameObject.SetActive(false);
            HPPotionFour.gameObject.SetActive(false);
            HPPotionFive.gameObject.SetActive(false);
            potionPlus.gameObject.SetActive(false);
        }

        //then mana? PlayerManager.Instance.HeldPotions

    }
}
