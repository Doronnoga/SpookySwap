using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace SceneHandlerScript
{
    public class SceneHandler : MonoBehaviour
    {
        [SerializeField]
        private int sceneIndex;
        [SerializeField]
        private int currentScene;
        [SerializeField]
        private int nextScene;

        private void Start()
        {
            currentScene= SceneManager.GetActiveScene().buildIndex;
            nextScene= SceneManager.GetActiveScene().buildIndex +1;
        }
        public void goToStartScene()
        {
            Debug.Log("went to starts scene");
            SceneManager.LoadScene(0);
        }

        public void reloadScene()
        {
            Debug.Log("reloaded scene");
            SceneManager.LoadScene(currentScene);
        }       

        public void loadSpecigicLevel() 
        {
            Debug.Log($"went to specific scene {sceneIndex}");
            SceneManager.LoadScene(sceneIndex);
        }

        public void closeApplication() 
        {
            Debug.Log("Exit");
            Application.Quit();
        }
        public void loadNextScene() 
        {
            Debug.Log($"went to next scene {nextScene}");
            SceneManager.LoadScene(nextScene);
        }
    }
}
