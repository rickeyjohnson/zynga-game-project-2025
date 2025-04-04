using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ANTS.AntWorkerController
{
    public class PlayerAnimation : MonoBehaviour
    {
      [SerializeField] private Animator animator;
      [SerializeField] private float locomotionBlendSpeed = .02f;
      
      private PlayerLocomotionInput playerLocomotionInput;
      private PlayerState playerState;
      
      private static int inputXHash = Animator.StringToHash("InputX");
      private static int inputYHash = Animator.StringToHash("InputY");
      private static int isGroundedHash = Animator.StringToHash("isGrounded");
      private static int isJumpingHash = Animator.StringToHash("isJumping");
      private static int isFallingHash = Animator.StringToHash("isFalling");
      private static int isAttacking1Hash = Animator.StringToHash("isAttacking1");
      private static int isAttacking2Hash = Animator.StringToHash("isAttacking2");

      private Vector3 currentBlendInput = Vector3.zero;

      private void Awake()
      {
          playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
          playerState = GetComponent<PlayerState>();
      }

      private void Update()
      {
          UpdateAnimationState();
      }

      private void UpdateAnimationState()
      {
          bool isIdling = playerState.CurrentPlayerMovementState == PlayerMovementState.Idling;
          bool isWalking = playerState.CurrentPlayerMovementState == PlayerMovementState.Walking;
          bool isRunning = playerState.CurrentPlayerMovementState == PlayerMovementState.Running;
          bool isJumping = playerState.CurrentPlayerMovementState == PlayerMovementState.Jumping;
          bool isFalling = playerState.CurrentPlayerMovementState == PlayerMovementState.Falling;
          bool isGrounded = playerState.InGroundedState();
          bool isAttacking1 = playerState.CurrentPlayerMovementState == PlayerMovementState.Attacking1;
          bool isAttacking2 = playerState.CurrentPlayerMovementState == PlayerMovementState.Attacking2;

         
          Vector2 inputTarget = playerLocomotionInput.MovementInput;
          currentBlendInput = Vector3.Lerp(currentBlendInput, inputTarget, locomotionBlendSpeed * Time.deltaTime);
          
          animator.SetBool(isGroundedHash, isGrounded);
          animator.SetBool(isJumpingHash, isJumping);
          animator.SetBool(isFallingHash, isFalling);
          animator.SetBool(isAttacking1Hash,isAttacking1);
          animator.SetBool(isAttacking2Hash,isAttacking2);
          animator.SetFloat(inputXHash, inputTarget.x);
          animator.SetFloat(inputYHash, inputTarget.y);
      }
    }
}