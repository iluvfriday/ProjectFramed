using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform player;
    public float sightRange = 10f;
    public float chaseSpeed = 4f;
    public float patrolSpeed = 2f;
    public float waitTime = 5f;

    private int currentPatrolIndex;
    private NavMeshAgent agent;
    private bool chasingPlayer = false;
    private bool waiting = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;
        currentPatrolIndex = 0;
        MoveToNextPatrolPoint();
    }

    private void Update()
    {
        if (CanSeePlayer())
        {
            StopAllCoroutines();
            ChasePlayer();
        }
        else
        {
            if (chasingPlayer)
            {
                chasingPlayer = false;
                agent.speed = patrolSpeed;
                MoveToNextPatrolPoint();
            }

            if (!waiting && !agent.pathPending && agent.remainingDistance < 0.5f)
            {
                StartCoroutine(WaitAtPatrolPoint());
            }
        }
    }

    private IEnumerator WaitAtPatrolPoint()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);
        MoveToNextPatrolPoint();
        waiting = false;
    }

    private void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;
        agent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    private bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < sightRange)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, directionToPlayer) > 0.5f)
            {
                RaycastHit hit;
                if (
                    Physics.Raycast(
                        transform.position + Vector3.up,
                        directionToPlayer,
                        out hit,
                        sightRange
                    )
                )
                {
                    if (hit.collider.CompareTag("Player"))
                        return true;
                }
            }
        }
        return false;
    }

    private void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        agent.destination = player.position;
        chasingPlayer = true;
    }
}
