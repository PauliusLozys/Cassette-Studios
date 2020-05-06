using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private PlayerStats playerStats;
    public int coinValue = 100;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerStats.IncreaseMoney(coinValue);
            Debug.Log("Coin destroyed");
            Destroy(gameObject);
        }
    }
}
