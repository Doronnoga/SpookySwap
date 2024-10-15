using PlayerMovementScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagerClass;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] 
    private Animator animator;
    [SerializeField]
    private PlayerMovement playerMovement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        // Subscribe to events
        if (playerMovement != null)
        {
            playerMovement.OnW += OnPlayerMove;
            playerMovement.OnS += OnPlayerMove;
            playerMovement.OnA += OnPlayerMove;
            playerMovement.OnD += OnPlayerMove;
            playerMovement.OnJump += MoveJump;
            playerMovement.OnStop += StopMovment;
            playerMovement.OnPush += PushMovment;
            playerMovement.OnSwitch += OnSwitchingPlayer;
        }
    }

    private void MoveJump() 
    {
        Debug.Log("Jump");
        animator.SetTrigger("Jump");
    }

    private void OnPlayerMove()
    {
        Debug.Log("Move");
        animator.SetBool("Move", true);
    }

    private void PushMovment() 
    {
        Debug.Log("Push");
        animator.SetBool("Push", true);
    }

    private void StopMovment() 
    {
        Debug.Log("StopMove");
        animator.SetBool("Move", false);
    }

    private void OnSwitchingPlayer() 
    {
        Debug.Log("Switched");
        animator.SetTrigger("Switched");
    }

}
