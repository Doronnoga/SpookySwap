using UnityEngine;
using UnityEngine.InputSystem;
using PlayerMovementScript;

namespace PlayerMovementScript
{
    public class GhostMovement : PlayerMovement
    {
        protected override void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component missing from this GameObject.");
            }

            rb.gravityScale = 0;//make sure ghost is floaty
        }

        protected override void FixedUpdate()
        {
            if (rb != null)
            {
                rb.velocity = new Vector2(moveDirection.x* moveSpeed, moveDirection.y * moveSpeed); // Only move up and down
            }
        }
    }
}
