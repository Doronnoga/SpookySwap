using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoalClass;
using PlayerMovementScript;
using UnityEngine.UI;
using Cinemachine;
using CollectibleClass;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Playables;

namespace LevelManagerClass
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Camera\n")]
        [SerializeField] 
        private CinemachineVirtualCamera virtualCamera;
        [SerializeField]
        public Timeline timeline;

        public bool beenWon = false;

        [Header("GOALS\n")]
        [SerializeField]
        List<Goal> goalList = new List<Goal>();
        int goalsWon = 0;

        [Header("PLAYERS\n")]
        [SerializeField]
        private GameObject Ghost;
        [SerializeField]
        private GameObject Skeleton;
        [SerializeField]
        private GameObject Body;
        private GameObject lastActivePlayer;


        [Header("PLAYERS ACTIVITION scripts\n")] //checks if all the players should be in this scene
        [SerializeField]
        private bool isSkeletonActive = true;
        [SerializeField]
        private bool isBodyActive = true;

        [Header("Player's MOVEMENT scripts\n")]
        [SerializeField]
        private PlayerMovement ghostMovement;
        [SerializeField]
        private PlayerMovement skeletonMovement;
        [SerializeField]
        private PlayerMovement bodyMovement;

        [Header("Ui CANVAS and BUTTONS\n")]
        [SerializeField]
        public GameObject uiCanvas;
        [SerializeField]
        List<Button> activePlayerButtonList = new List<Button>();

        [Header("Collectible\n")]
        [SerializeField]
        public LevelCollectible levelCollectible;

        public delegate void LevelManagerDelegate();
        public event LevelManagerDelegate levelWon;
        public event LevelManagerDelegate playUISound;
        public void checkIfNull()
        {
            if (virtualCamera == null)
            {
                Debug.LogError("Cinemachine Virtual Camera is not assigned!");
            }
            if (goalList.Contains(null))
            {
                Debug.LogError("THE LEVEL MANAGER IS MISSING A GOAL: please give it a goal.");
            }
            if (Ghost == null)
            {
                Debug.LogError("GHOST IS NULL please give it a PLAYER.");
            }
            else if (Skeleton == null) 
            {
                Debug.LogError("SKELETON IS NULL please give it a PLAYER.");
            }
            else if (Body == null) 
            {
                Debug.LogError("BODY IS NULL please give it a PLAYER.");
            }
        }

        private void ActivatePlayer(GameObject player)
        {
            //checkwhowasactive
            lastActivePlayer = player;

            //turn all off if not null
            if (ghostMovement != null)
            {
                ghostMovement.enabled = false;
            }
            if (bodyMovement != null)
            {
                bodyMovement.enabled = false;
            }
            if (skeletonMovement != null)
            {
                skeletonMovement.enabled = false;
            }

            for (int i = 0; i < activePlayerButtonList.Count; i++)
            {
                activePlayerButtonList[i].interactable = false;
            }

            // compare tag
            if (player.tag.Equals("Ghost"))
            {
                ghostMovement.enabled = true;
                activePlayerButtonList[0].interactable = true;
            }
            else if (player.tag.Equals("Body"))
            {
                bodyMovement.enabled = true;
                activePlayerButtonList[2].interactable = true;
            }
            else if (player.tag.Equals("Skeleton"))
            {
                skeletonMovement.enabled = true;
                activePlayerButtonList[1].interactable = true;
            }
            playUISound?.Invoke();
        }

        private void disablePlayers() 
        {
            //turn all off
            ghostMovement.enabled = false;
            bodyMovement.enabled = false;
            skeletonMovement.enabled = false;
            for (int i = 0; i < activePlayerButtonList.Count; i++)
            {
                activePlayerButtonList[i].interactable = false;
            }//turn off interaction button
        }

        private void checkInput()
        {
            if (lastActivePlayer != null)
            {
                if (lastActivePlayer.GetComponent<Animator>().GetBool("Move"))
                {
                    lastActivePlayer.GetComponent<Animator>().SetBool("Move", false);
                }// no more running on transfer
            }
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) // Switch to Ghost
            {
                ActivatePlayer(Ghost);
                changeCameraTarget(Ghost.transform);
                Skeleton.GetComponent<Animator>().SetTrigger("Unswitched");
                Body.GetComponent<Animator>().SetTrigger("Unswitched");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) // Switch to Skeleton
            {
                if (isSkeletonActive)
                {
                    ActivatePlayer(Skeleton);
                    changeCameraTarget(Skeleton.transform);
                    Body.GetComponent<Animator>().SetTrigger("Unswitched");
                }
                else
                {                  
                    Debug.Log("SkeletonIsn'tactive");
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3) && isBodyActive) // Switch to Body
            {
                if (isBodyActive)
                {
                    ActivatePlayer(Body);
                    changeCameraTarget(Body.transform);
                    Skeleton.GetComponent<Animator>().SetTrigger("Unswitched");
                }
                else 
                {
                    Debug.Log("BodyIsntActive"); 
                }
            }
        }

        private void checkForWin()
        {
            goalsWon = 0;

            // Count how many goals have been won
            foreach (var goal in goalList)
            {
                if (goal.win)
                {
                    goalsWon++;
                }
            }

            // Check if all goals have been won
            if (goalsWon == goalList.Count && (levelCollectible == null || levelCollectible.collected)) //if there are collectibles in scene & if they were collected
            {
                if (!beenWon)
                {
                    if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1) //checking if it's the goal of the last scene
                    {
                        SceneManager.LoadScene(0);
                    }

                    else if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 2) //checking if it's the goal of before the last scene
                    {
                        disablePlayers();
                        timeline.PlayTimeline();         
                    }

                    else
                    {
                        Debug.Log("Levbel is won");
                        levelWon?.Invoke();
                        beenWon = true;
                        disablePlayers();
                    }
                }
            }
            else
            {
                if (beenWon) 
                {
                    beenWon = false;
                }
            }
        }

        private void changeCameraTarget(Transform targetTransform)
        {
            if (virtualCamera != null && targetTransform != null)
            {
                virtualCamera.Follow = targetTransform;
                virtualCamera.LookAt = targetTransform;
            }
        }

        void Start ()
        {
            for (int i = 0; i < goalList.Count; i++) 
            {
               goalList[i].OnGoalEnter += checkForWin;
            }
            checkIfNull();
            if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1) //checking of it's the credit scene
            {
                ActivatePlayer(Body);
                changeCameraTarget(Body.transform);
            }
            else
            {
                ActivatePlayer(Ghost);
            }
        }

        private void Update ()
        {
            if (!beenWon) //if level is not won check stuff
            {
              checkInput();
            }
        }//check for input
    }
}