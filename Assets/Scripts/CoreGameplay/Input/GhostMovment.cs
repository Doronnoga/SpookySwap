using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerMovementScript
{
    public class GhostMovement : MonoBehaviour
    {
        private PlayerControls controls;
        private Vector2 moveDirection = Vector2.zero;

        [SerializeField]
        private Rigidbody2D rb;

        [SerializeField]
        private float moveSpeed = 5f;

        private void Awake()
        {
           
            controls = new PlayerControls();

            
            controls.Ghost.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
            controls.Ghost.Move.canceled += ctx => moveDirection = Vector2.zero;
        }

        private void OnEnable()
        {
            controls.Ghost.Enable();
            Debug.Log("Ghost Controls Enabled");
        }

        private void OnDisable()
        {
            controls.Ghost.Disable();
            Debug.Log("Ghost Controls Disabled");
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component missing from this GameObject.");
            }

            rb.gravityScale = 0;//make sure ghost is floaty
        }

        private void FixedUpdate()
        {
            if (rb != null)
            {
                rb.velocity = new Vector2(0, moveDirection.y * moveSpeed); // Only move up and down
                Debug.Log("Ghost MOVING");
            }
        }
    }
}
