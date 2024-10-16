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
        public event PlayerActionEvent OnStopPull;
        public event PlayerActionEvent OnSwitch;

        // New vars for push/pull
        protected GameObject box;
        protected Vector2 interactionOffset;
        [SerializeField]
        protected float interactionDistance = 5f;
        [SerializeField]
        public LayerMask boxMask;
        protected FixedJoint2D currentJoint = null;
        protected bool facingRight = true; // Track player orientation

        protected void pushAndPull()
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
                    OnStopPull?.Invoke();
                }
            }
        }

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
                    transform.localScale = new Vector3(1, 1, 1);
                    OnMove?.Invoke();
                }
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    OnMove?.Invoke();
                }
                if (rb.velocity == Vector2.zero) 
                {
                    OnStop?.Invoke();
                }
                if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Alpha3)) 
                {
                    OnSwitch?.Invoke();
                }
            }
        }

        protected virtual void Jump()
        {
            if (rb != null && Mathf.Abs(rb.velocity.y) < 0.01f) // Check if the player is on the ground
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                OnJump?.Invoke();
            }
        }
    }
}
