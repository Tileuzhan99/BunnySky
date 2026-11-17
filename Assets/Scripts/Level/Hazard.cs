using UnityEngine;

public class Hazard : MonoBehaviour
{
    public ItemValues items;

    void OnCollisionStay(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(items.spikeDamage);
        }
    }
}
