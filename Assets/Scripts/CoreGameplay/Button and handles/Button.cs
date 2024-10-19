using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using System.Diagnostics;

namespace ButtonClass
{
    public class Button : MonoBehaviour
    {
        public ControlType controlType; // declaration of the switch
        public GameObject buttonController;
        public GameObject leverController;
        private Animator anim;
        private Rigidbody rb;
        [SerializeField]
        private string tagToInteract;

        public delegate void ButtonPressed(); //declare a delegate type

        public event ButtonPressed OnButtonPressed; //event that reatcs to delegate
        public event ButtonPressed OnButtonReleased;

        public void Start()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            anim.SetBool("IsActivated", false);
        }

        // This will be called in edit mode when you change something in the Inspector
        private void OnValidate()
        {
            // Ensure this only runs in edit mode, not during play
            if (!Application.isPlaying)
            {
                SwitchControllers();
            }
        }

        //switch between a button and a lever
        public void SwitchControllers()
        {
            if (controlType == ControlType.Button)
            {
                buttonController.SetActive(true);
                leverController.SetActive(false);
                tagToInteract = "Body";
            }
            else if (controlType == ControlType.Lever)
            {
                buttonController.SetActive(false);
                leverController.SetActive(true);
                tagToInteract = "Skeleton";
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(tagToInteract))
            {
                anim.SetBool("IsActivated", true);
                OnButtonPressed?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(tagToInteract))
            {
                anim.SetBool("IsActivated", false);
                OnButtonReleased?.Invoke();
            }
        }
    }
}

public enum ControlType { Button, Lever } //the switch
