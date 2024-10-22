using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ButtonClass.Button;

namespace CollectibleClass 
{
    public class LevelCollectible : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField]
        protected string targetTag;
        [SerializeField]
        protected float time;
        [SerializeField]
        Animator animator;
        public bool collected = false;


        public delegate void CollectCollectible(); //declare a delegate type

        public event CollectCollectible OnCollected; //event that reatcs to delegate

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(targetTag))
            {
                Debug.Log("Collectible Collected");
                OnCollected?.Invoke();//invoke getting collected
                animator.SetTrigger("Destroy");//do anim
                Invoke("OnDestroy" , time);//wait anim durationto get destroyed
            }
        }

        private void OnDestroy()
        {
            Debug.Log("Collectible Destroyed");
            Destroy(gameObject, time);//die
            collected = true;//for goal porpuse
        }
    }
}
