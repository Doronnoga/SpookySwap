using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoalClass
{
    public class Goal : MonoBehaviour
    {
        [SerializeField]
        protected string playerTag = "";
        [SerializeField]
        protected bool goalCorrect = false;

        [SerializeField]
        public bool win = false;

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Triggered by: " + other.gameObject.name);
            // Check if the object inside the trigger has the correct playerTag
            if (other.gameObject.CompareTag(playerTag))
            {
                Debug.Log($"{playerTag} touched a CORRECT goal!");
                win = true;
            }
            else
            {
                Debug.Log($"INCORRECT: {other.gameObject.tag} touched a WRONG goal!");
                win = false;
            }
        }

        public virtual void OnTriggerExit2D(Collider2D other)
        {
                Debug.Log($"{other.gameObject.tag} left goal!");
                win = false;
        }

        void Start()
        {
            // Initialization logic (if any)
        }
    }
}