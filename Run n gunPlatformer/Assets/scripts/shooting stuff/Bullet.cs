using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 5;
    public float bulletspeed = 5;
    public float lifeTime = 2f;

    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right* bulletspeed *Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"));
        {
            //enemy.health -= damage;
        }

        Destroy(gameObject);
    }
}
