using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerMovementScript;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class BodyMovment : PlayerMovement
{
    // New vars for push/pull
    private GameObject box;
    private Vector2 interactionOffset;
    [SerializeField]
    private float interactionDistance = 5f;
    [SerializeField]
    public LayerMask boxMask;
    private FixedJoint2D currentJoint = null;
    private bool facingRight = true; // Track player orientation

    public event PlayerActionEvent OnPush;
    public event PlayerActionEvent OnStop;


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(direction * interactionDistance));
    }
    private void pushAndPull()
    {
        // Update facing direction
        if (moveDirection.x > 0)
            facingRight = true;
        else if (moveDirection.x < 0)
            facingRight = false;
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, interactionDistance, boxMask);

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("KeyIsDown");
            if (hit.collider != null && hit.collider.CompareTag("Pushable"))
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
                        OnPush?.Invoke();
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
                Debug.Log("KeyIsUp");
                Destroy(currentJoint);
                currentJoint.connectedBody = null;
                currentJoint.enableCollision = false;
                OnStop?.Invoke();
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        pushAndPull();
    }
}
