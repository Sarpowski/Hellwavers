using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private float playerSpeed = 2.0f;

    private float horizontal;

    private float vertical;
    private Vector3 playerVelocity;
    [SerializeField]
    private CharacterController _controller;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontal, 0, vertical);
        _controller.Move(move * Time.deltaTime * playerSpeed);
        
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        _controller.Move(playerVelocity * Time.deltaTime);
    }
}
