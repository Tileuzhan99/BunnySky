using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 1;

    public virtual void TakeHit()
    {
        hp--;
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
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(1);
        }
    }
}
