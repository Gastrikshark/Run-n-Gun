using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 100f;
    Bullet Bullet;

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
        Debug.Log("Player Died");
        Destroy(gameObject);
    }
}
