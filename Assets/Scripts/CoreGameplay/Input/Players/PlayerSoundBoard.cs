using GhostMovementScript;
using PlayerMovementScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundBoard : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private bool isGhost = false;
    [SerializeField]
    private GhostMovement ghostMovment;
    private bool isPlaying = false;
    [SerializeField]
    private AudioSource audioSource;

    [Header("Soundboard")]
    [SerializeField]
    private AudioClip move;
    [SerializeField]
    private AudioClip jump;
    [SerializeField]
    private AudioClip push;
    [SerializeField]
    private AudioClip possessed;
   
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        if (audioSource == null) { Debug.LogError("No Audio Source!"); }
        if (playerMovement == null) { Debug.LogError("No Player movment script!"); }


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

    private void OnPlayerMove() 
    {
        if (move != null && !audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.PlayOneShot(move);
        }
    }

    private void MoveJump()
    {
        if (jump != null && !audioSource.isPlaying)
        {
            audioSource.loop = false;
            audioSource.PlayOneShot(jump);
        }
    }

    private void StopMovment() 
    {
        if (move != null && audioSource.isPlaying)
        {
            isPlaying = false;
            audioSource.loop = false;
            audioSource.Stop();
        }
    }

    private void PushMovment() 
    {
        if (push != null && !audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.PlayOneShot(push);
        }
    }

    private void StopPushMovment() 
    {
        if (audioSource.isPlaying)
        audioSource.loop = false;
        audioSource.Stop();
    }

    private void OnSwitchingPlayer() 
    {
        if (possessed != null && !audioSource.isPlaying)
        {
            audioSource.loop = false;
            audioSource.PlayOneShot(possessed);
        }
    }
}
