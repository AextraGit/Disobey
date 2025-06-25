using UnityEngine;

public class PoliceMovement : MonoBehaviour
{
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // rotation turned off
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
