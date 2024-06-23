using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour
{
    public float dashSpeed = 10f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 1f;

    private bool isDashing = false;
    private float dashTimer = 0f;

    private Movement movement;

    private bool isCooldown = false;
    private float cooldownTimer = 0f;

    void Start()
    {
        movement = GetComponent<Movement>();
        if (movement == null)
        {
            Debug.LogError("Movement component not found!");
        }
    }

    void Update()
    {
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                EndDash();
            }
        }

        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isCooldown = false;
            }
        }
    }

    public void PerformDash()
    {
        if (isDashing || isCooldown)
        {
            return;
        }

        isDashing = true;
        isCooldown = true;
        cooldownTimer = dashCooldown;

        float originalSpeed = movement.speed;
        movement.speed = dashSpeed;

        Vector2 dashDirection = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;
        movement.rb.velocity = dashDirection * dashSpeed;

        dashTimer = dashDuration;

        StartCoroutine(ResetMovementSpeed(originalSpeed, dashDuration)); 
    }

    IEnumerator ResetMovementSpeed(float originalSpeed, float delay) 
    {
        yield return new WaitForSeconds(delay);

        movement.speed = originalSpeed;

        EndDash();
    }

    void EndDash()
    {
        isDashing = false;
        movement.rb.velocity = Vector2.zero;
    }
}