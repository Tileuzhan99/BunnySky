using UnityEngine;

[CreateAssetMenu(menuName = "Bunny Sky/Projectile Data")]
public class ProjectileData : ScriptableObject
{
    public float speed;
    public float lifeTime;
    public int damage;
}
