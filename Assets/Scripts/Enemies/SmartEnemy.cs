using UnityEngine;

public class SmartEnemy : Enemy
{
    public float visionRange = 8f;
    public float visionAngle = 120f;
    public float eyeHeight = 0.5f;
    public float playerHeight = 0.8f;
    public AudioClip warnSound;
    public Color warnColor = Color.red;
    public float warnTint = 0.7f;
    public float fadeTime = 0.1f;

    protected Transform player;
    protected Animator anim;

    AudioSource audioSource;
    Material[] materials;
    Color[] baseColors;

    protected virtual void Start()
    {
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

    protected bool CanSeePlayer()
    {
        Vector3 eye = transform.position + Vector3.up * eyeHeight;
        Vector3 target = player.position + Vector3.up * playerHeight;
        Vector3 toPlayer = target - eye;

        if (toPlayer.magnitude > visionRange) return false;

        Vector3 flat = new Vector3(toPlayer.x, 0f, toPlayer.z);
        if (Vector3.Angle(transform.forward, flat) > visionAngle / 2f) return false;

        RaycastHit hit;
        if (Physics.Raycast(eye, toPlayer.normalized, out hit, visionRange, ~0, QueryTriggerInteraction.Ignore))
        {
            return hit.transform == player;
        }
        return false;
    }

    protected bool OnScreen()
    {
        Vector3 v = Camera.main.WorldToViewportPoint(transform.position + Vector3.up * eyeHeight);
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
        Tint(warnColor, warnTint);
        if (warnSound != null) audioSource.PlayOneShot(warnSound);
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
        anim.CrossFade(clip, fadeTime);
    }
}
