using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        List<string> goalList = new List<string>();
        [SerializeField]
        public string sceneName = "";//name of connected scene

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Level manager alive");
        }
    }
}