using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class Player_Look : MonoBehaviour

{

    public float sensitivity = 100f;
    public Transform cameraTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnLook(InputValue value)
    {
        Vector2 delta = value.Get<Vector2>(); // delta mouse movement from last frame

        float deltaY = delta.x * sensitivity * Time.deltaTime; // delta mouse movement in Y direction (left, right), combined with sensitivity and time (for stable performance indipendent of framerate)
        float deltaX = delta.y * sensitivity * Time.deltaTime; // delta mouse movement in X direction (up, down)

        cameraTransform.Rotate(0, deltaX, deltaY); //zxy

        Debug.Log($"Mouse Delta: {value}, deltaX (vertical): {deltaX}, deltaY (horizontal): {deltaY}");
    }
}
