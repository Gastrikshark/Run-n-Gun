using System;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;


    private float nextFireTime = 0f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        // Creart de kogel en waar die geshoten moet worden
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // pakt de Rigidbody2D component van de kogel en onthoudt zijn snelheid
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // berkent de richting en de bullet vandaan komt gebaseerd op waar de player muis is 
            Vector2 aimDirection = GetAimDirection();

            // Set the bullet's velocity based on the aim direction
            rb.velocity = aimDirection.normalized * bulletSpeed;
        }
    }

    private Vector2 GetAimDirection()
    {
        // kijkt waar welke righting de player kijkt volgens de Movement script
        Movement movement = GetComponent<Movement>();
        Vector2 facingDirection = movement.facingRight ? Vector2.right : -Vector2.right;

        // berekent welke rotatie de shooting thing heeft gebaseerd op de facing direction van de movement script en past op de juiste manier aan 
        Vector2 aimDirection = firePoint.right * facingDirection.x + firePoint.up * aimY;

        return aimDirection;
    }

    private float aimY = 0f; //dit veranderd gebaseerd op waar de muis is 
}
    
