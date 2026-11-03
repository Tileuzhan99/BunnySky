using UnityEngine;

public class Bullet : Projectile
{
    protected override void Hit(Collider other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeHit();
            Destroy(gameObject);
        }
    }
}
