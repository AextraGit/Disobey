using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;



public class Player_Movement : MonoBehaviour
{
    public Rigidbody rb;

    public float moveSpeed = 1;
    private Vector2 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX; // rotation in Z and X axis turned off to prevent the player from falling over
        rb.constraints = RigidbodyConstraints.FreezeRotation; // rotation turned off to prevent the player from falling over
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, 0, moveDirection.y * moveSpeed);
    }

    public void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }
}
