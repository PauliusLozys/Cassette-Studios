using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private PlayerStats playerStats;
    public int coinValue = 100;
    public int SpawnedIndex = -1;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (SpawnedIndex != -1)
                LevelManager.currentLevelData.Value.spawnambles[SpawnedIndex] = (LevelManager.currentLevelData.Value.spawnambles[SpawnedIndex].transform,
                                                                                 true,
                                                                                 LevelManager.currentLevelData.Value.spawnambles[SpawnedIndex].type);
            playerStats.IncreaseMoney(coinValue);
            Debug.Log("Coin destroyed");
            Destroy(gameObject);
        }
    }
}
