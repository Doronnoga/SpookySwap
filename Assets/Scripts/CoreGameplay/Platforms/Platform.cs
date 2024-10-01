using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Target")]
    [SerializeField]
    protected string targetTag;
    [SerializeField]
    private Collider2D parentTriggerCollider;//THIS DOESNT WORK YET TRIGGER COLLISIONNOT CONSISTANT

    private bool playerInArea = false;
    private bool isBroken = false;
    [Header("Time to Re-Appear")]
    [SerializeField]
    private float reappearDelay = 15f;


    [SerializeField]
    protected Animator animator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            BreakPlatform();
        Invoke("TryReappear", reappearDelay); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            Debug.LogWarning("IN AREA");
            playerInArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            Debug.LogWarning("EXIT NOT IN AREA");
            playerInArea = false;
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
            Debug.LogWarning("TRY APPEAR");
            animator.SetTrigger("Appear");
            isBroken = false; 
        }
        else
        {
            Invoke("TryReappear", 0.5f); 
        }
    }

}