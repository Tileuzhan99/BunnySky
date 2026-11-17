using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : Enemy
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public Transform player;
    public Slider hpBar;
    public AudioClip hitSound;

    BossStats data;
    Animator anim;
    AudioSource audioSource;

    void Start()
    {
        data = (BossStats)stats;
        hpBar.maxValue = data.maxHp;
        hpBar.value = hp;
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Attack());
    }

    void Update()
    {
        Vector3 lookPoint = player.position;
        lookPoint.y = transform.position.y;
        transform.LookAt(lookPoint);
    }

    public override void TakeHit(int damage)
    {
        audioSource.PlayOneShot(hitSound);
        base.TakeHit(damage);
        hpBar.value = hp;
    }

    protected override void Die()
    {
        SaveSystem.BossBeaten();
        SceneManager.LoadScene("Win");
    }

    IEnumerator Attack()
    {
        while (true)
        {
            float pause = data.slowPause;
            int count = data.slowCount;

            if (hp <= data.maxHp * data.fastBelowHpPart)
            {
                pause = data.fastPause;
                count = data.fastCount;
            }

            yield return new WaitForSeconds(pause);

            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(data.attackDelay);

            ShootFan(count);
        }
    }

    void ShootFan(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float angle = (i - count / 2) * data.fanAngle;
            Quaternion rotation = firePoint.rotation * Quaternion.Euler(0f, angle, 0f);
            Instantiate(fireballPrefab, firePoint.position, rotation);
        }
    }
}
