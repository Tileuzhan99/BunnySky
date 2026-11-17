using UnityEngine;

[CreateAssetMenu(menuName = "Bunny Sky/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public int maxHp;
    public bool touchDamage;
    public int contactDamage;
}
