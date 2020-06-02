using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawningPlace : MonoBehaviour
{
    [SerializeField]
    public bool IsLeft = false;

    public Transform player;

    void Start()
    {
        Debug.Log($"Current level: {SceneManager.GetActiveScene().name}");
        if (!LevelManager.LeftOnLeft && IsLeft)
        {
            player.position = transform.position;
        }
        else if (LevelManager.LeftOnLeft && !IsLeft)
        {
            player.position = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (IsLeft)
            {
                LevelManager.LeftOnLeft = true;
                // Go to the left scene
                if(LevelManager.currentLevelData.Previous != null)
                {
                    LevelManager.SetLeftLevelAsCurrent();
                    SceneManager.LoadScene(LevelManager.currentLevelData.Value.LevelIndex);
                }
            }
            else
            {
                LevelManager.LeftOnLeft = false;
                // Go to the right scene
                if (LevelManager.currentLevelData.Next != null)
                {
                    LevelManager.SetRightLevelAsCurrent();
                    SceneManager.LoadScene(LevelManager.currentLevelData.Value.LevelIndex);
                }
            }
        }


    }
}
