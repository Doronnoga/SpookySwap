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
    [SerializeField]
    private bool holdToActivate = true;
    [SerializeField]
    private bool oneTimeMovment = true;

    public void ActivatePlatform()
    {
        isActivated = true;
        Debug.Log("Platform Activated");
    }

    public void DeactivatePlatform()
    {
        if (holdToActivate)
        {
            isActivated = false;
            Debug.Log("Platform Deactivated");
        }
        Debug.Log("Platform not on hold");
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
            if (oneTimeMovment)
            {
                float normalizedTime = elapsedTime / duration;
                float curveValue = speedCurve.Evaluate(normalizedTime);

                if (movingToEnd)
                {
                    platform.position = Vector2.Lerp(start.position, end.position, curveValue);
                }
                else
                {
                    platform.position = Vector2.Lerp(end.position, start.position, curveValue);
                }

                elapsedTime += Time.deltaTime;
            }

            else
            {
                movePlatform();
            }
        }
    }
}

