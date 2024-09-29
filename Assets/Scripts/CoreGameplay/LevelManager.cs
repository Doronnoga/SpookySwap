using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoalClass;

namespace LevelManager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        List<Goal> goalList = new List<Goal>();
        [SerializeField]
        public string sceneName = "";//name of connected scene

        public void checkIfNull()
        {
            if (goalList.Contains(null))
            {
                Debug.Log("THE LEVEL MANAGER IS MISSING A GOAL: please give it a goal.");
            }
            else { Debug.Log("Levelmanager Goal list okay"); }
        }
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Level manager alive");
                checkIfNull();
        }
    }
}