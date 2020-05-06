using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    
    [SerializeField]
    private float maxHealth;

    private float currentHealth;

    public EnemyHealthBar HealthBar;

    private PlayerStats playerStats;
    //private Animator anim; 

    private void Start()
    {
        HealthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        //anim = GetComponent<Animator>();
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        //anim.SetBool("isHit", true);
        Debug.Log(currentHealth);
        HealthBar.SetHealth(currentHealth);
        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // Get that fat cash
        int random = Random.Range(1, 100);
        if (random > 35)
        {
            playerStats.IncreaseMoney(random / 2);
        }
        
        Destroy(gameObject);
    }

    //public void FinnishAnimation()
    //{
    //    anim.SetBool("isHit", false);
    //}
}
