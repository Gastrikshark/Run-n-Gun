using System;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [Header("Primary Gun Settings")]
    public GameObject primaryGun;
    public GameObject primaryBulletPrefab;
    public Transform primaryFirePoint;
    public float primaryBulletSpeed = 10f;
    public float primaryFireRate = 0.5f;

    [Header("Secondary Gun Settings")]
    public GameObject secondaryGun;
    public GameObject secondaryBulletPrefab;
    public Transform secondaryFirePoint;
    public float secondaryBulletSpeed = 8f;
    public float secondaryFireRate = 0.7f;

    private GameObject activeGun;
    private GameObject activeBulletPrefab;
    private Transform activeFirePoint;
    private float activeBulletSpeed;
    private float activeFireRate;
    private float nextFireTime = 0f;

    private void Start()
    {
        EquipGun(primaryGun, primaryBulletPrefab, primaryFirePoint, primaryBulletSpeed, primaryFireRate);
    }

    private void Update()
    {
        HandleGunSwitching();
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + activeFireRate;
        }
    }

    private void HandleGunSwitching()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipGun(primaryGun, primaryBulletPrefab, primaryFirePoint, primaryBulletSpeed, primaryFireRate);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipGun(secondaryGun, secondaryBulletPrefab, secondaryFirePoint, secondaryBulletSpeed, secondaryFireRate);
        }
    }

    private void EquipGun(GameObject gun, GameObject bulletPrefab, Transform firePoint, float bulletSpeed, float fireRate)
    {
        if (activeGun != null)
        {
            activeGun.SetActive(false);
        }

        activeGun = gun;
        activeBulletPrefab = bulletPrefab;
        activeFirePoint = firePoint;
        activeBulletSpeed = bulletSpeed;
        activeFireRate = fireRate;

        activeGun.SetActive(true);
    }


    private void Shoot()
    {
        // Creart de kogel en waar die geshoten moet worden
        GameObject bullet = Instantiate(activeBulletPrefab, activeFirePoint.position, activeFirePoint.rotation);

        // pakt de Rigidbody2D component van de kogel en onthoudt zijn snelheid
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // berkent de richting en de bullet vandaan komt gebaseerd op waar de player muis is 
            Vector2 aimDirection = GetAimDirection();

            // Set the bullet's velocity based on the aim direction
            rb.velocity = aimDirection.normalized * activeBulletSpeed;
        }
    }

    private Vector2 GetAimDirection()
    {
        // kijkt waar welke righting de player kijkt volgens de Movement script
        Movement movement = GetComponent<Movement>();
        Vector2 facingDirection = movement.facingRight ? Vector2.right : -Vector2.right;

        // berekent welke rotatie de shooting thing heeft gebaseerd op de facing direction van de movement script en past op de juiste manier aan 
        Vector2 aimDirection = activeFirePoint.right * facingDirection.x + activeFirePoint.up * aimY;

        return aimDirection;
    }

    private float aimY = 0f; //dit veranderd gebaseerd op waar de muis is 
}
    
