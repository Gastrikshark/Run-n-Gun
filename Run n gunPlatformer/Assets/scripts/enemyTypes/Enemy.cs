using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health Settings")]
    public float health = 25f;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireRate = 1f;

    [Header("Pickup Settings")]
    public GameObject healthPickupPrefab;
    public GameObject smallHealthPickupPrefab;
    public float healthPickupChance = 0.2f;
    public float smallHealthPickupChance = 0.6f; 

    protected float nextFireTime = 0f;
    private Transform player;
    Player playerCharacter;

    void Start()
    {
        //zoekt naar een object met de tag Player
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //ik weet niet echt wat voor zinvols ik hier kan neerzeten behalve dat hij de HandleShooting funtie aanroept
    void Update()
    {
        HandleShooting();
    }

    void HandleShooting()
    {
        //zoekt naar de player en herlaad om dan weer op de player te schieten
        if (player != null && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;

        }
    }

    void Shoot()
    {
        // zoekt naar het verschil tussen de fire point en de player's pozietzi,
        Vector2 direction = (player.position - firePoint.position).normalized;
        // Instantiate a new bullet at the fire point's position with no rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        // zoekt naar de Rigidbody2D die op de bullet staat 
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            //dan pakt hij de snelhied van de Rigdbody2d plus ruimte tussen direction keer de bulletspeed
            rb.velocity = direction * bulletSpeed;
        }
    }

    // als health 0 of lager is word de die funtie aangeroepen
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        //het geeft de player coins en verwijderd de enemy as de funtie wordt aangeroepen  
        int coinsToAdd = Random.Range(2, 6);
        Player.instance.AddCoins(coinsToAdd);

        //  spawned de health pickups gebaseerd op de kansen beschreven in de 2 floats van pickupsettings
        float randomValue = Random.value;
        if (randomValue < healthPickupChance)
        {
            Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
        }
        else if (randomValue < healthPickupChance + smallHealthPickupChance)
        {
            Instantiate(smallHealthPickupPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);


    }

    protected virtual void Flip()
    {
        // laat de enemy omdraien
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;

    }

    //dit roopt de flip funtie aan als de enemy in contact komt met een object met de tag"TurnPoint"
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("TurnPoint"))
        {
            Flip();
        }
    }
}
