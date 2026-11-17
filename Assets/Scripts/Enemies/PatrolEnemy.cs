using UnityEngine;

public class PatrolEnemy : Enemy
{
    public Transform pointA;
    public Transform pointB;

    PatrolEnemyStats data;
    Transform target;

    void Start()
    {
        data = (PatrolEnemyStats)stats;
        target = pointB;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, data.speed * Time.deltaTime);
        transform.LookAt(target.position);

        if (Vector3.Distance(transform.position, target.position) < data.arriveDistance)
        {
            if (target == pointA)
            {
                target = pointB;
            }
            else
            {
                target = pointA;
            }
        }
    }
}
