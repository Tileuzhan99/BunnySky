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
            Die();
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
