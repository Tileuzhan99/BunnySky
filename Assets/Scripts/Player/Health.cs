using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public PlayerStats stats;
    public Image[] hearts;
    public Color emptyHeartColor;
    public AudioClip hurtSound;
    public AudioClip healSound;

    public int hp;

    bool invincible;
    Renderer[] renderers;
    AudioSource audioSource;

    void Start()
    {
        hp = stats.maxHp;
        renderers = GetComponentsInChildren<Renderer>();
        audioSource = GetComponent<AudioSource>();
        ShowHearts();
    }

    public void TakeDamage(int damage)
    {
        if (invincible) return;

        hp -= damage;
        ShowHearts();
        audioSource.PlayOneShot(hurtSound);

        if (hp <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            StartCoroutine(Blink());
        }
    }

    public void Heal(int amount)
    {
        hp += amount;
        if (hp > stats.maxHp) hp = stats.maxHp;
        ShowHearts();
        audioSource.PlayOneShot(healSound);
    }

    void ShowHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < hp) hearts[i].color = Color.white;
            else hearts[i].color = emptyHeartColor;
        }
    }

    IEnumerator Blink()
    {
        invincible = true;

        float time = 0f;
        while (time < stats.invincibleTime)
        {
            foreach (Renderer r in renderers)
            {
                r.enabled = !r.enabled;
            }
            yield return new WaitForSeconds(stats.blinkInterval);
            time += stats.blinkInterval;
        }

        foreach (Renderer r in renderers)
        {
            r.enabled = true;
        }

        invincible = false;
    }
}
