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

    void Start()
    {
        hp = stats.maxHp;
        renderers = GetComponentsInChildren<Renderer>();
        ShowHearts();
    }

    public void TakeDamage(int damage)
    {
        if (invincible) return;

        hp -= damage;
        ShowHearts();
        if (hp <= 0)
        {
            Die();
        }
        else
        {
            SoundManager.Play(hurtSound);
            StartCoroutine(Blink());
        }
    }

    public void Heal(int amount)
    {
        hp += amount;
        if (hp > stats.maxHp) hp = stats.maxHp;
        ShowHearts();
        SoundManager.Play(healSound);
    }

    public void Die()
    {
        SoundManager.Play(SoundManager.instance.loseSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
