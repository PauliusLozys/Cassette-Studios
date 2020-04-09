using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField]
    private int maxHealth;

    private int currentHealth;

    public PlayerHealthBar playerHealthBar;

    private void Start()
    {
        playerHealthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;

    }

    public void DecreaseHealth(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        playerHealthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
