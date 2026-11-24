using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStats stats;

    protected int hp;

    protected virtual void Awake()
    {
        hp = stats.maxHp;
    }

    public virtual void TakeHit(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            SoundManager.Play(stats.deathSound);
            Die();
        }
        else
        {
            SoundManager.Play(stats.hitSound);
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!stats.touchDamage) return;

        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(stats.contactDamage);
        }
    }
}
