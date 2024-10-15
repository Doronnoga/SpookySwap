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

        public delegate void PlayerActionEvent();
        public event PlayerActionEvent OnMove;
        public event PlayerActionEvent OnStop;
        public event PlayerActionEvent OnJump;
        public event PlayerActionEvent OnPush;
        public event PlayerActionEvent OnSwitch;

        protected virtual void Awake()
        {
            controls = new PlayerControls();
            controls.Player.Move.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
            controls.Player.Move.canceled += ctx => moveDirection = Vector2.zero;
            controls.Player.Jump.performed += ctx => Jump();
        }

        protected virtual void OnEnable()
        {
            controls.Player.Enable();
        }

        protected virtual void OnDisable()
        {
            controls.Player.Disable();
            rb.velocity = Vector2.zero;
        }

        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component missing from this GameObject.");
            }
        }

        protected virtual void Update()
        {
            if (rb != null)
            {
                // Apply movement
                rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y); // Maintain vertical velocity
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    Debug.Log("Right arrow");
                    transform.localScale = new Vector3(1, 1, 1);
                    OnMove?.Invoke();
                }
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    Debug.Log("Left arrow");
                    OnMove?.Invoke();
                }
                if (rb.velocity == Vector2.zero) 
                {
                    Debug.Log("Zero movment");
                    OnStop?.Invoke();
                }
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3)) 
                {
                    Debug.Log("Key 123");
                    OnSwitch?.Invoke();
                }
            }
        }

        protected virtual void Jump()
        {
            if (rb != null && Mathf.Abs(rb.velocity.y) < 0.01f) // Check if the player is on the ground
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                Debug.Log("Jump");
                OnJump?.Invoke();
            }
        }
    }
}
