using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoalClass
{
    public class GoalClass : MonoBehaviour
    {
        [SerializeField]
        private string goalName = "";

        [SerializeField]
        protected string playerTag = "";


        // Called when another object with a Collider2D marked as "Is Trigger" enters this object's Collider2D
        public void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Triggered by: " + other.gameObject.name);

            // Check for different tags and handle them
            if (other.gameObject.CompareTag("Ghost"))
            {
                Debug.Log("Ghost touched a goal!");
            }

            if (other.gameObject.CompareTag("Skeleton"))
            {
                Debug.Log("Skeleton touched a goal!");
            }

            if (other.gameObject.CompareTag("Body"))
            {
                Debug.Log("Body touched a goal!");
            }
        }

        // Called every frame while another object is within this object's Collider2D (trigger)
        public virtual void OnTriggerStay2D(Collider2D other)
        {
            // Check if the object inside the trigger has the correct playerTag
            if (other.gameObject.CompareTag(playerTag))
            {
                Debug.Log($"{playerTag} touched a CORRECT goal!");
            }
            else
            {
                Debug.Log($"INCORRECT: {other.gameObject.tag} touched a WRONG goal!");
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            // Initialization logic (if any)
        }
    }
}