using UnityEngine;
using UnityEngine.AI;

public class PoliceMovement : MonoBehaviour
{
    private Rigidbody rb;
    public Transform player;
    private NavMeshAgent agent;
    public float followDistance = 10f;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // rotation turned off

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= followDistance)
        {
            agent.SetDestination(player.position);
        }

    }
}
