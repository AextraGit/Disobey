using UnityEngine;
using UnityEngine.AI;

public interface ProtesterState
{
    void Enter();
    void Update();
    void Exit();
}

public class WanderState : ProtesterState
{
    private ProtesterMovement npc;
    public WanderState(ProtesterMovement npc) { this.npc = npc; }

    private Vector3 startLocation;
    private Vector3 wanderLocation;

    public void Enter()
    {
        npc.agent.speed = 1f;
        startLocation = npc.transform.position;
        SetNewWanderLocation();
    }

    public void Update()
    {
        if (Vector3.Distance(npc.transform.position, wanderLocation) <= 1.1)
        {
            SetNewWanderLocation();
        }
    }

    private void SetNewWanderLocation()
    {
        Vector3 randomOffset = new Vector3(Random.Range(-5, 6), 0, Random.Range(-5, 6));
        Vector3 potentialLocation = startLocation + randomOffset;

        NavMeshHit hit;
        // Suche innerhalb von 2 Einheiten um den Punkt nach einer gültigen NavMesh-Position
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
        npc.agent.speed = 3.5f;
    }
}

public class SeekState : ProtesterState
{
    private ProtesterMovement npc;
    public SeekState(ProtesterMovement npc) { this.npc = npc; }

    public void Enter()
    {

    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}

public class HuntState : ProtesterState
{
    private ProtesterMovement npc;
    public HuntState(ProtesterMovement npc) { this.npc = npc; }

    public void Enter()
    {

    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}