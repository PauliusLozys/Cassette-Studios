using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    
    [SerializeField]
    private float maxHealth;

    private float currentHealth;

    public EnemyHealthBar HealthBar;

    private void Start()
    {
        HealthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        Debug.Log(currentHealth);
        HealthBar.SetHealth(currentHealth);
        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }


}
