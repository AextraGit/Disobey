using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class ThrowMolli : MonoBehaviour
{
    public GameObject bottlePrefab;
    public float throwForce = 700f;
    
    public Feedback feedback;
    
    public int maxCount = 1000;
    public float cooldownTime = 10f;

    public Camera mainCamera;
    public float leftRotationAdjustment = -0.1f;
    public float upRotationAdjustment = 0.3f;

    // public InputActionReference throwAction;

    private int count;
    private int mollisLeft;

    void Start()
    {
        count = 0;
        mollisLeft = maxCount;
        feedback.UpdateCount(count, mollisLeft, maxCount);
    }

    public void OnBomb(InputValue value)
    {
        if (value.isPressed && count < maxCount)
        {
            Throw();
            count++;
            mollisLeft--;
            feedback.UpdateCount(count, mollisLeft, maxCount);
            // StartCoroutine(CoolDown()); // Start cooldown after throwing a brick
        }
        else if (value.isPressed && count >= maxCount)
        {
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

    public int GetMollisLeft()
    {
        return mollisLeft;
    }
}
