using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurt : MonoBehaviour
{
    public float damage = 0.1f;
    Player player;
    public float damageCooldownTime;
    private float currentDamageCooldownTime;
    void Update()
    {// verlacht de currentDamageCooldownTime float
        if (currentDamageCooldownTime <= 0f)
        {// halt schade bij de player af en reset de currentDamageCooldownTime
            player.health -= damage;
            currentDamageCooldownTime = damageCooldownTime;
            Debug.Log("Took damage");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {// als de player er instaat krijgt de player scade
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
