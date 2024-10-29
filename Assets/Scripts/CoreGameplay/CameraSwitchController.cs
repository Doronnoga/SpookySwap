using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagerClass;
using UnityEngine.SceneManagement;

public class CameraSwitchController : MonoBehaviour
{
    [Header("Camera\n")]
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;
   
    [Header("PLAYERS\n")]
    [SerializeField]
    private GameObject Ghost;
    [SerializeField]
    private GameObject Skeleton;
    [SerializeField]
    private GameObject Body;
    [SerializeField]
    private bool isBodyActive;
    [SerializeField]
    private bool isSkeletonActive;

    private void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) // Switch to Ghost
        {
            changeCameraTarget(Ghost.transform);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) // Switch to Skeleton
        {
            if (isSkeletonActive)
            {
                changeCameraTarget(Skeleton.transform);
            }
            else
            {
                Debug.Log("SkeletonIsn'tactive");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3) && isBodyActive) // Switch to Body
        {
            if (isBodyActive)
            {
                changeCameraTarget(Body.transform);
            }
            else
            {
                Debug.Log("BodyIsntActive");
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

    void Start()
    {
        if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1 && isBodyActive) //checking of it's the credit scene
        {
            changeCameraTarget(Body.transform);
        }
        else
        {
            changeCameraTarget(Ghost.transform);
        }
    }

    void Update()
    {
        checkInput();
    }
}
