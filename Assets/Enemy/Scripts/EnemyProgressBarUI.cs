using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyProgressbarUI : MonoBehaviour
{

    [SerializeField] private Image barImage;

    private void Start()
    {
        
    }

    private void Update()
    {
        barImage.fillAmount = EnemyManager.Instance.Health;
    }
}
