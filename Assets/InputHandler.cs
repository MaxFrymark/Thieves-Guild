using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] HumanPlayer player;
    
    PlayerInputActions inputActions;
    
    void Start()
    {
        SetUpInput();
    }

    private void SetUpInput()
    {
        inputActions = new PlayerInputActions();
        
        inputActions.Enable();
        
        inputActions.Mouse.Click.performed += OnClick;
    }
    
    private void OnClick(InputAction.CallbackContext context)
    {

    }
}
