using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class ProtesterMovement : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject player;
    public NavMeshAgent agent;
    private bool isPlayerNearby;
    private int numberOfPoliceNearby = 0;
    public List<GameObject> policeNearby = new List<GameObject>();
    private ProtesterState currentState;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // rotation turned off

        ChangeState(new ProtesterWanderState(this));
    }

    void Update()
    {
        currentState?.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
        if (other.CompareTag("Police"))
        {
            numberOfPoliceNearby++;
            policeNearby.Add(other.gameObject);
            if (numberOfPoliceNearby > 0 && !(currentState is ProtesterHuntState))
            {
                ChangeState(new ProtesterHuntState(this));
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
        if (other.CompareTag("Police"))
        {
            numberOfPoliceNearby--;
            policeNearby.Remove(other.gameObject);
            if (numberOfPoliceNearby == 0)
            {
                ChangeState(new ProtesterWanderState(this));
            } else
            {
                ChangeState(new ProtesterHuntState(this));
            }
        }
    }
    public void Call()
    {
        if (isPlayerNearby && !(currentState is ProtesterSeekState))
        {
            ChangeState(new ProtesterSeekState(this));
        }
    }

    public void ChangeState(ProtesterState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
