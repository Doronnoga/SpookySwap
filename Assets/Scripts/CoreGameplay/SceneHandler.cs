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
            SceneManager.LoadScene(0);
        }

        public void reloadScene()
        {
            SceneManager.LoadScene(currentScene);
        }       

        public void loadSpecigicLevel() 
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void closeApplication() 
        {
            Application.Quit();
        }
        public void loadNextScene() 
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
