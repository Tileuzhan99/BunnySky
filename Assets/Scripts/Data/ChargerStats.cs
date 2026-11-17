using UnityEngine;

[CreateAssetMenu(menuName = "Bunny Sky/Charger Stats")]
public class ChargerStats : SmartEnemyStats
{
    public float wanderRadius;
    public float wanderSpeed;
    public float arriveDistance;
    public float triggerRange;
    public float windUp;
    public float chargeSpeed;
    public float chargeTime;
    public float hitDistance;
    public float tiredTime;
    public Color tiredColor;
    public int damage;
}
