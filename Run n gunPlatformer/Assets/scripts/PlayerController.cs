using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement movement;
    void Start()
    {
        movement = GetComponent<Movement>();
        if (movement == null)
        {
            Debug.LogError("Movement component not found!");
        }
    }

    void Update()
    {
        // mabby need to change set the command here just in case
        float moveInput = Input.GetAxis("Horizontal");
        movement.SetMovementInput(moveInput);

        if (Input.GetKeyDown(KeyCode.W))
        {
            movement.Jump();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            movement.Dash();
        }
    }
}
