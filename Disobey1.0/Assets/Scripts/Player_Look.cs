using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using static UnityEngine.Rendering.DebugUI;

public class Player_Look : MonoBehaviour
{

    public float sensitivity = 30f;
    public Transform cameraTransform;
    public Transform playerTransform;

    float maxYRotation = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraTransform.Rotate(0f, 0f, 0f);
    }


    public void OnLook(InputValue value)
    {
        Vector2 delta = value.Get<Vector2>(); // delta mouse movement from last frame

        float deltaX = delta.x * sensitivity * Time.deltaTime; // delta mouse movement in Y direction (left, right), combined with sensitivity and time (for stable performance indipendent of framerate)
        float deltaY = - delta.y * sensitivity * Time.deltaTime; // delta mouse movement in X direction (up, down)
        maxYRotation += deltaY;
        maxYRotation = Mathf.Clamp(maxYRotation, -90f, 90f); // Ensuring y doesnt go any further than 90° for feels
        cameraTransform.transform.localRotation = Quaternion.Euler(maxYRotation, 0f, 0f);
        transform.Rotate(0f, deltaX, 0f); //.y, .x, .z I presume
        //playerTransform.Rotate(0f, deltaX, 0f);

        Debug.Log($"Mouse Delta: {value}, deltaX (vertical): {deltaX}, deltaY (horizontal): {deltaY}");
    }
}
