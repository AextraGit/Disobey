using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowStone : MonoBehaviour
{
    public GameObject brickPrefab; // Prefab for the brick to be thrown
    public Transform throwPoint; // Point from where the brick will be thrown
    public float throwForce = 500f; // Force applied to the brick when thrown
    public Feedback feedback; // Reference to the Feedback script to update the count
    public int maxCount = 10;
    public float cooldownTime = 10f; // Cooldown time in seconds

    // public InputActionReference throwAction;

    private int count; // Counter for the number of bricks thrown
    private int bricksLeft; // Counter for the number of bricks left

    void Start()
    {
        count = 0;
        bricksLeft = maxCount; // Initialize the number of bricks left
        feedback.UpdateCount(count, bricksLeft, maxCount); // Initialize the count in the Feedback script
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

    public void OnThrow(InputValue value)
    {
        if (value.isPressed && count < maxCount)
        {
            Throw();
            count++;
            bricksLeft--;
            feedback.UpdateCount(count, bricksLeft, maxCount); // Update the count in the Feedback script
            // StartCoroutine(CoolDown()); // Start cooldown after throwing a brick
        } else if (value.isPressed && count >= maxCount)
        {
            // make text red or something
            StartCoroutine(feedback.UpdateMaxCountReached(count, bricksLeft, maxCount));
        }
    }

    void Throw()
    {
        GameObject brick = Instantiate(brickPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody brickRB = brick.GetComponent<Rigidbody>();
        brickRB.AddForce(throwPoint.forward * throwForce);
    }
}
