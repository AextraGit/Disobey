using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceMovement : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject player;
    public NavMeshAgent agent;
    private bool isPlayerNearby = false;
    public GameObject playerNearby;
    public List<GameObject> policeNearby = new List<GameObject>();
    public List<GameObject> protestersNearby = new List<GameObject>();
    private PoliceState currentState;
    public Animator animator;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // rotation turned off

        ChangeState(new PoliceWanderState(this));
    }

    // Update is called once per frame
    void Update()
    {
        policeNearby.RemoveAll(npc => npc == null);
        protestersNearby.RemoveAll(npc => npc == null);

        int enemyCount = protestersNearby.Count;
        if (isPlayerNearby) enemyCount++;

        if ((enemyCount > 0 && enemyCount <= policeNearby.Count + 1))
        {
            if (!(currentState is PoliceHuntState))
            {
                ChangeState(new PoliceHuntState(this));
            }
        } else if (enemyCount > 0 && enemyCount > policeNearby.Count + 1)
        {
            if (!(currentState is PoliceFleeState))
            { 
            ChangeState(new PoliceFleeState(this));
            }
        } else if (!(currentState is PoliceWanderState))
        {
            ChangeState(new PoliceWanderState(this));
        }

            currentState.Update();
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
            policeNearby.Add(other.gameObject);
        }
        if (other.CompareTag("Protester"))
        {
            protestersNearby.Add(other.gameObject);
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
            policeNearby.Remove(other.gameObject);
        }
        if (other.CompareTag("Protester"))
        {
            protestersNearby.Remove(other.gameObject);
        }
    }

    public void ChangeState(PoliceState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
