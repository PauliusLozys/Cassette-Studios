using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShopController : MonoBehaviour, IShopCustomer
{
    public Transform ShopUI;

    private PlayerCombatController playerCombat;
    private PlayerStats playerStats;

    private BaseShop currentShop = null;
    private bool IsShopOpen = false;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        playerCombat = transform.GetComponent<PlayerCombatController>();
    }
    private void Update()
    {
        CheckInteraction();
    }
    private void CheckInteraction()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && currentShop != null)
        {
            IsShopOpen = true;
            playerCombat.SetCombat(false);

            currentShop.ShowShop(this);
        }
        else if(Input.GetKeyDown(KeyCode.Q) && IsShopOpen)
        {
            Debug.Log("Shop closed");
            currentShop.HideShop();
            IsShopOpen = false;
            playerCombat.SetCombat(true);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            playerStats.IncreaseMoney(10_000);
        }
        
    }
    public bool TrySpendGold(int goldAmount)
    {
        if (playerStats.GetPlayerMoney() - goldAmount >= 0)
        {
            playerStats.DecreaseMoney(goldAmount);
            return true;
        }
        return false;
    }
    public void BoughtItem(BaseShop.UpgradeStats stats)
    {
        switch (stats)
        {
            default:
            case BaseShop.UpgradeStats.ArmourUpgrade:
                playerStats.SetPlayerDefence(playerStats.GetPlayerDefence() + 1);
                break;
            case BaseShop.UpgradeStats.HealthUpgrade:
                playerStats.SetPlayerMaxHealth(playerStats.GetPlayerMaxHealth() + 5);
                playerStats.SetPlayerCurrentHealth(playerStats.GetPlayerCurrentHealth() + 5);
                break;
            case BaseShop.UpgradeStats.AgilityUpgrade:
                playerStats.SetPlayerMovementSpeed(playerStats.GetPlayerMovementSpeed() + 0.2f);
                break;
            case BaseShop.UpgradeStats.JumpingUpgrade:
                playerStats.SetPlayerNumberOfJumps(playerStats.GetPlayerNumberOfJumps() + 1);
                break;
            case BaseShop.UpgradeStats.WeaponUpgrade:
                playerStats.SetPlayerDamage(playerStats.GetPlayerDamage() + 5f);
                break;
            case BaseShop.UpgradeStats.RangedUpgrade:
                playerStats.SetPlayerRangedDamage(playerStats.GetPlayerRangedDamage() + 2f);
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            currentShop = collision.GetComponent<BaseShop>(); 
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (currentShop != null)
        {
            IsShopOpen = false;
            currentShop.HideShop();
            playerCombat.SetCombat(true);
            currentShop = null;
        }
    }
}
