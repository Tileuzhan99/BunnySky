using UnityEngine;

[System.Serializable]
public class SaveData
{
    public const int LevelCount = 3;

    public int unlockedLevels = 1;
    public float[] bestTimes = new float[LevelCount];
    public bool bossBeaten;
    public float musicVolume = 1f;
    public float soundVolume = 1f;

    public void Fix()
    {
        if (bestTimes == null || bestTimes.Length != LevelCount)
        {
            bestTimes = new float[LevelCount];
        }
        unlockedLevels = Mathf.Clamp(unlockedLevels, 1, LevelCount + 1);
        musicVolume = Mathf.Clamp01(musicVolume);
        soundVolume = Mathf.Clamp01(soundVolume);
    }
}
