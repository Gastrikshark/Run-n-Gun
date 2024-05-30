using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] public float speed = 15;
    [SerializeField] private float jumpHeight = 225;
    

    [Header("Shooting Settings")]
    GameObject Bullet;  
    [SerializeField] private int bulletAmount = 1;
    [SerializeField] private float bulletSpeed = 5;

    [Header("Components")]
    [SerializeField] private GroundChecker groundChecker;
    private Dash dash;

    public Rigidbody2D rb { get; private set; }

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
        velocity.x = Input.GetAxis("Horizontal") * speed;
        rb.velocity = velocity;
    }

    private void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && groundChecker.IsGround)
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
    }

    private void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            rb.AddForce(Vector2.right * jumpHeight, ForceMode2D.Impulse);
        }
    }
}

