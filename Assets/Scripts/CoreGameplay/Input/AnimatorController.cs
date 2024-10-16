using PlayerMovementScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagerClass;
using static GhostMovementScript.GhostMovement;
using GhostMovementScript;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] 
    private Animator animator;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private bool isGhost = false;
    [SerializeField]
    private GhostMovement ghostMovment;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        if (playerMovement != null)
        {
            playerMovement.OnMove += OnPlayerMove;
            playerMovement.OnJump += MoveJump;
            playerMovement.OnStop += StopMovment;
            playerMovement.OnPush += PushMovment;
            playerMovement.OnStopPull += StopPushMovment;
            playerMovement.OnSwitch += OnSwitchingPlayer;

            if (isGhost)
            {
                ghostMovment = GetComponent<GhostMovement>();
                ghostMovment.OnGhostMove += OnPlayerMove;
                ghostMovment.OnGhostStop += StopMovment;
                ghostMovment.OnGhostSwitch += OnSwitchingPlayer;
            }
        }
    }

    private void MoveJump() 
    {
        animator.SetTrigger("Jump");
    }

    private void StopMovment() 
    {
        animator.SetBool("Move", false);
    }

    private void OnPlayerMove()
    {
        animator.SetBool("Move", true);
    }

    private void PushMovment() 
    {
        animator.SetBool("Push", true);
    }

    private void StopPushMovment()
    {
        animator.SetBool("Push", false);
        animator.SetBool("Pull", false);
    }


    private void OnSwitchingPlayer() 
    {
        animator.SetTrigger("Switched");
    }

}
