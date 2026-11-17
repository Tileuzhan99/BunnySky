using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundSlider;

    void OnEnable()
    {
        musicSlider.SetValueWithoutNotify(SaveSystem.Data.musicVolume);
        soundSlider.SetValueWithoutNotify(SaveSystem.Data.soundVolume);
    }

    public void SetMusic(float value)
    {
        SaveSystem.Data.musicVolume = value;
        SaveSystem.Save();
    }

    public void SetSound(float value)
    {
        SaveSystem.Data.soundVolume = value;
        AudioListener.volume = value;
        SaveSystem.Save();
    }
}
