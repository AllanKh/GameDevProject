using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    float alpha = 0f;

    void Start()
    {
        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha);
    }

    void Update()
    {
        Debug.Log(PlayerManager.Instance.damageTaken);
        Debug.Log(alpha);

        if (PlayerManager.Instance.damageTaken == true && PlayerManager.Instance.Health != 0)
        {
            if (alpha == 0f)
            {
                alpha = 1f;
            }
            if (alpha > 0f)
            {
                alpha -= 1f * Time.deltaTime;
            }
            if (alpha <= 0f)
            {
                PlayerManager.Instance.damageTaken = false;
                alpha = 0f;
            }
        }
        gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha);
    }
}
