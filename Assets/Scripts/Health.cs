using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Health : MonoBehaviour
{
    public int maxHp = 3;
    public int hp;
    public TextMeshProUGUI hpText;
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
        hpText.text = "x " + hp;
    }

    public void TakeDamage(int damage)
    {
        if (invincible) return;

        hp -= damage;
        hpText.text = "x " + hp;
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
        hpText.text = "x " + hp;
        audioSource.PlayOneShot(healSound);
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
