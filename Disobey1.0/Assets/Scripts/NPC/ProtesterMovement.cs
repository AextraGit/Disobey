using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class ProtesterMovement : MonoBehaviour
{
    private Rigidbody rb;
    public Transform player;
    public NavMeshAgent agent;
    private bool isPlayerNearby;
    private int numberOfEnemysNearby;
    public List<GameObject> enemysNearby = new List<GameObject>();

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
        if (other.CompareTag("Police"))
        {
            numberOfEnemysNearby++;
            enemysNearby.Add(other.gameObject);
            if (numberOfEnemysNearby == 1)
            {
                ChangeState(new HuntState(this));
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
        if (other.CompareTag("Police"))
        {
            numberOfEnemysNearby--;
            enemysNearby.Remove(other.gameObject);
            if (numberOfEnemysNearby == 0)
            {
                ChangeState(new WanderState(this));
            } else
            {
                ChangeState(new HuntState(this));
            }
        }
    }
    public void Call()
    {
        if (isPlayerNearby)
        {
            ChangeState(new SeekState(this));
        }
    }

    public void ChangeState(ProtesterState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
