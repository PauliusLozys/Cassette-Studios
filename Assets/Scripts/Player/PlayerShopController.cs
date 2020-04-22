using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShopController : MonoBehaviour, IShopCustomer
{
    public Transform ShopUI;
    private UI_ShopDefence defenceShop;
    private UI_ShopOffence offenceShop;

    private PlayerCombatController playerCombat;
    private PlayerStats playerStats;

    private Interactable currentInteractableObject = null;
    private bool IsShopOpen = false;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        defenceShop = ShopUI.GetComponent<UI_ShopDefence>();
        offenceShop = ShopUI.GetComponent<UI_ShopOffence>();
        playerCombat = transform.GetComponent<PlayerCombatController>();
    }
    private void Update()
    {
        CheckInteraction();
    }
    private void CheckInteraction()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && currentInteractableObject != null)
        {
            IsShopOpen = true;
            playerCombat.SetCombat(false);
            if(currentInteractableObject.shopType == Interactable.ShopType.DefenceShop)
            {
                defenceShop.Show(this);
            }
            else if(currentInteractableObject.shopType == Interactable.ShopType.OffenceShop)
            {
                offenceShop.Show(this);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Q) && IsShopOpen)
        {
            Debug.Log("Shop closed");
            defenceShop.Hide();
            offenceShop.Hide();
            IsShopOpen = false;
            playerCombat.SetCombat(true);
        }
    }
    public void BoughtItem(Item.OffenceStat stat)
    {
        Debug.Log($"Bought offencsive upgrade");
        switch (stat)
        {
            
            default:
            case Item.OffenceStat.WeaponUpgrade:
                playerStats.SetPlayerDamage(playerStats.GetPlayerDamage() + 10f);
                break;
            case Item.OffenceStat.RangedUpgrade:
                playerStats.SetPlayerRangedDamage(playerStats.GetPlayerRangedDamage() + 10f);
                break;
        }
    }

    public void BoughtItem(Item.DefenceStat stat)
    {
        Debug.Log("Bought defensive upgrade");
        switch (stat)
        {
            default:
            case Item.DefenceStat.ArmourUpgrade:
                playerStats.SetPlayerDefence(playerStats.GetPlayerDefence() + 1);
                break;
            case Item.DefenceStat.HealthUpgrade:
                playerStats.SetPlayerMaxHealth(playerStats.GetPlayerMaxHealth() + 5);
                break;
            case Item.DefenceStat.AgilityUpgrade:
                playerStats.SetPlayerMovementSpeed(playerStats.GetPlayerMovementSpeed() + 0.5f);
                break;
            case Item.DefenceStat.JumpingUpgrade:
                playerStats.SetPlayerNumberOfJumps(playerStats.GetPlayerNumberOfJumps() + 1);
                break;

        }
    }

    public bool TrySpendGold(int goldAmount)
    {
        // Check with gold, when we have it
        return true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            currentInteractableObject = collision.GetComponent<Interactable>();
            currentInteractableObject.showPopup();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (currentInteractableObject != null)
        {
            defenceShop.Hide();
            offenceShop.Hide();
            IsShopOpen = false;
            playerCombat.SetCombat(true);
            currentInteractableObject.hidePopup();
            currentInteractableObject = null;
        }
    }

    public bool IsStatusCapReaced(Item.DefenceStat stat)
    {
        switch (stat)
        {
            default:
            case Item.DefenceStat.ArmourUpgrade:
                if (playerStats.GetPlayerDefence() >= playerStats.GetStatusCaps(stat))
                    return true;
                break;
            case Item.DefenceStat.HealthUpgrade:
                if (playerStats.GetPlayerMaxHealth() >= playerStats.GetStatusCaps(stat))
                    return true;
                break;
            case Item.DefenceStat.AgilityUpgrade:
                if (playerStats.GetPlayerMovementSpeed() >= playerStats.GetStatusCaps(stat))
                    return true;
                break;
            case Item.DefenceStat.JumpingUpgrade:
                if (playerStats.GetPlayerNumberOfJumps() >= playerStats.GetStatusCaps(stat))
                    return true;
                break;
        }
        return false;
    }

    public bool IsStatusCapReaced(Item.OffenceStat stat)
    {
        switch (stat)
        {
            default:
            case Item.OffenceStat.WeaponUpgrade:
                if (playerStats.GetPlayerDamage() >= playerStats.GetStatusCaps(stat))
                    return true;
                break;
        }
        return false;
    }
}
