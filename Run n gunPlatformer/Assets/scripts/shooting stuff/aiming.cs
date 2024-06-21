using UnityEngine;

public class Aiming : MonoBehaviour
{
    private Transform playerTransform;
    public GameObject bulletPrefab; // Assign your bullet prefab in the Inspector
    public float bulletSpeed = 10f; // Adjust the bullet speed as needed
    void Update()
    {
        // kijkt naar de muis pozietzi en onthoud waar die is
        Vector3 mousePosition = Input.mousePosition;

         // zorgt er voor dat de muis z posietzie en die van de camera het zelfde zijn
        mousePosition.z = transform.position.z - Camera.main.transform.position.z;

        // zet de muis positzie van het scherm om naar cordienaten van de wereld
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calculate the direction from the object to the mouse position
        Vector2 direction = worldMousePosition - transform.position;

        // bekijkt de rotatie van de gun
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // hier gebeurt de rotatie door middle van de Euler
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}


