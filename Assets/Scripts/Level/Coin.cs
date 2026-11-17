using UnityEngine;

public class Coin : MonoBehaviour
{
    public ItemValues items;

    void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.AddCoin(items.coinValue);
            Destroy(gameObject);
        }
    }
}
