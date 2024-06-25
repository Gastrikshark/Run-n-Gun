using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    public float moveSpeed = 2f; 
    public Transform pointA; 
    public Transform pointB; 
    private bool movingToB = true;

    private void Update()
    {
        Move();
        HandleShooting();
    }

    // onthoud 2 pozieties en gaat konstand heen en weer met die pozietzies
    private void Move()
    {
        Transform targetPoint = movingToB ? pointB : pointA;
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            movingToB = !movingToB;
            Flip();
        }
    }

    // wacht totdat de nextFireTime van de ouder scipt enemy gebeurt en gaat dan pass de shoot funtie aanroepen
    private void HandleShooting()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    
    private void Shoot()
    {
        // creart de bullet onhout de firepoint pozietzie en rotatie en vuurt hem af
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {   // onthoud waar de player is en aimt daar en schiet daar heen
            Vector2 direction = (Player.instance.transform.position - firePoint.position).normalized;
            rb.velocity = direction * bulletSpeed;
        }
    }
}
