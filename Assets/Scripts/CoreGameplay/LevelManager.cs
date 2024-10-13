using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoalClass;
using PlayerMovementScript;
using UnityEngine.UI;
using Cinemachine;
using CollectibleClass;
using static GoalClass.Goal;


namespace LevelManagerClass
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Scene name\n")]
        [SerializeField] 
        private CinemachineVirtualCamera virtualCamera;
        [SerializeField]
        private int sceneIndex;//for game manager
        [SerializeField]
        public string sceneName = "";//name of connected scene
        [SerializeField]
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
        public GameObject endScreenCanvas;
        [SerializeField]
        public GameObject uiCanvas;
        [SerializeField]
        List<Button> activePlayerButtonList = new List<Button>();

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
            if (Input.GetKeyDown(KeyCode.Alpha1 ) || Input.GetKeyDown(KeyCode.Keypad1)) // Switch to Ghost
            {
                ActivatePlayer(Ghost);
                changeCameraTarget(Ghost.transform);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2) && isSkeletonActive) // Switch to Skeleton
            {
                ActivatePlayer(Skeleton);
                changeCameraTarget(Skeleton.transform);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3) && isBodyActive) // Switch to Body
            {
                ActivatePlayer(Body);
                changeCameraTarget(Body.transform);

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
            if (goalsWon == goalList.Count)
            {
                if (!beenWon)
                {
                    beenWon = true;
                    disablePlayers();
                    endScreenCanvas.SetActive(true);
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
            endScreenCanvas.SetActive(false);
            for (int i = 0; i < goalList.Count; i++) 
            {
               goalList[i].OnGoalEnter += checkForWin;
            }
            checkIfNull();
            ActivatePlayer(Ghost);
        }

        private void FixedUpdate ()
        {
            if (!beenWon) //if level is not won check stuff
            {
              checkInput();
            }
        }//check for input
    }
}