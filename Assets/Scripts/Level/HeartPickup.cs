using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public ItemValues items;

    void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.Heal(items.heartHeal);
            Destroy(gameObject);
        }
    }
}
