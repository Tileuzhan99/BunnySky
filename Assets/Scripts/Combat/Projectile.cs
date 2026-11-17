using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileData data;

    void Start()
    {
        Destroy(gameObject, data.lifeTime);
    }

    void Update()
    {
        transform.position += transform.forward * data.speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        Hit(other);
    }

    protected virtual void Hit(Collider other)
    {
    }
}
