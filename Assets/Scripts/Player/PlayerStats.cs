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
    
    [SerializeField]
    private int defence;

    [SerializeField]
    private int numberOfJumps;

    [SerializeField]
    private float playerDamage;

    [SerializeField]
    private float movementSpeed;
    
    [SerializeField]
    private int currentHealth;

    public PlayerHealthBar playerHealthBar;


    public int GetPlayerMaxHealth() => maxHealth;
    public int GetPlayerDefence() => defence;
    public int GetPlayerNumberOfJumps() => numberOfJumps;
    public float GetPlayerDamage() => playerDamage;
    public float GetPlayerMovementSpeed () => movementSpeed;
    public float GetPlayerRangedDamage() => playerRangedDamage;
    public float GetPlayerRangedSpeed() => playerRangedSpeed;

    public void SetPlayerMaxHealth(int value)
    {
        maxHealth = value;
    }
    public void SetPlayerDefence(int value)
    {
        defence = value;
    }
    public void SetPlayerNumberOfJumps(int value)
    {
        numberOfJumps = value;
    }
    public void SetPlayerDamage(float value)
    {
        playerDamage = value;
    }
    public void SetPlayerMovementSpeed(float value)
    {
        movementSpeed = value;
    }
    public void SetPlayerRangedDamage(float value)
    {
        playerRangedDamage = value;
    }
    public void SetPlayerRangedSpeed(float value)
    {
        playerRangedSpeed = value;
    }
    public float GetStatusCaps(Item.DefenceStat stat)
    {
        switch (stat)
        {
            default:
            case Item.DefenceStat.ArmourUpgrade:
                return 10;
            case Item.DefenceStat.HealthUpgrade:
                return 300;
            case Item.DefenceStat.AgilityUpgrade:
                return 15;
            case Item.DefenceStat.JumpingUpgrade:
                return 4;
        }
    }
    public float GetStatusCaps(Item.OffenceStat stat)
    {
        switch (stat)
        {
            default:
            case Item.OffenceStat.WeaponUpgrade:
                return 50;
            case Item.OffenceStat.RangedUpgrade:
                return 40;
        }
    }


    private void Start()
    {
        playerHealthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }

    public void DecreaseHealth(int damage)
    {
        // Damage reduction
        var newDamage = damage - (defence * 0.75f);
        
        currentHealth -= (int)newDamage;
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
