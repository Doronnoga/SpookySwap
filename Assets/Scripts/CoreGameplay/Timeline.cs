using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Timeline : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector director;

    private void Start()
    {
        director = GetComponent<PlayableDirector>();
        director.stopped += DirectorStopped;
    }
    public void PlayTimeline()
    {
        director.Play();
    }

    private void DirectorStopped(PlayableDirector obj)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnDestroy()
    {
        director.stopped -= DirectorStopped;
    }
}
