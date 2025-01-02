using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Score;
    public string PlayerName;
}

public class DataManager : MonoBehaviour
{
    private static string filePath => Path.Combine(Application.persistentDataPath, "PlayerData.json");

    public static void SaveData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        Debug.Log($"Data saved to {filePath}");
    }

    public static PlayerData LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            Debug.LogWarning("No data file found, returning default data.");
            return new PlayerData { Score = 0, PlayerName = "Player" };
        }
    }
}
