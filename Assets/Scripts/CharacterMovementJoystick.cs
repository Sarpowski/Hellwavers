using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleInputNamespace;
[RequireComponent(typeof(Rigidbody))]
public class CharacterMovementJoystick : MonoBehaviour
{
    
   [SerializeField] private Rigidbody _rigidbody;
   [SerializeField] private FloatingJoystick _joystick;
   [SerializeField] private Animator _animator;
   [SerializeField] private float _moveSpeed= 2.0f;
   [SerializeField] private float _rotationSpeed = 10.0f;  // Added for smooth rotation
  //new stuff
   [SerializeField] private float _dashSpeed = 10.0f;
   [SerializeField] private float _dashDuration = 0.5f;
   
   private bool _isDashing = false;
   
   
 public void Move()
 {
     
     Vector3 moveDirection = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y, _joystick.Vertical * _moveSpeed);
     _rigidbody.velocity = moveDirection;
     float moveAmount = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical).magnitude;
     _animator.SetFloat("MoveAmount", moveAmount);

     if (moveDirection.magnitude > 0.1f)
     {
         Vector3 targetForwardDirection = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
         Quaternion targetRotation = Quaternion.LookRotation(targetForwardDirection);
         _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation, _rotationSpeed * Time.deltaTime));
     }
 }

 public void AddMoveSpeed(float moveSpeed)
 {
     float tolerance = 0.0001f;
     float maxMoveSpeed = 14.0f;

     
     if (Mathf.Abs(moveSpeed - 0.1f) < tolerance)
     {
         return;
     }

     float newMoveSpeed = _moveSpeed + moveSpeed;
     
     if (newMoveSpeed > maxMoveSpeed)
     {
         _moveSpeed = maxMoveSpeed;
     }
     else
     {
         _moveSpeed = newMoveSpeed;
     }
 }
 public void Dash()
 {
     if (!_isDashing)
     {
         StartCoroutine(DashCoroutine());
     }
 }

 private IEnumerator DashCoroutine()
 {
     _isDashing = true;

     float originalMoveSpeed = _moveSpeed;
     _moveSpeed = _dashSpeed;

     yield return new WaitForSeconds(_dashDuration);

     _moveSpeed = originalMoveSpeed;
     _isDashing = false;
 }
}
