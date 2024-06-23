using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthAmount = 20f; // Amount of health the pickup restores

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Heal(healthAmount);
                Destroy(gameObject); 
            }
        }
    }
}
