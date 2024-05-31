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

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, transform.position.y);

        transform.right = direction;
    }
}


