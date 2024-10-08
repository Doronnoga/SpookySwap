using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManager;

namespace GameManager
{
    public class Gamemanager : MonoBehaviour
    {
        public static Gamemanager instance;

        Dictionary<string, bool> levelDictionary = new Dictionary<string, bool> ();//dictionary for levels- maybe change to hold level manager instead of string

        void Start()
        {

        }

        void Update()
        {

        }
    }
}