using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject pauseUI;
    [SerializeField]
    private int money;

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

    public int GetPlayerMoney() => money;
    public int GetPlayerMaxHealth() => maxHealth;
    public int GetPlayerCurrentHealth() => currentHealth;
    public int GetPlayerDefence() => defence;
    public int GetPlayerNumberOfJumps() => numberOfJumps;
    public float GetPlayerDamage() => playerDamage;
    public float GetPlayerMovementSpeed () => movementSpeed;
    public float GetPlayerRangedDamage() => playerRangedDamage;
    public float GetPlayerRangedSpeed() => playerRangedSpeed;

    public void SetPlayerMoney(int value)
    {
        money = value;
    }
    public void SetPlayerMaxHealth(int value)
    {
        maxHealth = value;
    }
    public void SetPlayerCurrentHealth(int value)
    {
        currentHealth = value;
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
    private void Awake()
    {
        PlayerData data = SaveSystem.LoadSave();
        pauseUI = GameObject.Find("Canvas").transform.Find("DeathScreen").gameObject;
        if(data != null && SceneManager.GetActiveScene().name != "TutorialScene") // If save file exists AND its not a tutorial level
        {
            maxHealth = data.maxHealth;
            currentHealth = data.currentHealth;
            playerDamage = data.playerDamage;
            playerRangedDamage = data.playerRangedDamage;
            playerRangedSpeed = data.playerRangedSpeed;
            defence = data.defence;
            numberOfJumps = data.numberOfJumps;
            movementSpeed = data.movementSpeed;
            money = data.money;
            Debug.Log("File succsessfully loaded");
        }
        else // if save file is not found, assing defaul values
        {
            SetDefaultStats();
        }

        text.text = money.ToString();
        playerHealthBar.SetMaxHealth(maxHealth);
        
        if (SceneManager.GetActiveScene().name == "HubScene")
            currentHealth = maxHealth;
            
        playerHealthBar.SetHealth(currentHealth);
    }

    /// <summary>
    /// Call this function only when a new game is initiated
    /// </summary>
    public void SetDefaultStats()
    {
        maxHealth = 100;
        playerDamage = 10;
        playerRangedDamage = 10;
        playerRangedSpeed = 10;
        defence = 0;
        numberOfJumps = 1;
        movementSpeed = 10;
        currentHealth = maxHealth;
        money = 0;
    }

    private void OnDestroy()
    {
        if(SceneManager.GetActiveScene().name != "TutorialScene")
            SaveSystem.SavePlayer(maxHealth, currentHealth, defence, numberOfJumps, playerRangedDamage, playerRangedSpeed, playerDamage, movementSpeed, money);
    }
    public void DecreaseHealth(int damage)
    {
        // Damage reduction
        if (defence > 0)
            currentHealth -= (int)(damage * (defence / 100.0));
        else
            currentHealth -= damage;

        //Debug.Log(currentHealth);

        playerHealthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Delete Level data
        pauseUI.SetActive(true);
        currentHealth = maxHealth;
        LevelManager.isPlayerDead = true;
        SaveSystem.DeleteLevelSave();
        Destroy(gameObject);
        
    }

    public void IncreaseMoney(int amount)
    {
        money += amount;
        text.text = money.ToString();
    }

    public void DecreaseMoney(int amount)
    {
        money -= amount;
        text.text = money.ToString();
    }
}
