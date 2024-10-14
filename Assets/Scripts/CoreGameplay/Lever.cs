using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using Unity.Mathematics;

namespace ButtonClass
{
    public class Lever : MonoBehaviour
    {
        [SerializeField]
        private string tagToInteract = "Skeleton";

        private Animator anim;
        private Rigidbody rb;

        public delegate void ButtonPressed(); //declare a delegate type

        public event ButtonPressed OnButtonPressed; //event that reatcs to delegate
        public event ButtonPressed OnButtonReleased;

        public void Start()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            anim.SetBool("IsActivated", false);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(tagToInteract))
            {
                anim.SetBool("IsActivated", true);
                OnButtonPressed?.Invoke();
            }
            else
            {
                anim.SetBool("IsActivated", false);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(tagToInteract))
            {
                OnButtonReleased?.Invoke();
            }
        }
    }
}

