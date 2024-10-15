using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class UiInput : MonoBehaviour
{
    public delegate void uiHandlerDelegate();
    public event uiHandlerDelegate OnEsc;
    
    
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        }
    }
}
