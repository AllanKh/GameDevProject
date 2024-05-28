using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarFade : MonoBehaviour
{



    private Image barImage;
    private SkeletonManager skeletonManager;

    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
        skeletonManager = transform.Find("Enemy1.1").GetComponent<SkeletonManager>();
    }

    private void Start()
    {
        
    }
    private void SetHealth()
    {
        //barImage.fillAmount = skeletonManager.SkeletonHealth;
    }


}
