using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace ButtonClass 
{ 
   public class Button : MonoBehaviour
{
    [SerializeField]
    private string tagToInteract = "Body";

    public delegate void ButtonPressed(); //declare a delegate type

    public event ButtonPressed OnButtonPressed; //event that reatcs to delegate
    public event ButtonPressed OnButtonReleased;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagToInteract)) 
        {
            OnButtonPressed?.Invoke();
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
