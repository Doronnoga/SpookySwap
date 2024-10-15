using System.Buffers.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerMovementScript;

namespace GhostMovementScript
{
    public class GhostMovement : PlayerMovement
    {
        [SerializeField]
        protected float acceleration = 5f; // Control how quickly the ghost speeds up
        [SerializeField]
        protected float deceleration = 3f; // Control how quickly the ghost slows down
        [SerializeField]
        public delegate void GhostMovmentAction();
        public event GhostMovmentAction OnMove;
        public event GhostMovmentAction OnStop;
        public event GhostMovmentAction OnSwitch;

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
                    OnStop?.Invoke();
                }

                // Trigger events based on input
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    OnMove?.Invoke();
                }
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    OnMove?.Invoke();
                }
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    OnMove?.Invoke();
                }
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    OnMove?.Invoke();
                }
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
                {
                    Debug.Log("Key 123 GHOST");
                    OnSwitch?.Invoke();
                }
            }
        }

        protected override void Jump()
        {
            Debug.Log("Ghosts don't jump!");
        }
    }
}
