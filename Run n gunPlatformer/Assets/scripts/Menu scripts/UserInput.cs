using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{

    public static UserInput instance;

    public Vector2 Moveing;

    public bool Dash { get; private set; }

    public bool MenuOpenClose { get; private set; }

    private PlayerInput _playerInput;

    private InputAction _Moveing;
    private InputAction _Dash;
    private InputAction _MenuOpenClose;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _playerInput = GetComponent<PlayerInput>();

        SetUpActions();
    }

    private void SetUpActions()
    {
        _Moveing = _playerInput.actions["Moveing"];
        _Dash = _playerInput.actions["Dash"];
        _MenuOpenClose = _playerInput.actions["PauzeMenu"];
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateMovements();
    }

    private void UpdateMovements()
    {
        Moveing = _Moveing.ReadValue<Vector2>();
        Dash = _Dash.WasPressedThisFrame();
        MenuOpenClose = _MenuOpenClose.WasPressedThisFrame();
    }
}