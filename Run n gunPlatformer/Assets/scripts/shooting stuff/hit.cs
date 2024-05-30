using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour
{

    public GameObject Bullet;
    public GameObject ShootingPoint;
    public bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot)
        {
            Debug.Log("shoot");
            shoot();
            
        }
    }

    void shoot()
    {
        if (!canShoot)
            return;

        GameObject si = Instantiate(Bullet, ShootingPoint);
        si.transform.parent = null;
    }
}
