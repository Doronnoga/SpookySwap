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
        protected float moveSpeed = 5f;

        [SerializeField]
        protected float jumpForce = 5f;

        protected virtual void Awake()
        {
            // Initialize the PlayerControls
            controls = new PlayerControls();

            // Bind the Move action
            controls.Player.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
            controls.Player.Move.canceled += ctx => moveDirection = Vector2.zero;

            // Bind the jump action
            controls.Player.Jump.performed += ctx => Jump();

            // Bind the interact action
            controls.Player.Interact.performed += ctx => Interact();
        }

        protected virtual void OnEnable()
        {
            controls.Player.Enable();
            Debug.Log($"{this.GetType().Name} Controls Enabled");
        }

        protected virtual void OnDisable()
        {
            controls.Player.Disable();
            rb.velocity = Vector2.zero;
            Debug.Log($"{this.GetType().Name} Controls Disabled");
        }

        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component missing from this GameObject.");
            }
        }

        protected virtual void FixedUpdate()
        {
            if (rb != null)
            {
                // Apply movement
                rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y); // Maintain vertical velocity
            }
        }

        protected virtual void Jump()
        {
            if (rb != null && Mathf.Abs(rb.velocity.y) < 0.01f) // Check if the player is on the ground
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                Debug.Log($"{this.GetType().Name} Jumped");
            }
        }

        protected virtual void Interact()
        {
            Debug.Log($"{this.GetType().Name} Interacted");
        }
    }
}
