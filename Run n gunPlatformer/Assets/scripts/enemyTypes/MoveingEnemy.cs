using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Enemy
{
    public float moveSpeed = 2f; 
    private bool movingRight = true;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Move the enemy in the current direction
        Vector3 movement = (movingRight ? Vector3.right : Vector3.left) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
    private void Flip()
    {
        // Flip the enemy's sprite
        movingRight = !movingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with a collider tagged as "TurnPoint"
        if (collision.collider.CompareTag("TurnPoint"))
        {
            Flip();
        }
    }
}