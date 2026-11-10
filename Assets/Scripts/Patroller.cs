using UnityEngine;
using UnityEngine.AI;

public class Patroller : SmartEnemy
{
    enum State { Patrol, Chase, Attack }

    public Transform[] waypoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;
    public float arriveDistance = 0.3f;
    public float loseSightTime = 2f;
    public float attackRange = 1.6f;
    public float windUp = 0.5f;
    public float cooldown = 1.2f;
    public int damage = 1;

    State state;
    NavMeshAgent agent;
    int waypointIndex;
    float timer;
    float lastSeenTime;
    bool attacked;

    protected override void Start()
    {
        base.Start();
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
        agent.speed = patrolSpeed;
        agent.SetDestination(waypoints[waypointIndex].position);
        Play("Walk");
    }

    void UpdatePatrol()
    {
        if (!agent.pathPending && agent.remainingDistance < arriveDistance)
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
        agent.speed = chaseSpeed;
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

        if (DistanceToPlayer() <= attackRange && OnScreen())
        {
            StartAttack();
        }
        else if (Time.time - lastSeenTime > loseSightTime)
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
            if (timer >= windUp)
            {
                attacked = true;
                Tint(warnColor, 0f);
                Play("Bite_Front");
                if (DistanceToPlayer() <= attackRange)
                {
                    HurtPlayer(damage);
                }
            }
        }
        else if (timer >= windUp + cooldown)
        {
            StartChase();
        }
    }

    public override void TakeHit()
    {
        base.TakeHit();
        if (state == State.Patrol)
        {
            StartChase();
        }
    }
}
