using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    public float moveSpeed = 2f; // Speed of the enemy movement
    public Transform pointA; // Starting point
    public Transform pointB; // Ending point
    private bool movingToB = true;

    private void Update()
    {
        Move();
        HandleShooting();
    }


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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (Player.instance.transform.position - firePoint.position).normalized;
            rb.velocity = direction * bulletSpeed;
        }
    }
}
