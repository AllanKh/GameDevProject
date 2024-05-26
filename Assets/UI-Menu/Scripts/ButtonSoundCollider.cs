using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonSoundCollider : MonoBehaviour , IPointerEnterHandler, ISelectHandler
{

    public static event EventHandler ButtonHovered;
    public static event EventHandler ButtonPressed;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("The mouse has entered the button!");
        Time.timeScale = 1; //This seems so silly
        ButtonHovered?.Invoke(this, EventArgs.Empty);
        Time.timeScale = 0; //So, SO very silly
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("The button was selected!");
        Time.timeScale = 1; //This seems so silly
        ButtonPressed?.Invoke(this, EventArgs.Empty);
        Time.timeScale = 0; //So, SO very silly
    }
}
