using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

namespace ANTS.AntWorkerController
{
    [DefaultExecutionOrder(-1)]
    public class NewBehaviourScript : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Camera playerCamera;
       // private float verticalCameraRotation = 0f; 
        public Transform playerTransform;
        //public Transform cameraTransform;
        [Header("Base Movement")]
        public float runAcceleration = .25f;
        public float runSpeed = 6f;
        public float drag = .1f;
        public float movingThreshold = 0.01f;
        public float gravity = 25f;
        public float jumpSpeed = 1.0f;

        [Header("Camera Settings")]
        public float lookSenseH = .1f;
        public float lookSenseV = .1f;
        public float lookLimitV = 89f;
       
        [Header("Environment Details")]
        [SerializeField] public LayerMask groundLayer;
        
        
        private PlayerLocomotionInput playerLocomotionInput;
        private PlayerState playerState;
        private Vector2 cameraRotation = Vector2.zero;
        private Vector2 playerTargetRotation = Vector2.zero;
        //private float antibump;

        private float verticalVelocity = 0f;

        private void Awake()
        {
            playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
            playerState = GetComponent<PlayerState>();

            //antibump = runSpeed;
        }

        
        private void Update()
        {
            UpdateMovementState();
            HandleVerticalMovement();
            HandleLateralMovement();
            
        }
        
        private void UpdateMovementState()
        {
            bool isMovementInput = playerLocomotionInput.MovementInput != Vector2.zero;
            bool isMovingLaterally = IsMovingLaterally();
            bool isGrounded = IsGrounded();
            
            

            PlayerMovementState lateralState = isMovingLaterally || isMovementInput
                ? PlayerMovementState.Running
                : PlayerMovementState.Idling;
            playerState.SetPlayerMovementState(lateralState);

            if (!isGrounded && characterController.velocity.y > 0f)
            {
                playerState.SetPlayerMovementState(PlayerMovementState.Jumping);
            }
            else if (!isGrounded && characterController.velocity.y < 0f)
            {
                playerState.SetPlayerMovementState(PlayerMovementState.Falling);
            }
            
            
        }

        private void HandleVerticalMovement()
        {
            bool isGrounded = playerState.InGroundedState();
            
            
            if (isGrounded && verticalVelocity < 0)
                verticalVelocity = -.5f;
            verticalVelocity -= gravity * Time.deltaTime;

            if (playerLocomotionInput.JumpPressed && isGrounded)
            {
                verticalVelocity += Mathf.Sqrt(jumpSpeed * 3 * gravity);
            }
        }
        private void HandleLateralMovement()
        {
            bool isGrounded = playerState.InGroundedState();
            Vector3 cameraForwardXZ = new Vector3(playerCamera.transform.forward.x, 0f, playerCamera.transform.forward.z).normalized;
            Vector3 cameraRightXZ = new Vector3(playerCamera.transform.right.x, 0f, playerCamera.transform.right.z).normalized;
            Vector3 movementDirection = cameraRightXZ * playerLocomotionInput.MovementInput.x + cameraForwardXZ * playerLocomotionInput.MovementInput.y;
            
            Vector3 movementDelta = movementDirection * runAcceleration * Time.deltaTime;
            Vector3 newVelocity = characterController.velocity + movementDelta;
            
            Vector3 currentDrag = newVelocity.normalized * drag * Time.deltaTime;
            newVelocity = (newVelocity.magnitude > drag * Time.deltaTime) ? newVelocity - currentDrag: Vector3.zero;
            newVelocity  = Vector3.ClampMagnitude(newVelocity, runSpeed);
            newVelocity.y += verticalVelocity;
            
           
            characterController.Move(newVelocity * Time.deltaTime);

        }
        
        private void LateUpdate()
        {
            cameraRotation.x += lookSenseH * playerLocomotionInput.LookInput.x;
            cameraRotation.y = Mathf.Clamp(cameraRotation.y - lookSenseV * playerLocomotionInput.LookInput.y, -lookLimitV, lookLimitV);
            
            playerTargetRotation.x += transform.eulerAngles.x + lookSenseH * playerLocomotionInput.LookInput.x;
            transform.rotation = Quaternion.Euler(0f, playerTargetRotation.x, 0f);
            
            playerCamera.transform.rotation = Quaternion.Euler(cameraRotation.y, cameraRotation.x, 0f);
        }

        private bool IsMovingLaterally()
        {
            Vector3 lateralVelocity = new Vector3(characterController.velocity.x, 0f, characterController.velocity.y);
            return lateralVelocity.magnitude > movingThreshold;
        }

        private bool IsGrounded()
        {
            return characterController.isGrounded;
        }


        private bool IsGroundedWhileGrounded()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y-characterController.radius, transform.position.z);
            bool grounded = Physics.CheckSphere(spherePosition, characterController.radius, groundLayer, QueryTriggerInteraction.Ignore);

            return grounded;
        }

        private bool IsGroundedWhileAirborne()
        {
            return characterController.isGrounded;
        }
    }
    
}

