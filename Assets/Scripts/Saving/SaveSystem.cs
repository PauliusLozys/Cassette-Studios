using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayer(int maxHealth, int currentHealth, int defence, int numberOfJumps, float playerRangedDamage, float playerRangedSpeed, float playerDamage, float movementSpeed)
    {
        string filePath = $"{Application.persistentDataPath}.hashtagbringbacktherealsimonas";
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            PlayerData playerData = new PlayerData(maxHealth, currentHealth, defence, numberOfJumps, playerRangedDamage, playerRangedSpeed, playerDamage, movementSpeed);
            formatter.Serialize(fs, playerData);
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
                //Debug.Log(filePath);
                return formatter.Deserialize(fs) as PlayerData;
            }
        }
        else
        {
            Debug.Log($"File not found at directory {filePath}");
            return null;
        }

    }

    public static void DeleteSave()
    {
        string filePath = $"{Application.persistentDataPath}.hashtagbringbacktherealsimonas";
        if (File.Exists(filePath))
            File.Delete(filePath);

        Debug.Log("Save deleted");
    }
}
