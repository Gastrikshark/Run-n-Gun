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
            // geeft de kogel zijn snelhijd
            rb.velocity = firePoint.right * bulletSpeed;
        }
    }
}