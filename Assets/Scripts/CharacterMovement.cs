using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    // ============================== Movement Settings ==============================
    [Header("Movement Settings")]
    [SerializeField] private float baseWalkSpeed = 5f;
    [SerializeField] private float baseRunSpeed = 8f;
    [SerializeField] private float rotationSpeed = 10f;

    // ============================== Jump Settings =================================
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundCheckDistance = 1.1f;
    [SerializeField] public bool canDoubleJump = false;
    private bool hasDoubleJumped = false;

    // ============================== Health Settings ==============================
    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;

    // ============================== Other Variables ==============================
    private Rigidbody rb;
    private Transform cameraTransform;
    private float moveX;
    private float moveZ;
    private bool jumpRequest;
    private Vector3 moveDirection;

    public float speedMultiplier = 1.0f; // For speed power-ups
    public float groundSpeed; // For animation

    public bool IsGrounded =>
        Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, groundCheckDistance);

    private bool IsRunning => Input.GetButton("Run");

    // Expose the private 'hasDoubleJumped' via a public property
    public bool HasDoubleJumped
    {
        get { return hasDoubleJumped; }
    }

    private bool isInTrap = false; // Flag to prevent multiple damage from trap

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        if (Camera.main)
            cameraTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        UIManager.Instance.UpdateHealth(currentHealth);
    }

    private void Update()
    {
        RegisterInput();

        // Detect if player falls off the map
        if (transform.position.y < -10)
        {
            TakeDamage(1);
            Respawn();
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void RegisterInput()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        if (IsGrounded)
        {
            hasDoubleJumped = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpRequest = true;
        }
    }

    private void HandleMovement()
    {
        CalculateMoveDirection();
        HandleJump();
        RotateCharacter();
        MoveCharacter();
    }

    private void CalculateMoveDirection()
    {
        if (!cameraTransform)
        {
            moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        }
        else
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            moveDirection = (forward * moveZ + right * moveX).normalized;
        }
    }

    private void HandleJump()
    {
        if (jumpRequest)
        {
            if (IsGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                hasDoubleJumped = false;
            }
            else if (canDoubleJump && !hasDoubleJumped)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                hasDoubleJumped = true;

                // Play double jump animation or effect (optional)
            }
            jumpRequest = false;
        }
    }

    private void RotateCharacter()
    {
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void MoveCharacter()
    {
        float speed = IsRunning ? baseRunSpeed : baseWalkSpeed;
        groundSpeed = (moveDirection != Vector3.zero) ? speed : 0.0f;

        Vector3 newVelocity = new Vector3(
            moveDirection.x * speed * speedMultiplier,
            rb.velocity.y,
            moveDirection.z * speed * speedMultiplier
        );

        rb.velocity = newVelocity;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Damage taken: " + damage); // Log the damage taken
        currentHealth -= damage;
        UIManager.Instance.UpdateHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.ResetGame();
    }

    private void Respawn()
    {
        transform.position = GameManager.Instance.lastCheckpointPosition;
        rb.velocity = Vector3.zero;
    }

    private IEnumerator TrapDamageCoroutine()
    {
        while (isInTrap)
        {
            TakeDamage(1); // Apply damage every second
            yield return new WaitForSeconds(1f); // Delay for 1 second between damage applications
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trap") && !isInTrap)
        {
            isInTrap = true; // Mark that the player is in the trap
            StartCoroutine(TrapDamageCoroutine()); // Start applying damage over time
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            isInTrap = false; // Reset the flag when leaving the trap
        }
    }

    public void ActivateSpeedBoost(float duration)
    {
        StartCoroutine(SpeedBoostCoroutine(duration));
    }

    public void ActivateJumpBoost(float duration)
    {
        StartCoroutine(JumpBoostCoroutine(duration));
    }

    private IEnumerator SpeedBoostCoroutine(float duration)
    {
        speedMultiplier = 1.5f;
        yield return new WaitForSeconds(duration);
        speedMultiplier = 1.0f;
    }

    private IEnumerator JumpBoostCoroutine(float duration)
    {
        canDoubleJump = true;
        yield return new WaitForSeconds(duration);
        canDoubleJump = false;
    }

    // Add ResetPosition method to reset the character's position
    public void ResetPosition()
    {
        transform.position = GameManager.Instance.lastCheckpointPosition; // Reset to last checkpoint
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero; // Stop movement immediately
        }
    }
}
