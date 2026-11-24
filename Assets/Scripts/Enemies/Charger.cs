using UnityEngine;
using UnityEngine.AI;

public class Charger : SmartEnemy
{
    enum State { Wander, WindUp, Charge, Tired }

    ChargerStats data;
    State state;
    NavMeshAgent agent;
    Vector3 home;
    Vector3 chargeDirection;
    float timer;
    bool hitPlayer;

    protected override void Start()
    {
        base.Start();
        data = (ChargerStats)stats;
        agent = GetComponent<NavMeshAgent>();
        home = transform.position;
        StartWander();
    }

    void Update()
    {
        if (state == State.Wander) UpdateWander();
        else if (state == State.WindUp) UpdateWindUp();
        else if (state == State.Charge) UpdateCharge();
        else UpdateTired();
    }

    void StartWander()
    {
        state = State.Wander;
        ClearTint();
        agent.isStopped = false;
        agent.speed = data.wanderSpeed;
        PickWanderPoint();
        Play("Walk");
    }

    void PickWanderPoint()
    {
        Vector2 random = Random.insideUnitCircle * data.wanderRadius;
        Vector3 point = home + new Vector3(random.x, 0f, random.y);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(point, out hit, data.wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    void UpdateWander()
    {
        if (!agent.pathPending && agent.remainingDistance < data.arriveDistance)
        {
            PickWanderPoint();
        }

        if (DistanceToPlayer() <= data.triggerRange && CanSeePlayer() && OnScreen())
        {
            state = State.WindUp;
            timer = 0f;
            agent.isStopped = true;
            Play("No");
            Warn();
        }
    }

    void UpdateWindUp()
    {
        timer += Time.deltaTime;
        FacePlayer();

        if (timer >= data.windUp)
        {
            chargeDirection = player.position - transform.position;
            chargeDirection.y = 0f;
            chargeDirection.Normalize();
            state = State.Charge;
            timer = 0f;
            hitPlayer = false;
            Play("Bite_Front");
            SoundManager.Play(stats.attackSound);
        }
    }

    void UpdateCharge()
    {
        timer += Time.deltaTime;
        agent.Move(chargeDirection * data.chargeSpeed * Time.deltaTime);

        if (!hitPlayer && DistanceToPlayer() <= data.hitDistance)
        {
            hitPlayer = true;
            HurtPlayer(data.damage);
        }

        if (timer >= data.chargeTime)
        {
            state = State.Tired;
            timer = 0f;
            Tint(data.tiredColor, data.warnTint);
            Play("Idle");
        }
    }

    void UpdateTired()
    {
        timer += Time.deltaTime;
        if (timer >= data.tiredTime)
        {
            StartWander();
        }
    }
}
