using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowStone : MonoBehaviour
{
    public GameObject brickPrefab; // Prefab for the brick to be thrown
    public Transform throwPoint; // Point from where the brick will be thrown
    public float throwForce = 500f; // Force applied to the brick when thrown
    public Feedback feedback; // Reference to the Feedback script to update the count

    private int count; // Counter for the number of bricks thrown
    private int maxCount;

    void Start()
    {
        count = 0;
        maxCount = 50;
        feedback.UpdateCount(count, maxCount); // Initialize the count in the Feedback script
    }

    public void OnThrow(InputValue value)
    {
        if (value.isPressed && count < maxCount)
        {
            Throw();
            count++;
            feedback.UpdateCount(count, maxCount); // Update the count in the Feedback script
        } else
        {
            // make text red or something
        }
    }

    void Throw()
    {
        GameObject brick = Instantiate(brickPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody brickRB = brick.GetComponent<Rigidbody>();
        brickRB.AddForce(throwPoint.forward * throwForce);
    }
}
