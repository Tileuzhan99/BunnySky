using UnityEngine;

public class Fireball : Projectile
{
    protected override void Hit(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(data.damage);
            Destroy(gameObject);
        }
    }
}
