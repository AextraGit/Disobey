using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowStone : MonoBehaviour
{
    public GameObject brickPrefab; // Prefab for the brick to be thrown
    public Transform throwPoint; // Point from where the brick will be thrown
    public float throwForce = 500f; // Force applied to the brick when thrown
    public Feedback feedback; // Reference to the Feedback script to update the count
    public int maxCount = 10;

    private int count; // Counter for the number of bricks thrown
    private int bricksLeft; // Counter for the number of bricks left

    void Start()
    {
        count = 0;
        bricksLeft = maxCount; // Initialize the number of bricks left
        feedback.UpdateCount(count, bricksLeft, maxCount); // Initialize the count in the Feedback script
    }

    public void OnThrow(InputValue value)
    {
        if (value.isPressed && count < maxCount)
        {
            Throw();
            count++;
            bricksLeft--;
            feedback.UpdateCount(count, bricksLeft, maxCount); // Update the count in the Feedback script
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
