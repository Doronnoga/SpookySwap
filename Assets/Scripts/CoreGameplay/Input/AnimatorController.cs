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
            playerMovement.OnSwitch += OnSwitchingPlayer;

            if (isGhost)
            {
                ghostMovment = GetComponent<GhostMovement>();
                ghostMovment.OnMove += OnPlayerMove;
                ghostMovment.OnStop += StopMovment;
                ghostMovment.OnSwitch += OnSwitchingPlayer;
            }
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
