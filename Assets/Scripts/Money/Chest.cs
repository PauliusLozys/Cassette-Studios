using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public int chestValue = 1000;
    public int SpawnedIndex = -1;
    private PlayerStats playerStats;
    private Collider2D collider2D;
    
    private void Update()
    {
        if (collider2D != null && Input.GetKeyDown(KeyCode.E))
        {
            if (SpawnedIndex != -1)
                LevelManager.currentLevelData.Value.spawnambles[SpawnedIndex] = (LevelManager.currentLevelData.Value.spawnambles[SpawnedIndex].transform,
                                                                                 true,
                                                                                 LevelManager.currentLevelData.Value.spawnambles[SpawnedIndex].type);
            playerStats.IncreaseMoney(chestValue);
            Debug.Log("Chest destroyed");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collider2D = other;
        }
    }
}
