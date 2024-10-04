using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrittlePlatform : MonoBehaviour
{
    [Header("Target")]
    [SerializeField]
    protected string targetTag;

    private bool playerInArea = false;
    private bool isBroken = false;

    [Header("Time to Re-Appear")]
    [SerializeField]
    private float reappearDelay = 15f;

    [SerializeField]
    protected Animator animator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            playerInArea = true;
            if (!isBroken) 
            {
                BreakPlatform();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            playerInArea = false;
            if (isBroken) 
            {
                Invoke("TryReappear", reappearDelay);
            }
        }
    }

    private void BreakPlatform()
    {
        animator.SetTrigger("Break");
        isBroken = true;
    }

    private void TryReappear()
    {
        if (!playerInArea && isBroken)
        {
            animator.SetTrigger("Appear");
            isBroken = false;
        }
    }

}