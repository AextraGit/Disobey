using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowStone : MonoBehaviour
{
    public Feedback feedback;
    public GameObject brickPrefab;
    public float throwForce = 700f;
    public int maxCount = 1000;
    public float cooldownTime = 10f;
    
    public Camera mainCamera;
    
    public float leftRotationAdjustment = -0.1f;
    public float upRotationAdjustment = 0.3f;

    // public InputActionReference throwAction;

    private int count;
    private int bricksLeft;

    void Start()
    {
        count = 0;
        bricksLeft = maxCount;
        feedback.UpdateCount(count, bricksLeft, maxCount);
    }

    public void OnThrow(InputValue value)
    {
        if (value.isPressed && count < maxCount)
        {
            Throw();
            count++;
            bricksLeft--;
            feedback.UpdateCount(count, bricksLeft, maxCount);
            // StartCoroutine(CoolDown());
        } else if (value.isPressed && count >= maxCount)
        {
            StartCoroutine(feedback.UpdateMaxCountReached(count, bricksLeft, maxCount));
        }
    }

    void Throw()
    {
        Vector3 spawnPosition = transform.position + transform.up * 1.5f + transform.forward * 1f + transform.right * 1f;
        GameObject brick = Instantiate(brickPrefab, spawnPosition, Quaternion.Euler(UnityEngine.Random.Range(-360, 361), UnityEngine.Random.Range(-360, 361), UnityEngine.Random.Range(-360, 361)));
        Rigidbody brickRB = brick.GetComponent<Rigidbody>();
        Vector3 throwDirection = (mainCamera.transform.forward + Vector3.up * upRotationAdjustment + Vector3.right * leftRotationAdjustment).normalized;
        brickRB.AddForce(throwDirection * throwForce);
    }

    public int GetBricksLeft()
    {
        return bricksLeft;
    }

    /*
       void OnEnable()
       {
           throwAction.action.started += ThrowAction;
           throwAction.action.Enable(); // Ensure the action is enabled
       }

       void OnDisable()
       {
           throwAction.action.started -= ThrowAction;
           throwAction.action.Disable(); // Disable the action when not in use
       }


       private void ThrowAction(InputAction.CallbackContext obj)
       {
           if (obj.performed && count < maxCount)
           {
               Throw();
               count++;
               bricksLeft--;
               feedback.UpdateCount(count, bricksLeft, maxCount); // Update the count in the Feedback script
               StartCoroutine(CoolDown()); // Start cooldown after throwing a brick
           } 
           else if (obj.performed && count >= maxCount)
           {
               StartCoroutine(feedback.UpdateMaxCountReached(count, bricksLeft, maxCount));
           }

       }

       IEnumerator CoolDown()
       {
           // Disable action while waiting for the cooldown time
           throwAction.action.Disable();
           yield return new WaitForSeconds(cooldownTime);
           // Re-enable action after the cooldown time
           throwAction.action.Enable();
       }
    */
}
