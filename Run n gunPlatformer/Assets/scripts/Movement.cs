using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 15;
    public float jumpHeight = 150;
    public float gravityScale = 1;

    [Header("Components")]
    [SerializeField] private GroundChecker groundChecker;
    private Dash dash;

    SoundManager audio;
    
    public Rigidbody2D rb { get; private set; }

    public bool facingRight = true;
    private float moveInput;

    private bool isJumping = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dash = GetComponent<Dash>();
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

    private void Update()
    {
        {
            Vector2 vel = rb.velocity;
            vel.x = moveInput * speed;
            rb.velocity = vel;

            if (moveInput > 0 && !facingRight)
            {
                Flip();
            }
            else if (moveInput < 0 && facingRight)
            {
                Flip();
            }

            if (rb.velocity.y > 0)
            {
                vel.y = gravityScale;
            }
        }
    }

    public void SetMovementInput(float input)
    {
        moveInput = input;
    }

    public void Jump()
    {
        if (groundChecker.IsGround && !isJumping) 
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            isJumping = true;
            // hij speelt hier de sound effect af
            audio.Playsoundeffect(audio.Jump);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false; 
        }
    }

    public void Dash()
    {
        if (dash != null)
        {
            dash.PerformDash();
        }
    }

    private void Flip()
    {
        // zorgt er voor dat de player naar achter kan kijken
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;

        // Flipt de gun op de goede pozietzie
        Transform gun = transform.Find("Gun");
        if (gun != null)
        {
            Vector3 gunScale = gun.localScale;
            gunScale.x *= -1;
            gun.localScale = gunScale;
        }
    }
}