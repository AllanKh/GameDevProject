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
        ButtonHovered?.Invoke(this, EventArgs.Empty);
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("The button was selected!");
        ButtonPressed?.Invoke(this, EventArgs.Empty);
    }
}
