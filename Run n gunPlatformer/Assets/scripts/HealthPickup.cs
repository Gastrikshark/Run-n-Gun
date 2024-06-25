using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthAmount = 20f; 
    // als de player in aanraking komt met een object met deze script krijt hij 20 health en
    // wordt het object verwoest
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
