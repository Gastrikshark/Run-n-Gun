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
        // pakt de movement script
        movement = GetComponent<Movement>();
    }

    void Update()
    {// kijkt of de player aan het dashen is 
        if (isDashing)
        {// telt af van de dash timer
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                EndDash();
            }
        }
        // als de dash's cooldown true is 
        if (isCooldown)
        {// telt af van de cooldown timer
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isCooldown = false;
            }
        }
    }

    public void PerformDash()
    { // als de player all aan het dashen is of de cooldown is active
        if (isDashing || isCooldown)
        {
            // doet hij niks
            return;
        }
        // hier zet hij de 2 dash bools op true 
        isDashing = true;
        isCooldown = true;
        // en zet de cooldown timer op de dashcooldown
        cooldownTimer = dashCooldown;
        
        float originalSpeed = movement.speed;
        movement.speed = dashSpeed;
        // doet de dash richting in een horizontale richting 
        Vector2 dashDirection = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;
        movement.rb.velocity = dashDirection * dashSpeed;

        dashTimer = dashDuration;
        
        StartCoroutine(ResetMovementSpeed(originalSpeed, dashDuration)); 
    }

    IEnumerator ResetMovementSpeed(float originalSpeed, float delay)
    {// hier word de movement speed terug gezet als de orsinele movement speed
        yield return new WaitForSeconds(delay);
        movement.speed = originalSpeed;
        // en dan eindeght hij de dash
        EndDash();
    }

    void EndDash()
    {// de dash wordt op false gezet en de movement gaat weer terug naar normal
        isDashing = false;
        movement.rb.velocity = Vector2.zero;
    }
}