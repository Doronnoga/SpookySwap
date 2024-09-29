using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerMovementScript.CoreGameplay
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerControls controls;
        private Vector2 moveInput;

        [SerializeField]
        private float moveSpeed = 5f;
        [SerializeField]
        private float jumpForce = 5f;
        private Rigidbody2D rb;

        private void Awake()
        {
            /// Initialize the PlayerControls
            controls = new PlayerControls();

            // Bind the Move action
            controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
            controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

            // Bind the Jump action
            controls.Player.Jump.performed += ctx => Jump();

            // Bind the Interact action
            controls.Player.Interact.performed += ctx => Interact();
            
        }

        private void OnEnable()
        {
            controls.Player.Enable();
            Debug.Log("Player Controls Enabled");
        }

        private void OnDisable()
        {
            controls.Player.Disable();
            Debug.Log("Player Controls Disabled");
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component missing from this GameObject.");
            }
        }

        private void FixedUpdate()
        {
            if (rb != null)
            {
                // Apply movement
                rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
                Debug.Log($"MOVING with Velocity: {rb.velocity}");
            }
        }

        private void Jump()
        {
            if (rb != null)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                Debug.Log("Jumped");
            }
        }

        private void Interact()
        {
            Debug.Log("Interacted");
        }
    }
}
