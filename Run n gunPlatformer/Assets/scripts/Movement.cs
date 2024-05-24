using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    public float speed = 0;
    public bool IsDashing = false;
    bool IsGround = false;
    public float jumpheight = 10;
    public float gravityscail = 10;

    [SerializeField]
    public int bulletamount = 1;
    public float bulletspeed = 5;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       Vector2 Vel = rb.velocity;
        Vel.x = Input.GetAxis("Horizontal") * speed;
        rb.velocity = Vel;


        if (Input.GetKeyDown(KeyCode.W) & (IsGround = true))
        {
            rb.AddForce(Vector2.up * jumpheight, ForceMode2D.Impulse);
        }

        if (rb.velocity.y > 0)
        {
            rb.velocity.y = gravityscail;
        }

 
            
            
    }
}
