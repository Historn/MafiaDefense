using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement2 : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;

    private Enemy enemy;

    public float distance = 0.45f;

    void Start()
    {
        enemy = GetComponent<Enemy>();

        target = Waypoints2.points[0];
    }

    void Update()
    {
        Vector3 direction = target.position - transform.position;
        enemy.transform.LookAt(target);
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= distance)
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;

    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints2.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints2.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WavesSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}