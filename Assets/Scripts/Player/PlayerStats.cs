using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private float playerRangedDamage;

    [SerializeField]
    private float playerRangedSpeed;

    private int currentHealth;

    public PlayerHealthBar playerHealthBar;

    public float GetPlayerRangedDamage() => playerRangedDamage;
    public float GetPlayerRangedSpeed() => playerRangedSpeed;

    public void SetPlayerRangedDamage(float value)
    {
        playerRangedDamage = value;
    }
    public void SetPlayerRangedSpeed(float value)
    {
        playerRangedSpeed = value;
    }


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
