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

    public bool isSecondaryGunUnlocked = false;
    private int secondaryGunCost = 50;

    private void Start()
    {
        // geeft de player standard de primary gun aka de first gun
        EquipGun(primaryGun, primaryBulletPrefab, primaryFirePoint, primaryBulletSpeed, primaryFireRate);
    }

    private void Update()
    {
        // roopt de gun switch funtie aan 
        HandleGunSwitching();

        // als de player op de linker muis knop drukt dan vuurt hij de kogel af
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + activeFireRate;
        }
        // als de player op b drukt en de 2de gun is nog niet ge unlocked dan
        // probeert hij hem te kopen 
        if (Input.GetKeyDown(KeyCode.B) && !isSecondaryGunUnlocked)
        {
            AttemptToUnlockSecondaryGun();
        }
    }

    private void HandleGunSwitching()
    {// als de player op 1 drukt word de eerste gun gepakt en als de player op 2 drukt
     // word de tweede gun gepakt
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipGun(primaryGun, primaryBulletPrefab, primaryFirePoint, primaryBulletSpeed, primaryFireRate);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && isSecondaryGunUnlocked)
        {
            EquipGun(secondaryGun, secondaryBulletPrefab, secondaryFirePoint, secondaryBulletSpeed, secondaryFireRate);
        }
    }
    // set een bepalde gun aan met alle dingen die er bij hooren 
    private void EquipGun(GameObject gun, GameObject bulletPrefab, Transform firePoint, float bulletSpeed, float fireRate)
    {// als er een gun niet active is word hij uitgezet
        if (activeGun != null)
        {
            activeGun.SetActive(false);
        }
        // hier wordt alles goed gezet 
        activeGun = gun;
        activeBulletPrefab = bulletPrefab;
        activeFirePoint = firePoint;
        activeBulletSpeed = bulletSpeed;
        activeFireRate = fireRate;
        // en hier zet hij hem weer aan
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
            // berekent de richting en de bullet vandaan komt gebaseerd op waar de player muis is 
            Vector2 aimDirection = GetAimDirection();

            // geeft de bullet's snelheid aangebaserd op waar de player aimed
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
    
    private void AttemptToUnlockSecondaryGun()
    {// kijkt of de player genoeg coins heeft en geeft de player de tweede gun
        if (Player.instance.coins >= secondaryGunCost)
        {
            // dan worden de coins afgenomen en de ui word geupdate
            Player.instance.coins -= secondaryGunCost;
            UIManager.instance.UpdateCoins(Player.instance.coins);
            // de tweede gun kan dan gekozen worden
            isSecondaryGunUnlocked = true;
            Debug.Log("Secondary gun unlocked!");
        }
        else
        {// als de player niet genoeg coins heeft gebeurt er dit 
            Debug.Log("Not enough coins to unlock the secondary gun.");
        }
    }

}

