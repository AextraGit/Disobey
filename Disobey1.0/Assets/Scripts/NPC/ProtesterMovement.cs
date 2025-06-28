using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class ProtesterMovement : MonoBehaviour
{
    private Rigidbody rb;
    public Transform player;
    public NavMeshAgent agent;

    private ProtesterState currentState;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // rotation turned off

        ChangeState(new WanderState(this));
    }

    void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(ProtesterState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
