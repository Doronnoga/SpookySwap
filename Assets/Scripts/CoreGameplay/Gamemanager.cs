using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagerClass;
using System;

namespace GameManager
{
    public class Gamemanager : MonoBehaviour
    {
        public static Gamemanager instance;
        [SerializeField]
        Dictionary<LevelManager, string> levelDic = new Dictionary<LevelManager, string>();

        //loading method

        //track levels

        //level loading

        //save\load..?

        //bindevents

        void Start()
        {
            //BIND THE EVENTS
        }

        private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
        void Update()
        {

        }
    }
}