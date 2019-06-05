using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    WaveConfig waveConfig;
    List<Transform> waypoints;
    float moveSpeed;
    int waypointIndex = 0;

    void Start()
    {
        moveSpeed = GetComponent<Enemy>().Speed;
        transform.position = waypoints[waypointIndex].transform.position;
    }

    void Update()
    {
        Move();
    }

    public void SetupPathing(List<Transform> path)
    {
        waypoints = path;
    }

    void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
                if (waypointIndex == waypoints.Count)
                {
                    if (GetComponent<Enemy>().Boss)
                        waypointIndex = 1;
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}