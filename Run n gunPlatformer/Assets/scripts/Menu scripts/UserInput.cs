using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput instance;

    public Vector2 Moveing { get; private set; }
    public bool Dash { get; private set; }
    public bool MenuOpenClose { get; private set; }

    private PlayerInput _playerInput;
    private InputAction _Moveing;
    private InputAction _Dash;

    private void Awake()
    {
        // als de script niet bestaat maakt hij hem aan
        if (instance == null)
        {
            instance = this;
        }
        else
        {// als er all eentje word hij verwoest
            Destroy(gameObject); 
            return;
        }
        // dit geburt niet als het object niet null is
        DontDestroyOnLoad(gameObject); 

        SetUpActions();
    }

    private void SetUpActions()
    {// set de movement en dash gebaseerd op de input die de player heeft gekozen in de options menu 
        _Moveing = _playerInput.actions["Moving"];
        _Dash = _playerInput.actions["Dash"];
    }

    private void Update()
    {// hier word die uitgevoord
        UpdateMovements();
    }

    private void UpdateMovements()
    {// hier worden de inputs aangepast mocht de player ze willen veranderen
        Moveing = _Moveing.ReadValue<Vector2>();
        Dash = _Dash.WasPressedThisFrame();
    }
}
