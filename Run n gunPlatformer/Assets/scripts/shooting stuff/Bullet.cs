using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("Bullet hit the player. Dealing " + damage + " damage.");
                player.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Bullet hit the enemy. Dealing " + damage + " damage.");
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
