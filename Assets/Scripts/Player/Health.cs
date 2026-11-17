using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHp = 3;
    public int hp;
    public Image[] hearts;
    public AudioClip hurtSound;
    public AudioClip healSound;

    bool invincible;
    Renderer[] renderers;
    AudioSource audioSource;

    void Start()
    {
        hp = maxHp;
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
        if (hp > maxHp) hp = maxHp;
        ShowHearts();
        audioSource.PlayOneShot(healSound);
    }

    void ShowHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < hp) hearts[i].color = Color.white;
            else hearts[i].color = new Color(0f, 0f, 0f, 0.4f);
        }
    }

    IEnumerator Blink()
    {
        invincible = true;

        for (int i = 0; i < 10; i++)
        {
            foreach (Renderer r in renderers)
            {
                r.enabled = !r.enabled;
            }
            yield return new WaitForSeconds(0.15f);
        }

        foreach (Renderer r in renderers)
        {
            r.enabled = true;
        }

        invincible = false;
    }
}
