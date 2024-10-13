using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerMovementScript;
using UnityEngine.UIElements;

public class BodyMovment : PlayerMovement
{
    // New vars for push/pull
    private GameObject box;
    private Vector2 interactionOffset;
    [SerializeField]
    private float interactionDistance = 3f;
    [SerializeField]
    public LayerMask boxMask;
    private FixedJoint2D currentJoint = null;


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, interactionDistance, boxMask);

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Inside if 1");
            if (hit.collider != null && hit.collider.CompareTag("Box"))
            {
                GameObject box = hit.collider.gameObject;
                Rigidbody2D boxRb = box.GetComponent<Rigidbody2D>();
                if (boxRb != null)
                {
                    // Check if a joint already exists
                    if (currentJoint == null)
                    {
                        // Add FixedJoint2D if it doesn't exist
                        currentJoint = gameObject.AddComponent<FixedJoint2D>();
                        currentJoint.connectedBody = boxRb;
                        currentJoint.enableCollision = true;
                    }
                }
                else
                {
                    Debug.LogWarning("Interactable object missing Rigidbody2D.");
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            // Remove the joint when E is released
            if (currentJoint != null)
            {
                currentJoint.enableCollision = false;
                currentJoint.connectedBody = null;
                currentJoint = null;
                Debug.Log("Interaction ended.");
            }
        }
    }
}
