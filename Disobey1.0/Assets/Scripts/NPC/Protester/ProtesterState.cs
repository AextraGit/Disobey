using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public interface ProtesterState
{
    void Enter();
    void Update();
    void Exit();
}

public class ProtesterWanderState : ProtesterState
{
    private ProtesterMovement npc;
    public ProtesterWanderState(ProtesterMovement npc)
    { 
        this.npc = npc; 
    }

    private Vector3 startLocation;
    private Vector3 wanderLocation;

    public void Enter()
    {
        npc.agent.speed = 1f;
        startLocation = npc.transform.position;
        SetNewWanderLocation();
        Debug.Log("Entered Wander state");
    }

    public void Update()
    {
        if (Vector3.Distance(npc.transform.position, wanderLocation) <= 1.5)
        {
            SetNewWanderLocation();
        }
    }

    private void SetNewWanderLocation()
    {
        Vector3 randomOffset = new Vector3(Random.Range(-5, 6), 0, Random.Range(-5, 6));
        Vector3 potentialLocation = startLocation + randomOffset;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(potentialLocation, out hit, 2.0f, NavMesh.AllAreas))
        {
            wanderLocation = hit.position;
            npc.agent.SetDestination(wanderLocation);
        }
        else
        {
            wanderLocation = npc.transform.position;
            npc.agent.SetDestination(wanderLocation);
        }
    }

    public void Exit()
    {
        Debug.Log("Exited Wander state");
    }
}

public class ProtesterSeekState : ProtesterState
{
    private ProtesterMovement npc;
    private Transform player;
    public ProtesterSeekState(ProtesterMovement npc) 
    { 
        this.npc = npc;
        player = npc.player.transform;
    }

    public void Enter()
    {
        npc.agent.speed = 2.5f;
    }

    public void Update()
    {
        Vector3 directionToNPC = (npc.transform.position - player.transform.position).normalized;
        npc.agent.SetDestination(player.position + directionToNPC * 5f);
    }

    public void Exit()
    {
        npc.agent.speed = 3.5f;
    }
}

public class ProtesterHuntState : ProtesterState
{
    private ProtesterMovement npc;
    private Transform player;
    public ProtesterHuntState(ProtesterMovement npc)
    { 
        this.npc = npc;
        player = npc.player.transform;
    }

    public GameObject target;

    public void Enter()
    {
        npc.agent.speed = 2f;
    }

    public void Update()
    {
        GameObject closestEnemy = null;
        float minDistance = float.MaxValue;
        Vector3 npcPos = npc.transform.position;

        foreach (GameObject enemy in npc.policeNearby)
        {
            float dist = Vector3.Distance(npcPos, enemy.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closestEnemy = enemy;
            }
        }

        target = closestEnemy;

        if (Vector3.Distance(target.transform.position, npc.transform.position) >= 1.0f)
        {
            npc.agent.SetDestination(target.transform.position);
        }
    }

    public void Exit()
    {
        npc.agent.speed = 3.5f;
    }
}