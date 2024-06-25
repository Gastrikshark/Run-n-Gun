using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public float lifeTime = 2f;
    SoundManager audio;


    private void Awake()
    {// pakt de component soundmanager gebaserd op een object dat de tag Audio heeft
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

    void Start()
    {// vernietegt de bullet as de livetime op 2f is
        Destroy(gameObject, lifeTime);
    }



    void OnTriggerEnter2D(Collider2D collision)
    {// hij speelt hier de sound effect af
        audio.Playsoundeffect(audio.Bullet);
        // als de bullet in aanraking komt met de player doet hij damage op de player en vernietight hij zich self
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);             
                Destroy(gameObject);
            }
        }
        // als de bullet in aanraking komt met de player doet hij damage op de enemy en vernietight hij zich self
        else if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);   
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }

    }
}

