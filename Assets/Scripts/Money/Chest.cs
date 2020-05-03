using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private PlayerStats playerStats;
    public int chestValue = 1000;
    private Collider2D collider2D;

    private void Update()
    {
        if (collider2D != null && Input.GetKeyDown(KeyCode.E))
        {
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
