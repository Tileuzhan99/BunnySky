using UnityEngine;

[CreateAssetMenu(menuName = "Bunny Sky/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public int maxHp;
    public bool touchDamage;
    public int contactDamage;
    public AudioClip windUpSound;
    public AudioClip attackSound;
    public AudioClip hitSound;
    public AudioClip deathSound;
}
