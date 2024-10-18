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
        private Animator animator;

        public delegate void GoalCheck();
        public event GoalCheck OnGoalEnter;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        public virtual void OnTriggerEnter2D(Collider2D other)
        {

            if (other.gameObject.CompareTag(playerTag))
            {
                animator.SetBool("Active" , true);
                win = true;
                goalCorrect = true;
                OnGoalEnter?.Invoke();  // Invoke event for LevelManager
            }
            else
            {
                animator.SetBool("Active", false);
                win = false;
                goalCorrect = false;
            }
        }

        public virtual void OnTriggerExit2D(Collider2D other)
        {
            // Ensure it's the correct player leaving, then reset
            if (other.gameObject.CompareTag(playerTag))
            {
                animator.SetBool("Active", false);
                win = false;
                goalCorrect = false;
            }
        }
    }
}
