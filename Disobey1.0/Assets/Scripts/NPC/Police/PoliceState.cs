using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public interface PoliceState
{
    void Enter();
    void Update();
    void Exit();
}

public class PoliceWanderState : PoliceState
{
    private PoliceMovement npc;

    public PoliceWanderState(PoliceMovement npc)
    {
        this.npc = npc;
    }

    private Vector3 wanderLocation;
    private Vector3 startLocation;

    public void Enter()
    {
        npc.agent.speed = 1f;
        startLocation = npc.transform.position;
        npc.agent.SetDestination(GetNewWanderLocation());
    }

    public void Update()
    {

        if (npc.policeNearby.Count > 0)
        {
            npc.agent.SetDestination(CalculateFlockingVector());
        } else if (Vector3.Distance(npc.transform.position, wanderLocation) <= 1.1f)
        {
            npc.agent.SetDestination(GetNewWanderLocation());
        }
    }

    private Vector3 CalculateFlockingVector()
    {

        Vector3 movePosition = Vector3.zero;
        Vector3 separationVector = Vector3.zero;

        float minDistance = 3f;

        foreach (GameObject police in npc.policeNearby)
        {
            movePosition += police.transform.position;

            float distance = Vector3.Distance(police.transform.position, npc.transform.position);
            Vector3 toNeighbor = police.transform.position - npc.transform.position;

            if (distance < minDistance)
            {
                if (distance < 0.001f) distance = 0.001f;
                separationVector -= toNeighbor.normalized * 3 / distance;
            }
        }

        movePosition = movePosition / npc.policeNearby.Count;

        return movePosition + separationVector * 1f;
    }

    private Vector3 GetNewWanderLocation()
    {
        Vector3 randomOffset = new Vector3(Random.Range(-8, 9), 0, Random.Range(-8, 9));
        Vector3 potentialLocation = startLocation + randomOffset;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(potentialLocation, out hit, 2.0f, NavMesh.AllAreas))
        {
            wanderLocation = hit.position;
            return(wanderLocation);
        }
        else
        {
            wanderLocation = npc.transform.position;
            return(wanderLocation);
        }
    }

    public void Exit()
    {
        npc.agent.speed = 3.5f;
    }
}

public class PoliceFleeState : PoliceState
{
    private PoliceMovement npc;
    public PoliceFleeState(PoliceMovement npc)
    {
        this.npc = npc;
    }

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

public class PoliceHuntState : PoliceState
{
    private PoliceMovement npc;
    private GameObject player;

    public PoliceHuntState(PoliceMovement npc)
    {
        this.npc = npc;
        player = npc.player;
    }

    public GameObject target;

    public void Enter()
    {
        npc.agent.speed = 3.5f;
    }

    public void Update()
    {
        if (target == null)
        {
            GameObject closestEnemy = null;
            float minDistance = float.MaxValue;
            Vector3 npcPos = npc.transform.position;

            // check if protesters nearby
            if (npc.protestersNearby.Count > 0)
            {
                foreach (GameObject enemy in npc.protestersNearby)
                {
                    float dist = Vector3.Distance(npcPos, enemy.transform.position);
                    if (dist < minDistance)
                    {
                        minDistance = dist;
                        closestEnemy = enemy;
                    }
                }

                //check if player is closer
                if (Vector3.Distance(closestEnemy.transform.position, npcPos) < Vector3.Distance(player.transform.position, npcPos))
                {
                    npc.agent.SetDestination(CalculateFlockingVector(closestEnemy));
                }
                else
                {
                    npc.agent.SetDestination(CalculateFlockingVector(player));
                }
            } else
            {
                npc.agent.SetDestination(CalculateFlockingVector(player));
            }
        }
    }

    public void Exit()
    {

    }

    private Vector3 CalculateFlockingVector(GameObject target)
    {

        Vector3 movePosition = Vector3.zero;
        Vector3 separationVector = Vector3.zero;

        float minDistance = 3f;

        foreach (GameObject police in npc.policeNearby)
        {
            movePosition += police.transform.position;

            float distance = Vector3.Distance(police.transform.position, npc.transform.position);
            Vector3 toNeighbor = police.transform.position - npc.transform.position;

            if (distance < minDistance)
            {
                if (distance < 0.001f) distance = 0.001f;
                separationVector -= toNeighbor.normalized * 3 / distance;
            }
        }

        movePosition = movePosition / npc.policeNearby.Count;
        movePosition = (movePosition + target.transform.position) / 2;

        return movePosition + separationVector * 1f;
    }
}