using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;  // Waypoints for patrol
    public float patrolSpeed = 2f;    // Speed for patrol movement
    public float chaseSpeed = 4f;     // Speed when chasing player
    public float chaseDistance = 5f;  // Distance within which enemy will chase player

    private int currentPointIndex = 0;
    private Transform player;
    private bool isChasing = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Patrol();

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= chaseDistance)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
    }

    void Patrol()
    {
        if (!isChasing)
        {
            Transform targetPoint = patrolPoints[currentPointIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, patrolSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPoint.position) < 0.2f)
            {
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            }
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }
}
