using UnityEngine;

public class Shooter : SmartEnemy
{
    enum State { Watch, Aim, Cooldown }

    public GameObject shotPrefab;
    public Transform firePoint;
    public LineRenderer laser;
    public float scanSpeed = 40f;
    public float windUp = 0.6f;
    public float cooldown = 1.6f;

    State state;
    float timer;

    protected override void Start()
    {
        base.Start();
        laser.enabled = false;
        state = State.Watch;
    }

    void Update()
    {
        if (state == State.Watch) UpdateWatch();
        else if (state == State.Aim) UpdateAim();
        else UpdateCooldown();
    }

    void UpdateWatch()
    {
        transform.Rotate(0f, scanSpeed * Time.deltaTime, 0f);

        if (CanSeePlayer() && OnScreen())
        {
            state = State.Aim;
            timer = 0f;
            laser.enabled = true;
            Warn();
        }
    }

    void UpdateAim()
    {
        timer += Time.deltaTime;
        FacePlayer();
        laser.SetPosition(0, firePoint.position);
        laser.SetPosition(1, player.position + Vector3.up * playerHeight);

        if (!CanSeePlayer())
        {
            laser.enabled = false;
            Tint(warnColor, 0f);
            state = State.Watch;
        }
        else if (timer >= windUp)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 target = player.position + Vector3.up * playerHeight;
        Quaternion rotation = Quaternion.LookRotation(target - firePoint.position);
        Instantiate(shotPrefab, firePoint.position, rotation);

        laser.enabled = false;
        Tint(warnColor, 0f);
        Play("Bite_Front");
        state = State.Cooldown;
        timer = 0f;
    }

    void UpdateCooldown()
    {
        timer += Time.deltaTime;
        if (timer >= cooldown)
        {
            Play("Flying");
            state = State.Watch;
        }
    }
}
