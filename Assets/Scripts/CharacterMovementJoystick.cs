using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class CharacterMovementJoystick : MonoBehaviour
{
   [SerializeField] private Rigidbody _rigidbody;
   [SerializeField] private FixedJoystick _joystick;
   [SerializeField] private Animator _animator;
   [SerializeField] private float _moveSpeed= 2.0f;
   [SerializeField] private float _rotationSpeed = 10.0f;  // Added for smooth rotation
 /*
   private void FixedUpdate()
   {
      _rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y,
         _joystick.Vertical * _moveSpeed);


      float moveAmount = _rigidbody.velocity.magnitude;
      Vector3 targetForwardDirection = _rigidbody.velocity;
     
      //get the rotation that corresponds to facing in the direction of the velocity
      Quaternion targetRotation = Quaternion.LookRotation(targetForwardDirection);
     
      //explicity set the rotation of the rigidbody
      _rigidbody.MoveRotation(targetRotation);
      _animator.SetFloat("MoveAmount", moveAmount);
      
      
   }
 */
 public void Move()
 {
     /*
     _rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y,
         _joystick.Vertical * _moveSpeed);

     float moveAmount = _rigidbody.velocity.magnitude;
     Vector3 targetForwardDirection = _rigidbody.velocity;
     
     // Get the rotation that corresponds to facing in the direction of the velocity
     Quaternion targetRotation = Quaternion.LookRotation(targetForwardDirection);
     
     // Explicitly set the rotation of the rigidbody
     _rigidbody.MoveRotation(targetRotation);
     _animator.SetFloat("MoveAmount", moveAmount);
     */
     
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
 
}
