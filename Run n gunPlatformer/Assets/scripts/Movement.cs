using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 15; // Made public for accessibility

    [SerializeField] private float jumpHeight = 225;
    [SerializeField] private float gravityScale = 10;

    [Header("Components")]
    [SerializeField] private GroundChecker groundChecker;
    private Dash dash;

    public Rigidbody2D rb { get; private set; }

    private bool facingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dash = GetComponent<Dash>();
    }

    private void Update()
    {
        UpdateMovement();
        CheckJump();
    }

    private void UpdateMovement()
    {
        Vector2 velocity = rb.velocity;
        float moveInput = Input.GetAxis("Horizontal");
        velocity.x = moveInput * speed;
        rb.velocity = velocity;

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    private void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && groundChecker.IsGround)
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;

        // Flip the fire point direction if you have one
        Transform firePoint = GetComponent<Shooting>().firePoint;
        Vector3 firePointScaler = firePoint.localScale;
        firePointScaler.x *= -1;
        firePoint.localScale = firePointScaler;
    }
}
