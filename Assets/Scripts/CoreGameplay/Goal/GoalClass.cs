using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoalClass
{
    public class Goal : MonoBehaviour
    {
        [SerializeField]
        protected string playerTag = "";  // The correct tag for players entering the goal
        [SerializeField]
        protected bool goalCorrect = false;  // Tracks if the correct player entered
        [SerializeField]
        public bool win = false;  // Tracks if the goal is "won"

        public delegate void GoalCheck();
        public event GoalCheck OnGoalEnter;

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Triggered by: " + other.gameObject.name);

            if (other.gameObject.CompareTag(playerTag))
            {
                Debug.Log($"{playerTag} touched a CORRECT goal!");
                win = true;
                goalCorrect = true;
                OnGoalEnter?.Invoke();  // Invoke event for LevelManager
            }
            else
            {
                Debug.Log($"INCORRECT: {other.gameObject.tag} touched a WRONG goal!");
                win = false;
                goalCorrect = false;
            }
        }

        // Called when an object exits the trigger
        public virtual void OnTriggerExit2D(Collider2D other)
        {
            // Ensure it's the correct player leaving, then reset
            if (other.gameObject.CompareTag(playerTag))
            {
                Debug.Log($"{playerTag} left goal!");
                win = false;
                goalCorrect = false;
            }
        }
    }
}
