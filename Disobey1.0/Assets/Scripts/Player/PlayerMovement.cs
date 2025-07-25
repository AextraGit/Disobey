using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;



public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float moveSpeed = 10;
    private Vector2 inputDirectionVector;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation; // rotation turned off to prevent the player from falling over and Y-Movement turned off
    }

    private void FixedUpdate()
    {
        Vector2 viewDirectionVector = new Vector2(transform.forward.x, transform.forward.z); // direction in which the Player is looking (used as basis vector b2)
        Vector2 viewDirectionRightVector = new Vector2(transform.right.x, transform.right.z); // basis vector b1
        Vector2 moveDirection = (inputDirectionVector.x * viewDirectionRightVector + inputDirectionVector.y * viewDirectionVector).normalized; // inputDirectionVector but viewDirection used as basis (x * b1 + y * b2)
        rb.linearVelocity = new Vector3(moveDirection.x, 0, moveDirection.y) * moveSpeed;
    }

    public void OnMove(InputValue value)
    {
        inputDirectionVector = value.Get<Vector2>();
    }
}
