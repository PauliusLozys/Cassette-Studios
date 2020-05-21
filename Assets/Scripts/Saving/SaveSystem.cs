using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayer(int maxHealth, int currentHealth, int defence, int numberOfJumps, float playerRangedDamage, float playerRangedSpeed, float playerDamage, float movementSpeed, int money)
    {
        string filePath = $"{Application.persistentDataPath}.hashtagbringbacktherealsimonas";
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            PlayerData playerData = new PlayerData(maxHealth, currentHealth, defence, numberOfJumps, playerRangedDamage, playerRangedSpeed, playerDamage, movementSpeed, money);
            formatter.Serialize(fs, playerData);
        }
    }

    public static void SaveLevels()
    {
        if (LevelManager.currentLevelData is null)
            return;

        string filePath = $"{Application.persistentDataPath}.SaveSamaUwU";
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            LevelSaveData Leveldata = new LevelSaveData(LevelManager.levels,LevelManager.currentLevelData);
            formatter.Serialize(fs, Leveldata);
        }
    }

    public static PlayerData LoadSave()
    {
        string filePath = $"{Application.persistentDataPath}.hashtagbringbacktherealsimonas";
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                return formatter.Deserialize(fs) as PlayerData;
            }
        }
        else
        {
            Debug.Log($"File not found at directory {filePath}");
            return null;
        }

    }

    public static LevelSaveData LoadLevels()
    {
        string filePath = $"{Application.persistentDataPath}.SaveSamaUwU";
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                return formatter.Deserialize(fs) as LevelSaveData;
            }
        }
        else
        {
            Debug.Log($"File not found at directory {filePath}");
            return null;
        }

    }
    public static void DeletePlayerSave()
    {
        string PlayerfilePath = $"{Application.persistentDataPath}.hashtagbringbacktherealsimonas";
        if (File.Exists(PlayerfilePath))
            File.Delete(PlayerfilePath);

        Debug.Log("Saves deleted");
    }
    public static void DeleteLevelSave()
    {
        string LevelfilePath = $"{Application.persistentDataPath}.SaveSamaUwU";

        if (File.Exists(LevelfilePath))
            File.Delete(LevelfilePath);

        Debug.Log("Saves deleted");
    }
}
