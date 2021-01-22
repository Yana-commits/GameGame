﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomBt : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    [HideInInspector]
    public bool pressed = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
   
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }
}
