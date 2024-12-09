using UnityEngine;
using UnityEngine.InputSystem;

public class NewMover : MonoBehaviour
{
    Rigidbody2D rb;
    AnimationController animator;
    [SerializeField] float moveSpeed = 3.0f;
    [SerializeField] float runMultiplier = 1.5f;
    [SerializeField] float diagonalFactor = 0.7f;
    [SerializeField] InputAction playerMovement;
    [SerializeField] InputAction Run;

    Vector2 moveDirection = Vector2.zero;
    Vector2 lastPureDirection = Vector2.zero;

    void Start()
    {
        animator = GetComponent<AnimationController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        playerMovement.Enable();
        Run.Enable();
    }

    void OnDisable()
    {
        playerMovement.Disable();
        Run.Disable();
    }

    void Update()
    {
        moveDirection = playerMovement.ReadValue<Vector2>();
        float currentSpeed = moveSpeed;
        bool isRunning = Run.IsPressed();

        if (isRunning)
        {
            currentSpeed *= runMultiplier;
        }

        // Normalize diagonal movement if needed
        if (moveDirection.x != 0 && moveDirection.y != 0)
        {
            rb.linearVelocity = new Vector2(
                moveDirection.x * currentSpeed * diagonalFactor,
                moveDirection.y * currentSpeed
            );
        }
        else
        {
            rb.linearVelocity = new Vector2(
                moveDirection.x * currentSpeed,
                moveDirection.y * currentSpeed
            );
        }

        // Determine if there's an immediate pure direction change
        Vector2 currentPureDirection = GetPureDirection(moveDirection);
        bool resetAnimation = currentPureDirection != Vector2.zero && currentPureDirection != lastPureDirection;

        // Update the animation controller
        animator.HandleMovement(moveDirection.x, moveDirection.y, resetAnimation);

        // Update last pure direction if a pure movement is detected
        if (currentPureDirection != Vector2.zero)
        {
            lastPureDirection = currentPureDirection;
        }
    }

    private Vector2 GetPureDirection(Vector2 direction)
    {
        // Return a pure direction (e.g., up, down, left, right)
        if (direction.x != 0 && direction.y == 0)
        {
            return new Vector2(Mathf.Sign(direction.x), 0); // Pure horizontal
        }
        else if (direction.y != 0 && direction.x == 0)
        {
            return new Vector2(0, Mathf.Sign(direction.y)); // Pure vertical
        }

        return Vector2.zero; // Not a pure direction
    }
}