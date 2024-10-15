using System.Buffers.Text;
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

        public event PlayerActionEvent OnW;
        public event PlayerActionEvent OnS;
        public event PlayerActionEvent OnD;
        public event PlayerActionEvent OnA;

        protected override void Start()
        {
            rb = GetComponent<Rigidbody2D>();

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
                Vector2 targetVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
                rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, acceleration * Time.fixedDeltaTime);

                if (moveDirection == Vector2.zero)
                {
                    rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
                }

                // Trigger events based on input
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    OnD?.Invoke(); // Use null conditional operator to avoid null ref exceptions
                }
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    OnA?.Invoke();
                }
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    OnS?.Invoke();
                }
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    OnW?.Invoke();
                }
            }
        }

        protected override void Jump()
        {
            Debug.Log("Ghosts don't jump!");
        }
    }
}
