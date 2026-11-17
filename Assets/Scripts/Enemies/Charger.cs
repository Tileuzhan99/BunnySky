using UnityEngine;
using UnityEngine.AI;

public class Charger : SmartEnemy
{
    enum State { Wander, WindUp, Charge, Tired }

    public float wanderRadius = 4f;
    public float wanderSpeed = 1.5f;
    public float arriveDistance = 0.3f;
    public float triggerRange = 9f;
    public float windUp = 0.7f;
    public float chargeSpeed = 11f;
    public float chargeTime = 0.8f;
    public float hitDistance = 1.3f;
    public float tiredTime = 1.5f;
    public Color tiredColor = Color.gray;
    public int damage = 1;

    State state;
    NavMeshAgent agent;
    Vector3 home;
    Vector3 chargeDirection;
    float timer;
    bool hitPlayer;

    protected override void Start()
    {
        base.Start();
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
        Tint(tiredColor, 0f);
        agent.isStopped = false;
        agent.speed = wanderSpeed;
        PickWanderPoint();
        Play("Walk");
    }

    void PickWanderPoint()
    {
        Vector2 random = Random.insideUnitCircle * wanderRadius;
        Vector3 point = home + new Vector3(random.x, 0f, random.y);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(point, out hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    void UpdateWander()
    {
        if (!agent.pathPending && agent.remainingDistance < arriveDistance)
        {
            PickWanderPoint();
        }

        if (DistanceToPlayer() <= triggerRange && CanSeePlayer() && OnScreen())
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

        if (timer >= windUp)
        {
            chargeDirection = player.position - transform.position;
            chargeDirection.y = 0f;
            chargeDirection.Normalize();
            state = State.Charge;
            timer = 0f;
            hitPlayer = false;
            Play("Bite_Front");
        }
    }

    void UpdateCharge()
    {
        timer += Time.deltaTime;
        agent.Move(chargeDirection * chargeSpeed * Time.deltaTime);

        if (!hitPlayer && DistanceToPlayer() <= hitDistance)
        {
            hitPlayer = true;
            HurtPlayer(damage);
        }

        if (timer >= chargeTime)
        {
            state = State.Tired;
            timer = 0f;
            Tint(tiredColor, warnTint);
            Play("Idle");
        }
    }

    void UpdateTired()
    {
        timer += Time.deltaTime;
        if (timer >= tiredTime)
        {
            StartWander();
        }
    }
}
