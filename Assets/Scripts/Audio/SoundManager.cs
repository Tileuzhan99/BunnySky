using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioMixer mixer;
    public AudioSource musicA;
    public AudioSource musicB;
    public AudioSource sfx;
    public AudioClip sceneMusic;
    public AudioClip clickSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public float musicVolume;
    public float fadeTime;
    public float minPitch;
    public float maxPitch;

    AudioSource current;

    void Awake()
    {
        if (instance != null)
        {
            instance.PlayMusic(sceneMusic);
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        current = musicA;
    }

    void Start()
    {
        SetVolume("MusicVolume", SaveSystem.Data.musicVolume);
        SetVolume("SFXVolume", SaveSystem.Data.soundVolume);
        PlayMusic(sceneMusic);
    }

    public static void Play(AudioClip clip)
    {
        if (instance == null || clip == null) return;

        instance.sfx.pitch = Random.Range(instance.minPitch, instance.maxPitch);
        instance.sfx.PlayOneShot(clip);
    }

    public void SetVolume(string parameter, float value)
    {
        mixer.SetFloat(parameter, Mathf.Log10(value) * 20f);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null || clip == current.clip) return;

        StopAllCoroutines();
        StartCoroutine(Crossfade(clip));
    }

    IEnumerator Crossfade(AudioClip clip)
    {
        AudioSource old = current;
        if (current == musicA) current = musicB;
        else current = musicA;

        current.clip = clip;
        current.volume = 0f;
        current.Play();

        float time = 0f;
        while (time < fadeTime)
        {
            time += Time.unscaledDeltaTime;
            float part = Mathf.Clamp01(time / fadeTime);
            current.volume = part * musicVolume;
            old.volume = (1f - part) * musicVolume;
            yield return null;
        }

        old.Stop();
    }
}
