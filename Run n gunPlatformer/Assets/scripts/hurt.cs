using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurt : MonoBehaviour
{
    public float damage = 0.1f;
    Player player;
    public float damageCooldownTime;
    private float currentDamageCooldownTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDamageCooldownTime <= 0f)
        {
            player.health -= damage;
            currentDamageCooldownTime = damageCooldownTime;
            Debug.Log("Took damage");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("trap hit the player. Dealing " + damage + " damage.");
                player.TakeDamage(damage);
            }
        }
    }
}
