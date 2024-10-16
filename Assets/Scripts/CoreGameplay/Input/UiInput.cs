using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

namespace uiInputScript
{
    public class UiInput : MonoBehaviour
    {
        public delegate void uiHandlerDelegate();
        public event uiHandlerDelegate OnEsc;

        private void CheckEsc()
        {
            OnEsc?.Invoke();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CheckEsc();
            }
        }
    }
}
