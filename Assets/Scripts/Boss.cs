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

    int maxHp;
    Animator anim;
    AudioSource audioSource;

    void Start()
    {
        maxHp = hp;
        hpBar.maxValue = maxHp;
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

    public override void TakeHit()
    {
        audioSource.PlayOneShot(hitSound);
        base.TakeHit();
        hpBar.value = hp;
    }

    protected override void Die()
    {
        SceneManager.LoadScene("Win");
    }

    IEnumerator Attack()
    {
        while (true)
        {
            float pause = 2.5f;
            int count = 3;

            if (hp <= maxHp / 2)
            {
                pause = 1.2f;
                count = 7;
            }

            yield return new WaitForSeconds(pause);

            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(0.5f);

            ShootFan(count);
        }
    }

    void ShootFan(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float angle = (i - count / 2) * 15f;
            Quaternion rotation = firePoint.rotation * Quaternion.Euler(0f, angle, 0f);
            Instantiate(fireballPrefab, firePoint.position, rotation);
        }
    }
}
