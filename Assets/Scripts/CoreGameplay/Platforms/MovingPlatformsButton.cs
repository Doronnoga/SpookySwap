using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovingPlatform;
using Unity.VisualScripting;

public class MovingPlatformsButton : MovingPlatforms
{
    [Header("Button Controls")]
    private bool isActivated = false;
    [SerializeField]
    private Collider2D buttonCollider;
    [SerializeField]
    protected string intendedTag = "";
    public void ActivatePlatform()
    {
        isActivated = true;
    }
    public void DeactivatePlatform() 
    {
        isActivated = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other = buttonCollider;
        Debug.Log("Enter");

        if (other.CompareTag(intendedTag))
        { 
          ActivatePlatform();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        other = buttonCollider;
        Debug.Log("Exit");
        if (other == buttonCollider && other.CompareTag(intendedTag))
        {
            DeactivatePlatform();
        } 
    }

    protected override void movePlatform()
    {
        base.movePlatform();
    }

    void Start()
    {
        isActivated = false;
        if (buttonCollider == null)
        {
            buttonCollider = GameObject.FindGameObjectWithTag("Button").GetComponent<Collider2D>();
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
