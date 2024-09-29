using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerMovementScript
{
    public abstract class PlayerMovement : MonoBehaviour
    {
        protected PlayerControls controls;
        protected Vector2 moveDirection = Vector2.zero;

        [SerializeField]
        protected Rigidbody2D rb;

        [SerializeField]
        public float moveSpeed = 5f;

        [SerializeField]
        public float jumpForce = 5f;

        private void Awake()
        {
            // Initialize the PlayerControls
            controls = new PlayerControls();

            // Bind the Move action
            controls.Player.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
            controls.Player.Move.canceled += ctx => moveDirection = Vector2.zero;

            // Bind the Jump action
            controls.Player.Jump.performed += ctx => Jump();

            // Bind the Interact action
            controls.Player.Interact.performed += ctx => Interact();
        }//dont touch

        private void OnEnable()
        {
            controls.Player.Enable();
            Debug.Log("Player Controls Enabled");
        }//dont touch

        private void OnDisable()
        {
            controls.Player.Disable();
            Debug.Log("Player Controls Disabled");
        }//dont touch

        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component missing from this GameObject.");
            }
        }//can change between players

        protected virtual void FixedUpdate()
        {
            if (rb != null)
            {
                // Apply movement
                rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y); // Maintain vertical velocity
                Debug.Log("MOVING");
            }
        }//can change between players

        protected virtual void Jump()
        {
            if (rb != null && Mathf.Abs(rb.velocity.y) < 0.01f) // Check if the player is on the ground
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                Debug.Log("Jumped");
            }
        }//only on some players-- should it be here?

        protected virtual void Interact()
        {
            Debug.Log("Interacted");
        }//all cana interact
    }
}
