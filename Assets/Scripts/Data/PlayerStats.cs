using UnityEngine;

[CreateAssetMenu(menuName = "Bunny Sky/Player Stats")]
public class PlayerStats : ScriptableObject
{
    public float moveSpeed;
    public float jumpForce;
    public float fireCooldown;
    public int maxHp;
    public float invincibleTime;
    public float blinkInterval;
    public float groundCheckHeight;
    public float groundCheckDistance;
    public float fallLimit;
}
