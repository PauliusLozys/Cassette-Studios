using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadHub()
    {
        var data = SaveSystem.LoadLevels();
        if (data != null)
        {
            LevelManager.levels = new LinkedList<LevelData>();
            for (int i = 0; i < data.levelsIndexs.Count; i++)
            {
                var tmp = new LevelData();
                tmp.LevelIndex = data.levelsIndexs[i];
                tmp.spawnambles = new List<(Vector2 transform, bool IsDead, SpawnType type)>();

                foreach (var item in data.levels[i])
                {
                    tmp.spawnambles.Add((new Vector2(item.Item1.x, item.Item1.y), item.Item2, item.Item3));
                }
                LevelManager.levels.AddLast(tmp);
            }

            var CurrentNode = LevelManager.levels.First;
            while(CurrentNode.Value != null)
            {
                if(CurrentNode.Value.LevelIndex == data.currentNode)
                {
                    LevelManager.currentLevelData = CurrentNode;
                    break;
                }
                CurrentNode = CurrentNode.Next;
            }

            SceneManager.LoadScene(LevelManager.currentLevelData.Value.LevelIndex);

            return;
        }
        SceneManager.LoadScene(2);
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("CreditScene");
    }
}
