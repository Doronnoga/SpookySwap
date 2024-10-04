using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovingPlatform;
using Unity.VisualScripting;
using ButtonClass;

public class MovingPlatformsButton : MovingPlatforms
{
    [Header("Button Controls")]
    [Header("Button")]
    [SerializeField]
    private Button button;
    [SerializeField]
    private bool isActivated = false;

    public void ActivatePlatform()
    {
        isActivated = true;
        Debug.Log("Platform Activated");
    }

    public void DeactivatePlatform()
    {
        isActivated = false;
        Debug.Log("Platform Deactivated");
    }

    protected override void movePlatform()
    {
        base.movePlatform();
    }

    void Start()
    {
        isActivated = false;

        if (button != null)//on starts subscribe methods to event
        {
            button.OnButtonPressed += ActivatePlatform;
            button.OnButtonReleased += DeactivatePlatform;
        }
    }

    void Update()
    {
        if (isActivated)
        {
            movePlatform();
        }
    }
}
