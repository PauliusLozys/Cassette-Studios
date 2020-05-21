using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public enum SpawnType
{
    ArcherEnemy,
    SkeletonEnemy,
    StrangeEnemy,
    GoldCoin,
    GoldChest
}
[System.Serializable]
public class LevelData
{
    /// <summary>
    /// Level build index
    /// </summary>
    public int LevelIndex { get; set; }

    // other level data

    /// <summary>
    /// A list of all spawnable types in a level
    /// </summary>
    public List<(Vector2 transform, bool IsDead, SpawnType type)> spawnambles { get; set; }
}
[System.Serializable]
public class LevelSaveData
{
    public List<int> levelsIndexs = new List<int>();
    public List<List<((float x, float y), bool, SpawnType)>> levels = new List<List<((float x, float y), bool, SpawnType)>>();
    public int currentNode;

    public LevelSaveData(LinkedList<LevelData> levels,LinkedListNode<LevelData> currentNode)
    {
      //  this.levels = levels.ToList();
        this.currentNode = currentNode.Value.LevelIndex;
        foreach (var item in levels)
        {
            levelsIndexs.Add(item.LevelIndex);
            var tmp = new List<((float x, float y), bool, SpawnType)>();

            if (item.spawnambles != null)
                foreach (var spawn in item.spawnambles)
                {
                    tmp.Add(((spawn.transform.x, spawn.transform.y), spawn.IsDead, spawn.type));    
                }

            this.levels.Add(tmp);
        }
    }

}


public static class LevelManager
{
    public static LinkedList<LevelData> levels;
    public static LinkedListNode<LevelData> currentLevelData;

    /// <summary>
    /// Indicates weather the player left the previous scene on the left side
    /// </summary>
    public static bool LeftOnLeft = false;

    public static void LoadLevelData()
    {
        // New Level generatiom

        levels = new LinkedList<LevelData>();
        System.Random rand = new System.Random();
        int[] levelIndexes = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }.OrderBy(x => rand.Next()).ToArray();

        var loadedLevels = new List<LevelData>();
        
        levels.AddFirst(new LevelData { LevelIndex = 3 }); // Adds the Dungeon entrance as first level

        LoadAllLevelSpawnables(loadedLevels);
        foreach (var item in levelIndexes)
        {
            levels.AddLast(loadedLevels[item]);
        }
        //foreach (var item in loadedLevels)
        //{
        //    levels.AddLast(item);
        //}
        levels.AddLast(new LevelData { LevelIndex = 12 }); // Adds the testing level at the end
        currentLevelData = levels.First;
    }
    public static void SetLeftLevelAsCurrent()
    {
        currentLevelData = currentLevelData.Previous;
    }
    public static void SetRightLevelAsCurrent()
    {
        currentLevelData = currentLevelData.Next;
    }
    private static void LoadAllLevelSpawnables(List<LevelData> levels)
    {
        // level 1 Data
        levels.Add(new LevelData
        {
            LevelIndex = 4,
            spawnambles = new List<(Vector2, bool, SpawnType)>
            {
                (new Vector2(-2.70f,9.5f),false,SpawnType.GoldChest),
                (new Vector2(5.3f,-6.5f),false,SpawnType.ArcherEnemy),
                (new Vector2(20f,8.5f),false,SpawnType.StrangeEnemy),
            }
        });

        // Level 2 Data
        levels.Add(new LevelData
        {
            LevelIndex = 5,
            spawnambles = new List<(Vector2, bool, SpawnType)>
            {
                (new Vector2(16.3f,4.5f),false,SpawnType.GoldChest),

                (new Vector2(11.2f,-2.7f),false,SpawnType.GoldCoin),
                (new Vector2(12.3f,-2.7f),false,SpawnType.GoldCoin),
                (new Vector2(13.3f,-2.7f),false,SpawnType.GoldCoin),
                (new Vector2(14.1f,-2.7f),false,SpawnType.GoldCoin),

                (new Vector2(4.3f,-5.5f),false,SpawnType.ArcherEnemy),
                (new Vector2(16f,-12.5f),false,SpawnType.StrangeEnemy),
            }
        });

        // Level 3 Data
        levels.Add(new LevelData
        {
            LevelIndex = 6,
            spawnambles = new List<(Vector2, bool, SpawnType)>
            {
                (new Vector2(-2.7f,8.5f),false,SpawnType.GoldChest), 
                (new Vector2(-7f,8.5f),false,SpawnType.StrangeEnemy),

                (new Vector2(15.2f,6.3f),false,SpawnType.GoldCoin), 
                (new Vector2(17.7f,6.3f),false,SpawnType.GoldCoin), 
                (new Vector2(-3.3f,12.4f),false,SpawnType.GoldCoin), 

                (new Vector2(3.56f,-2.47f),false,SpawnType.ArcherEnemy),
            }
        });

        // Level 4 Data
        levels.Add(new LevelData
        {
            LevelIndex = 7,
            spawnambles = new List<(Vector2, bool, SpawnType)>
            {

                (new Vector2(-15.4f,-3.51f),false,SpawnType.GoldChest),
                (new Vector2(-9.58f,-3.51f),false,SpawnType.StrangeEnemy),

                (new Vector2(-13.9f,-3.51f),false,SpawnType.GoldCoin),
                (new Vector2(-3.8f,-4.5f),false,SpawnType.GoldCoin),
                (new Vector2(-5.4f,-3.51f),false,SpawnType.GoldCoin),
            }
        });

        // Level 5 Data
        levels.Add(new LevelData
        {
            LevelIndex = 8,
            spawnambles = new List<(Vector2, bool, SpawnType)>
            {
                (new Vector2(-5.7f,14.3f),false,SpawnType.GoldCoin),
                (new Vector2(-6.7f, 14.3f),false,SpawnType.GoldCoin),
                (new Vector2(-7.7f, 14.3f),false,SpawnType.GoldCoin),
                
                (new Vector2(23.2f,0.5f),false,SpawnType.StrangeEnemy),
            }
        });

        // Level 6 Data
        levels.Add(new LevelData
        {
            LevelIndex = 9,
            spawnambles = new List<(Vector2, bool, SpawnType)>
            {
                (new Vector2(8.8f, -2.67f),false,SpawnType.GoldCoin),
                (new Vector2(18.5f, -2.67f),false,SpawnType.GoldCoin),
                (new Vector2(28f, -2.67f),false,SpawnType.GoldCoin),
                (new Vector2(37.3f, -2.67f),false,SpawnType.GoldCoin),


            }
        });

        // Level 7 Data
        levels.Add(new LevelData
        {
            LevelIndex = 10,
            spawnambles = new List<(Vector2, bool, SpawnType)>
            {
                (new Vector2(57.3f, -3.5f),false,SpawnType.GoldChest),
                (new Vector2(60.7f, -3.65f),false,SpawnType.GoldCoin),
                (new Vector2(61.7f, -3.65f),false,SpawnType.GoldCoin),
            }
        });

        // Level 8 Data
        levels.Add(new LevelData
        {
            LevelIndex = 11,
            spawnambles = new List<(Vector2, bool, SpawnType)>
            {
                (new Vector2(-15.9f, 13.31f),false,SpawnType.GoldChest),
                (new Vector2(-15.07f, 13.31f),false,SpawnType.GoldCoin),
                (new Vector2(-14.27f, 13.31f),false,SpawnType.GoldCoin),
                (new Vector2(-13.3f, 13.31f),false,SpawnType.GoldCoin),

                (new Vector2(21.23f, 7.55f),false,SpawnType.StrangeEnemy),
                (new Vector2(46f, 7.55f),false,SpawnType.StrangeEnemy),


            }
        });

    }
}
