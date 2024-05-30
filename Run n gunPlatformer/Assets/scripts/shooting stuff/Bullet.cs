using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float damage = 5;
    float bulletspeed = 5;

    Enemy enemy;
    ShootingAction shooting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right*transform.localScale.x*bulletspeed*Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"));
        {
            //if (other.GetComponent<ShootingAction>())
            //   other.GetComponent<ShootingAction>().Action();
            
        }

        Destroy(gameObject);
    }
}
