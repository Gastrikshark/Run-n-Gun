using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 0.3f;
    public float dashForce = 300f;

    private bool isDashing = false;
    private float dashTimer = 0f;

    private Movement movement;
    private GroundChecker groundChecker;

    private bool isCooldown = false;
    private float cooldownTimer = 0f;

    private void Start()
    {
        InitializeComponents();
    }

    private void Update()
    {
        CheckDashInput();
        UpdateDashTimer();
        UpdateCooldownTimer();
    }

    private void InitializeComponents()
    {
        movement = GetComponent<Movement>();
        if (movement == null)
        {
            Debug.LogError("Movement component not found!");
        }
    }

    private void CheckDashInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isDashing && !isCooldown)
        {
            Debug.Log("Dash key pressed!");
            StartDash();
        }
    }

    private void UpdateDashTimer()
    {
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                Debug.Log("Dash ended!");
                EndDash();
            }
        }
    }

    private void UpdateCooldownTimer()
    {
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                Debug.Log("Cooldown ended!");
                isCooldown = false;
            }
        }
    }

    private void StartDash()
    {
        if (isDashing || isCooldown) // Check if the player is already dashing or in cooldown
        {
            return; // Exit the method if the player cannot dash
        }

        Debug.Log("Starting dash!");
        isDashing = true;
        isCooldown = true; // Start the cooldown
        cooldownTimer = dashCooldown; // Set the cooldown timer

        // Save the current movement speed
        float originalSpeed = movement.speed;

        // Set the movement speed to the dash speed
        movement.speed = dashSpeed;

        // Calculate the direction of the dash based on player input (horizontal axis)
        Vector2 dashDirection = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;

        // Apply the dash velocity to the Rigidbody2D component
        movement.rb.velocity = dashDirection * dashSpeed;

        // Start the dash timer
        dashTimer = dashDuration;

        // Wait for the dash duration and then return the movement speed to normal
        StartCoroutine(ResetMovementSpeed(originalSpeed, dashDuration));
    }

    private IEnumerator ResetMovementSpeed(float originalSpeed, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Set the movement speed back to its original value
        movement.speed = originalSpeed;

        // End the dash
        EndDash();
    }

    private void EndDash()
    {
        Debug.Log("Ending dash!");
        isDashing = false;
        movement.rb.velocity = Vector2.zero;
    }
}