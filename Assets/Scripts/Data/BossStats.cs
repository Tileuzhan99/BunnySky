using UnityEngine;

[CreateAssetMenu(menuName = "Bunny Sky/Boss Stats")]
public class BossStats : EnemyStats
{
    public float slowPause;
    public int slowCount;
    public float fastPause;
    public int fastCount;
    public float fastBelowHpPart;
    public float fanAngle;
    public float attackDelay;
}
