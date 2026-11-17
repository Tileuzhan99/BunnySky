using UnityEngine;

[CreateAssetMenu(menuName = "Bunny Sky/Patroller Stats")]
public class PatrollerStats : SmartEnemyStats
{
    public float patrolSpeed;
    public float chaseSpeed;
    public float arriveDistance;
    public float loseSightTime;
    public float attackRange;
    public float windUp;
    public float cooldown;
    public int damage;
}
