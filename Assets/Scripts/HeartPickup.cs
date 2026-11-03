using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.Heal(1);
            Destroy(gameObject);
        }
    }
}
