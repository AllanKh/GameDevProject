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

    private void Start()
    {
        BossWalk.secondPhase += BossWalk_secondPhase; //listen to this event
    }

    private void BossWalk_secondPhase(object sender, System.EventArgs e)
    {
        //When the boss enters second phase, do this:

        barImage.color = Color.red;
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
