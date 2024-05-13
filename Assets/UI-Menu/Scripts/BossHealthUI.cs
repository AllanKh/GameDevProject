using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    public static BossHealthUI instance {  get; private set; }


    [SerializeField] private Image barImage;


    private void Awake()
    {
        instance = this;
        Hide();
    }

  



    private void Update()
    {
        
        barImage.fillAmount = BossManager.Instance.Health / 500;   
        

        
        if (barImage.fillAmount <= 0)
        {
            Hide();

        }
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }


}
