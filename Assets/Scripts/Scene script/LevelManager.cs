using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class LevelData
{
    public int LevelIndex { get; set; }

    // other level data
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
        System.Random rand = new System.Random();
        int[] levelIndexes = new int[] { 3, 4, 5, 6, 7 }.OrderBy(x => rand.Next()).ToArray();
        //levelIndexes = levelIndexes.OrderBy(x => rand.Next()).ToArray();
        levels = new LinkedList<LevelData>();
        levels.AddFirst(new LevelData { LevelIndex = 2 }); // Adds the Dungeon entrance as first level
        foreach (var item in levelIndexes)
        {
            levels.AddLast(new LevelData { LevelIndex = item });
        }
        levels.AddLast(new LevelData { LevelIndex = 8 }); // Adds the testing level at the end
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
}
