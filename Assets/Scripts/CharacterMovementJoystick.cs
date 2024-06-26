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
     _rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y,
         _joystick.Vertical * _moveSpeed);

     float moveAmount = _rigidbody.velocity.magnitude;
     Vector3 targetForwardDirection = _rigidbody.velocity;
     
     // Get the rotation that corresponds to facing in the direction of the velocity
     Quaternion targetRotation = Quaternion.LookRotation(targetForwardDirection);
     
     // Explicitly set the rotation of the rigidbody
     _rigidbody.MoveRotation(targetRotation);
     _animator.SetFloat("MoveAmount", moveAmount);
     
 }
 
}
