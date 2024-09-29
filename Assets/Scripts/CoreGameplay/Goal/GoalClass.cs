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

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Triggered by: " + other.gameObject.name);
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