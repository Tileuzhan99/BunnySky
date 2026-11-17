using UnityEngine;

public class Portal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && player.coins >= player.coinsNeeded)
        {
            LevelLoader.current.CompleteLevel();
        }
    }
}
