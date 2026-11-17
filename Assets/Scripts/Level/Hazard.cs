using UnityEngine;

public class Hazard : MonoBehaviour
{
    void OnCollisionStay(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(1);
        }
    }
}
