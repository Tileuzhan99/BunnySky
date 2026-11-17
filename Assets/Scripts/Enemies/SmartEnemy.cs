using UnityEngine;

public class SmartEnemy : Enemy
{
    public AudioClip warnSound;

    protected Transform player;
    protected Animator anim;

    SmartEnemyStats senses;
    AudioSource audioSource;
    Material[] materials;
    Color[] baseColors;

    protected virtual void Start()
    {
        senses = (SmartEnemyStats)stats;
        player = GameObject.FindWithTag("Player").transform;
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        int count = 0;
        foreach (Renderer r in renderers) count += r.materials.Length;
        materials = new Material[count];
        baseColors = new Color[count];
        int i = 0;
        foreach (Renderer r in renderers)
        {
            foreach (Material m in r.materials)
            {
                materials[i] = m;
                baseColors[i] = m.color;
                i++;
            }
        }
    }

    protected float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.position);
    }

    protected Vector3 PlayerChest()
    {
        return player.position + Vector3.up * senses.playerHeight;
    }

    protected bool CanSeePlayer()
    {
        Vector3 eye = transform.position + Vector3.up * senses.eyeHeight;
        Vector3 toPlayer = PlayerChest() - eye;

        if (toPlayer.magnitude > senses.visionRange) return false;

        Vector3 flat = new Vector3(toPlayer.x, 0f, toPlayer.z);
        if (Vector3.Angle(transform.forward, flat) > senses.visionAngle / 2f) return false;

        RaycastHit hit;
        if (Physics.Raycast(eye, toPlayer.normalized, out hit, senses.visionRange, ~0, QueryTriggerInteraction.Ignore))
        {
            return hit.transform == player;
        }
        return false;
    }

    protected bool OnScreen()
    {
        Vector3 v = Camera.main.WorldToViewportPoint(transform.position + Vector3.up * senses.eyeHeight);
        return v.z > 0f && v.x > 0f && v.x < 1f && v.y > 0f && v.y < 1f;
    }

    protected void FacePlayer()
    {
        Vector3 look = player.position;
        look.y = transform.position.y;
        transform.LookAt(look);
    }

    protected void HurtPlayer(int damage)
    {
        player.GetComponent<Health>().TakeDamage(damage);
    }

    protected void Warn()
    {
        Tint(senses.warnColor, senses.warnTint);
        if (warnSound != null) audioSource.PlayOneShot(warnSound);
    }

    protected void ClearTint()
    {
        Tint(senses.warnColor, 0f);
    }

    protected void Tint(Color color, float amount)
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = Color.Lerp(baseColors[i], color, amount);
        }
    }

    protected void Play(string clip)
    {
        anim.CrossFade(clip, senses.fadeTime);
    }
}
