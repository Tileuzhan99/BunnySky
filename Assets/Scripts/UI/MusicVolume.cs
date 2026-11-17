using UnityEngine;

public class MusicVolume : MonoBehaviour
{
    AudioSource source;
    float baseVolume;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        baseVolume = source.volume;
    }

    void Update()
    {
        source.volume = baseVolume * SaveSystem.Data.musicVolume;
    }
}
