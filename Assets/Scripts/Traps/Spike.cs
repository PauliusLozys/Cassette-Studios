using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private PlayerStats playerStats;
    private PlayerController player;
    int damage = 10;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Spike damage");
            playerStats.DecreaseHealth(damage);
            StartCoroutine(player.Knockback(0.02f, 1300, player.transform.position));
        }
    }
}
