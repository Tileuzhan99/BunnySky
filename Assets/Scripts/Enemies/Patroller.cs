using UnityEngine;
using UnityEngine.AI;

public class Patroller : SmartEnemy
{
    enum State { Patrol, Chase, Attack }

    public Transform[] waypoints;

    PatrollerStats data;
    State state;
    NavMeshAgent agent;
    int waypointIndex;
    float timer;
    float lastSeenTime;
    bool attacked;

    protected override void Start()
    {
        base.Start();
        data = (PatrollerStats)stats;
        agent = GetComponent<NavMeshAgent>();
        StartPatrol();
    }

    void Update()
    {
        if (state == State.Patrol) UpdatePatrol();
        else if (state == State.Chase) UpdateChase();
        else UpdateAttack();
    }

    void StartPatrol()
    {
        state = State.Patrol;
        agent.isStopped = false;
        agent.speed = data.patrolSpeed;
        agent.SetDestination(waypoints[waypointIndex].position);
        Play("Walk");
    }

    void UpdatePatrol()
    {
        if (!agent.pathPending && agent.remainingDistance < data.arriveDistance)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
            agent.SetDestination(waypoints[waypointIndex].position);
        }

        if (CanSeePlayer())
        {
            StartChase();
        }
    }

    void StartChase()
    {
        state = State.Chase;
        agent.isStopped = false;
        agent.speed = data.chaseSpeed;
        lastSeenTime = Time.time;
        Play("Walk");
    }

    void UpdateChase()
    {
        if (CanSeePlayer())
        {
            lastSeenTime = Time.time;
        }

        agent.SetDestination(player.position);

        if (DistanceToPlayer() <= data.attackRange && OnScreen())
        {
            StartAttack();
        }
        else if (Time.time - lastSeenTime > data.loseSightTime)
        {
            StartPatrol();
        }
    }

    void StartAttack()
    {
        state = State.Attack;
        agent.isStopped = true;
        timer = 0f;
        attacked = false;
        Play("Idle");
        Warn();
    }

    void UpdateAttack()
    {
        timer += Time.deltaTime;

        if (!attacked)
        {
            FacePlayer();
            if (timer >= data.windUp)
            {
                attacked = true;
                ClearTint();
                Play("Bite_Front");
                SoundManager.Play(stats.attackSound);
                if (DistanceToPlayer() <= data.attackRange)
                {
                    HurtPlayer(data.damage);
                }
            }
        }
        else if (timer >= data.windUp + data.cooldown)
        {
            StartChase();
        }
    }

    public override void TakeHit(int damage)
    {
        base.TakeHit(damage);
        if (state == State.Patrol)
        {
            StartChase();
        }
    }
}
