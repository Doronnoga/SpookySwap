using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerMovementScript
{
    public class GhostMovement : PlayerMovement
    {
        [SerializeField]
        protected float acceleration = 5f; // Control how quickly the ghost speeds up
        [SerializeField]
        protected float deceleration = 3f; // Control how quickly the ghost slows down
        [SerializeField]
        Animator animator;

        protected override void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            animator.SetBool("Moving", false);

            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component missing from ghost.");
            }

            rb.gravityScale = 0;
        }

        protected override void Update()
        {
            if (rb != null)
            {
                // Calculate target velocity based on input
                Vector2 targetVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

                // Gradually accelerate towards the target velocity
                rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, acceleration * Time.fixedDeltaTime);

                // Apply deceleration if no input is detected
                if (moveDirection == Vector2.zero)
                {
                    rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
                    animator.SetBool("Moving", false);
                }
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    animator.SetBool("Moving", true);

                }
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    animator.SetBool("Moving", true);
                }
            }
        }

        protected override void Jump()
        {
            Debug.Log("Ghosts don't jump!");
        }
    }
}
