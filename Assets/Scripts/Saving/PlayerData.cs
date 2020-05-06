using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int money;
    public int maxHealth;
    public int currentHealth;
    public int defence;
    public int numberOfJumps;
    public float playerRangedDamage;
    public float playerRangedSpeed;
    public float playerDamage;
    public float movementSpeed;

    public PlayerData(int maxHealth, int currentHealth, int defence, int numberOfJumps, float playerRangedDamage, float playerRangedSpeed, float playerDamage, float movementSpeed, int money)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
        this.defence = defence;
        this.numberOfJumps = numberOfJumps;
        this.playerRangedDamage = playerRangedDamage;
        this.playerRangedSpeed = playerRangedSpeed;
        this.playerDamage = playerDamage;
        this.movementSpeed = movementSpeed;
        this.money = money;
    }
}