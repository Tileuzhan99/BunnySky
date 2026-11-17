using UnityEngine;

public class Shooter : SmartEnemy
{
    enum State { Watch, Aim, Cooldown }

    public GameObject shotPrefab;
    public Transform firePoint;
    public LineRenderer laser;

    ShooterStats data;
    State state;
    float timer;

    protected override void Start()
    {
        base.Start();
        data = (ShooterStats)stats;
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
        transform.Rotate(0f, data.scanSpeed * Time.deltaTime, 0f);

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
        laser.SetPosition(1, PlayerChest());

        if (!CanSeePlayer())
        {
            laser.enabled = false;
            ClearTint();
            state = State.Watch;
        }
        else if (timer >= data.windUp)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Quaternion rotation = Quaternion.LookRotation(PlayerChest() - firePoint.position);
        Instantiate(shotPrefab, firePoint.position, rotation);

        laser.enabled = false;
        ClearTint();
        Play("Bite_Front");
        state = State.Cooldown;
        timer = 0f;
    }

    void UpdateCooldown()
    {
        timer += Time.deltaTime;
        if (timer >= data.cooldown)
        {
            Play("Flying");
            state = State.Watch;
        }
    }
}
