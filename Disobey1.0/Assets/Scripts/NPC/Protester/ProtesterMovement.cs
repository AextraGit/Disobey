using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

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
        if (numberOfPoliceNearby > 0)
        {
            if (!(currentState is ProtesterHuntState))
            {
                ChangeState(new ProtesterHuntState(this));
            }
        } else if (numberOfPoliceNearby == 0)
        {
            if (!(currentState is ProtesterWanderState) && !(currentState is ProtesterSeekState))
            {
                ChangeState(new ProtesterWanderState(this));
            }
        }
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
