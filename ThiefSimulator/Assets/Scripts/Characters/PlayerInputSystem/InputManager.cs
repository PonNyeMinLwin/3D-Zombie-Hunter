using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    InputControls inputControls;
    public Vector2 movementInput;
    
    private void OnEnable() {
        if (inputControls == null) { 
            inputControls = new InputControls(); 
            // Gets player input (WASD) and assigns it to a Vector2 variable
            inputControls.Player.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }
        inputControls.Enable();
    }

    private void OnDisable() {
        inputControls.Disable();
    }
}
