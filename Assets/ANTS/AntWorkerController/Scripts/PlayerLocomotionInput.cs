using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace ANTS.AntWorkerController
{
    [DefaultExecutionOrder(-2)]
    public class PlayerLocomotionInput : MonoBehaviour, PlayerControls.IPlayerLocomotionMapActions
    {
       public PlayerControls PlayerControls {get; private set;}
       public Vector2 MovementInput {get; private set;}
       
       public Vector2 LookInput {get; private set;}
       
       public bool JumpPressed {get; private set;}
       public bool Attack1Pressed {get; private set;}
       public bool Attack2Pressed {get; private set;}

       private void OnEnable()
       {
           PlayerControls = new PlayerControls();
           PlayerControls.Enable();
           
           PlayerControls.PlayerLocomotionMap.Enable();
           PlayerControls.PlayerLocomotionMap.SetCallbacks(this);
           
       }

       private void OnDisable()
       {
           PlayerControls.PlayerLocomotionMap.Disable();
           PlayerControls.PlayerLocomotionMap.RemoveCallbacks(this);
       }

       public void OnMovement(InputAction.CallbackContext context)
       {
           MovementInput = context.ReadValue<Vector2>();
           print(MovementInput);
       }

       private void LateUpdate()
       {
           JumpPressed = false;
           Attack2Pressed = false;
           Attack1Pressed = false;
       }

       public void OnLook(InputAction.CallbackContext context)
       {
           LookInput = context.ReadValue<Vector2>();
       }

       public void OnJump(InputAction.CallbackContext context)
       {
           if (!context.performed)
               return;
           
           JumpPressed = true;
       }

       public void OnAttack1(InputAction.CallbackContext context)
       {
           if (!context.performed)
               return;
           
           Attack1Pressed = true;
       }

       public void OnAttack2(InputAction.CallbackContext context)
       {
           if (!context.performed)
               return;
           
           Attack2Pressed = true;
       }
    }
}
