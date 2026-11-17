using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static string message = "";

    static SaveData data;

    public static string FilePath
    {
        get { return Path.Combine(Application.persistentDataPath, "save.json"); }
    }

    public static SaveData Data
    {
        get
        {
            if (data == null) Load();
            return data;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        Load();
        AudioListener.volume = data.soundVolume;
        GameObject go = new GameObject("SaveOnPause");
        Object.DontDestroyOnLoad(go);
        go.AddComponent<SaveOnPause>();
    }

    public static void Load()
    {
        data = new SaveData();
        message = "";

        if (!File.Exists(FilePath)) return;

        try
        {
            SaveData loaded = JsonUtility.FromJson<SaveData>(File.ReadAllText(FilePath));
            if (loaded == null) throw new System.Exception("file is empty");
            loaded.Fix();
            data = loaded;
        }
        catch (System.Exception e)
        {
            data = new SaveData();
            message = "Save file was broken - starting with default progress";
            Debug.LogWarning("Broken save file, using defaults: " + e.Message);
        }
    }

    public static void Save()
    {
        try
        {
            File.WriteAllText(FilePath, JsonUtility.ToJson(Data, true));
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Could not save: " + e.Message);
        }
    }

    public static float CompleteLevel(int index, float time)
    {
        SaveData d = Data;
        if (d.bestTimes[index] <= 0f || time < d.bestTimes[index])
        {
            d.bestTimes[index] = time;
        }
        if (d.unlockedLevels < index + 2)
        {
            d.unlockedLevels = index + 2;
        }
        Save();
        return d.bestTimes[index];
    }

    public static void BossBeaten()
    {
        Data.bossBeaten = true;
        Save();
    }

    public static void ResetProgress()
    {
        SaveData fresh = new SaveData();
        fresh.musicVolume = Data.musicVolume;
        fresh.soundVolume = Data.soundVolume;
        data = fresh;
        Save();
    }
}
