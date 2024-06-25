using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Enemy
{
    public float moveSpeed = 2f; 
    private bool movingRight = true;
    private void Update()
    {// roept de move funtie aan 
        Move();
    }

    private void Move()
    {
        // laat de enemy voor uit bewegen 
        Vector3 movement = (movingRight ? Vector3.right : Vector3.left) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
    protected override void Flip()
    {
        // Flipt de enemy's sprite halt de funtie uit (de parant script enemy)
        base.Flip();
        movingRight = !movingRight;
    }



    protected override void OnCollisionEnter2D(Collision2D collision)
    {// pakt de funtinalittijd uit de enemy script en voort hem uit
        base.OnCollisionEnter2D(collision);
    }
}