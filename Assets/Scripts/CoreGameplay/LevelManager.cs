using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoalClass;
using PlayerMovementScript;
using UnityEngine.UI;
using Cinemachine;


namespace LevelManager
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Scene name\n")]
        [SerializeField] 
        private CinemachineVirtualCamera virtualCamera; [SerializeField]
        public string sceneName = "";//name of connected scene

        [Header("GOALS\n")]
        [SerializeField]
        List<Goal> goalList = new List<Goal>();
        [SerializeField]
        public bool beenWon = false;

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
            else { Debug.Log("Levelmanager Goal list okay"); }
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
            else { Debug.Log("Levelmanager players all here okay"); }
        }

        private void ActivatePlayer(GameObject player)
        {
            //turn all off
            ghostMovement.enabled = false;
            bodyMovement.enabled = false;
            skeletonMovement.enabled = false;
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
            if (Input.GetKeyDown(KeyCode.Alpha1)) // Switch to Ghost
            {
                ActivatePlayer(Ghost);
                changeCameraTarget(Ghost.transform);
                Debug.Log("GHOST ACTIVATED");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && isSkeletonActive) // Switch to Skeleton
            {
                ActivatePlayer(Skeleton);
                changeCameraTarget(Skeleton.transform);
                Debug.Log("SKELETON ACTIVATED");

            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && isBodyActive) // Switch to Body
            {
                ActivatePlayer(Body);
                changeCameraTarget(Body.transform);
                Debug.Log("BODY ACTIVATED");

            }
        }

        private void checkForWin()
        {
            int goalsWon = 0;

            // count how many been won
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
                    Debug.Log("LEVEL WON.");
                    disablePlayers();
                    endScreenCanvas.SetActive(true);

                    // Enter Win event here- canvas or whatever
                    // SceneManager.LoadScene("NextSceneName");
                }
            }
            else
            {
                if (beenWon) // Reset 
                {
                    beenWon = false;
                    Debug.Log($"KEEP GOING. Goals won: {goalsWon}/{goalList.Count}");
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
            Debug.Log("Level manager alive");
            endScreenCanvas.SetActive(false);
            checkIfNull();
            ActivatePlayer(Ghost);
        }

        private void Update ()
        {
            if (!beenWon) //if level is not won check stuff
            {
              checkInput();
              checkForWin();
            }
        }
    }
}