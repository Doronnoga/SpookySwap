using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoalClass;
using PlayerMovementScript;

namespace LevelManager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        public string sceneName = "";//name of connected scene

        [SerializeField]
        List<Goal> goalList = new List<Goal>();

        [SerializeField]
        private GameObject Ghost;
        [SerializeField]
        private GameObject Skeleton;
        [SerializeField]
        private GameObject Body;


        [SerializeField]
        private PlayerMovement ghostMovement;
        [SerializeField]
        private PlayerMovement skeletonMovement;
        [SerializeField]
        private PlayerMovement bodyMovement;

        public void checkIfNull()
        {
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

            // compare tag
            if (player.tag.Equals("Ghost"))
            {
                ghostMovement.enabled = true;
            }
            else if (player.tag.Equals("Body"))
            {
                bodyMovement.enabled = true;
            }
            else if (player.tag.Equals("Skeleton"))
            {
                skeletonMovement.enabled = true;
            }
        }

        void Start()
        {
            Debug.Log("Level manager alive");
            checkIfNull();
            ActivatePlayer(Ghost);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) // Switch to Ghost
            {
                ActivatePlayer(Ghost);
                Debug.Log("GHOST ACTIVATED");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) // Switch to Skeleton
            {
                ActivatePlayer(Skeleton);
                Debug.Log("SKELETON ACTIVATED");

            }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) // Switch to Body
            {
                ActivatePlayer(Body);
                Debug.Log("BODY ACTIVATED");

            }
        }
    }
}