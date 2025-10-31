using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string FilePath =>
        Path.Combine(Application.persistentDataPath, "save.json");

    [System.Serializable]
    public class SaveData
    {
        public int totalItems = 0;     // cumulative “bags collected”
        public int highestLevelReached = 1; // optional (we’ll gate by items anyway)
    }

    public static SaveData Data { get; private set; } = new SaveData();

    public static void Load()
    {
        try
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                Data = JsonUtility.FromJson<SaveData>(json) ?? new SaveData();
            }
            else
            {
                Data = new SaveData();
                Save(); // create first file
            }
            // Safety clamp
            if (Data.highestLevelReached < 1) Data.highestLevelReached = 1;
            if (Data.totalItems < 0) Data.totalItems = 0;
        }
        catch
        {
            // Fallback on any error
            Data = new SaveData();
        }
    }

    public static void Save()
    {
        string json = JsonUtility.ToJson(Data, true);
        File.WriteAllText(FilePath, json);
    }

    public static void Reset()
    {
        Data = new SaveData();
        Save();
    }
}
