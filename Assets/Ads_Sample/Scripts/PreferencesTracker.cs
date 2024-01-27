using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class PreferencesTracker : MonoBehaviour
{
    private const string jsonFilePath = "Assets/Preferences/preferences.json"; // Change the path as needed
    private Preferences preferences;

    void Start()
    {
        LoadPreferences();
    }

    public void LogPreference(string colliderType)
    {
        colliderType = colliderType.ToLower();

        // Find the collider in the preferences
        ColliderData colliderData = preferences.preferences.Find(c => c.type == colliderType);

        if (colliderData != null)
        {
            colliderData.count++;
        }
        else
        {
            // If the collider type doesn't exist in preferences, add it
            ColliderData newColliderData = new ColliderData { type = colliderType, count = 1 };
            preferences.preferences.Add(newColliderData);
        }

        SavePreferences();
    }

    void LoadPreferences()
    {
        if (File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(jsonFilePath);
            preferences = JsonUtility.FromJson<Preferences>(json);
        }
        else
        {
            // If the file doesn't exist, create a new preferences object with an empty list
            preferences = new Preferences { preferences = new List<ColliderData>() };
        }
    }

    void SavePreferences()
    {
        string json = JsonUtility.ToJson(preferences, true);
        File.WriteAllText(jsonFilePath, json);
    }
}

[System.Serializable]
public class ColliderData
{
    public string type;
    public int count;
}

[System.Serializable]
public class Preferences
{
    public List<ColliderData> preferences = new List<ColliderData>();
}
