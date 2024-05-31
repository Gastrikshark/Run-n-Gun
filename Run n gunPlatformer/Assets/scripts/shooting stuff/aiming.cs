using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiming: MonoBehaviour
{


    void Start()
    {
       
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        GameObject direction = new Vector2(mousePosition.z - transform.position.x, transform.position.y);

        transform.up = direction;
    }
}


