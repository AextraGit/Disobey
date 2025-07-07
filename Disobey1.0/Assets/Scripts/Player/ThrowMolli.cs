using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class ThrowMolli : MonoBehaviour
{
    public GameObject bottlePrefab; // Prefab for the molli to be thrown
    public float throwForce = 700f; // Force applied to the molli when thrown
    public Feedback feedback; // Reference to the Feedback script to update the count
    public int maxCount = 1000;
    public float cooldownTime = 10f; // Cooldown time in seconds
    public Camera mainCamera;
    public float leftRotationAdjustment = -0.1f; // Adjustment for left rotation of the throw direction
    public float upRotationAdjustment = 0.3f; // Adjustment for right rotation of the throw direction

    // public InputActionReference throwAction;

    private int count; // Counter for the number of bricks thrown
    private int mollisLeft; // Counter for the number of bricks left

    void Start()
    {
        count = 0;
        mollisLeft = maxCount; // Initialize the number of bricks left
        feedback.UpdateCount(count, mollisLeft, maxCount); // Initialize the count in the Feedback script
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnThrow(InputValue value)
    {
        if (value.isPressed && count < maxCount)
        {
            Throw();
            count++;
            mollisLeft--;
            feedback.UpdateCount(count, mollisLeft, maxCount); // Update the count in the Feedback script
            // StartCoroutine(CoolDown()); // Start cooldown after throwing a brick
        }
        else if (value.isPressed && count >= maxCount)
        {
            // make text red or something
            StartCoroutine(feedback.UpdateMaxCountReached(count, mollisLeft, maxCount));
        }
    }

    void Throw()
    {
        Vector3 spawnPosition = transform.position + transform.up * 1.5f + transform.forward * 1f + transform.right * 1f;
        GameObject molli = Instantiate(bottlePrefab, spawnPosition, Quaternion.Euler(UnityEngine.Random.Range(-360, 361), UnityEngine.Random.Range(-360, 361), UnityEngine.Random.Range(-360, 361)));
        Rigidbody molliRB = molli.GetComponent<Rigidbody>();
        Vector3 throwDirection = (mainCamera.transform.forward + Vector3.up * upRotationAdjustment + Vector3.right * leftRotationAdjustment).normalized;
        molliRB.AddForce(throwDirection * throwForce);
    }
}
