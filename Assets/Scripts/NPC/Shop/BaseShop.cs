using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseShop : MonoBehaviour
{
    protected List<Upgrades> upgrades = new List<Upgrades>();
    protected List<Transform> buttons = new List<Transform>();
    protected IShopCustomer customer;
    protected PlayerStats playerStats;
    private Transform container;
    private Transform shopItemTemplate;

    /// <summary>
    ///     IMPORTANT !!!!
    /// 
    ///     To add more items to the shop
    ///     You need to add them to "UpgradeStats", "LoadAllUpdates" and "GetCurrentStatInfo"
    /// 
    /// </summary>

    public enum UpgradeStats
    {
        // Add shop upgrades as enums here
        HealthUpgrade,
        ArmourUpgrade,
        AgilityUpgrade,
        JumpingUpgrade,

        WeaponUpgrade,
        RangedUpgrade
    }
    protected void LoadAllUpgrades()
    {
        // Add shop upgrades into a list here
        upgrades.Add(new Upgrades(UpgradeStats.HealthUpgrade, "Health upgrade", "Current health:", 100, 400, true, GameAssets.i.HealthIconSprite));
        upgrades.Add(new Upgrades(UpgradeStats.ArmourUpgrade, "Defence upgrade", "Current defence:", 200, 20, true, GameAssets.i.ArmourIconSprite));
        upgrades.Add(new Upgrades(UpgradeStats.AgilityUpgrade, "Agility upgrade", "Current movement speed:", 200, 13.0f, true, GameAssets.i.AgilityIconSprite));
        upgrades.Add(new Upgrades(UpgradeStats.JumpingUpgrade, "Jumping upgrade", "Current number of Jumps:", 2000, 2, true, GameAssets.i.JumpingIconSprite));

        upgrades.Add(new Upgrades(UpgradeStats.WeaponUpgrade, "Weapon upgrade", "Current melee damage:", 100, 80, false, GameAssets.i.WeaponIconSprite));
        upgrades.Add(new Upgrades(UpgradeStats.RangedUpgrade, "Bow upgrade", "Current range damage:", 150, 50, false, GameAssets.i.RangedIconSprite));
    }
    protected float GetCurrentStatInfo(UpgradeStats upgrades)
    {
        // Gets the coresponding player stat
        switch (upgrades)
        {
            default:
            case UpgradeStats.ArmourUpgrade:
                return playerStats.GetPlayerDefence();
            case UpgradeStats.HealthUpgrade:
                return playerStats.GetPlayerMaxHealth();
            case UpgradeStats.AgilityUpgrade:
                return playerStats.GetPlayerMovementSpeed();
            case UpgradeStats.JumpingUpgrade:
                return playerStats.GetPlayerNumberOfJumps();
            case UpgradeStats.WeaponUpgrade:
                return playerStats.GetPlayerDamage();
            case UpgradeStats.RangedUpgrade:
                return playerStats.GetPlayerRangedDamage();
        }
    }
    private void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        container = GameObject.Find("container").GetComponent<Transform>();
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
        LoadAllUpgrades();
        LoadShopUpgrades();
    }
    protected Transform CreateButton(Upgrades upgrade, int shopIndex)
    {
        UpdateItemPrice(upgrade);
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 100f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * shopIndex);

        shopItemTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(upgrade.UpgradeName);
        shopItemTransform.Find("priceText").GetComponent<TextMeshProUGUI>().SetText(upgrade.Price.ToString());
        shopItemTransform.Find("itemIcon").GetComponent<Image>().sprite = upgrade.Sprite;
        if (GetCurrentStatInfo(upgrade.Stat) < upgrade.ItemCap && upgrade.Price <= playerStats.GetPlayerMoney())
            shopItemTransform.Find("currentStats").GetComponent<TextMeshProUGUI>().SetText($"{upgrade.UpgradeDescription} {GetCurrentStatInfo(upgrade.Stat)}/{upgrade.ItemCap}");
        else if(GetCurrentStatInfo(upgrade.Stat) >= upgrade.ItemCap)
        {
            shopItemTransform.Find("background").GetComponent<Image>().color = Color.red;
            shopItemTransform.Find("currentStats").GetComponent<TextMeshProUGUI>().SetText("Maximum limit reaced");
        }
        else if(upgrade.Price > playerStats.GetPlayerMoney())
        {
            shopItemTransform.Find("background").GetComponent<Image>().color = Color.red;
            shopItemTransform.Find("currentStats").GetComponent<TextMeshProUGUI>().SetText("Insufficient funds");
        }
        shopItemTransform.GetComponent<Button>().onClick.AddListener(() =>
        {
            TryBuyUpgrade(upgrade, shopIndex);
        });
        return shopItemTransform;
    }
    protected void UpdateItemPrice(Upgrades upgrade)
    {
        upgrade.Price = upgrade.BasePrice + (int)(upgrade.BasePrice * GetCurrentStatInfo(upgrade.Stat)/100);
    }
    public void TryBuyUpgrade(Upgrades upgrade, int shopIndex)
    {
        // If player has enough money
        if (GetCurrentStatInfo(upgrade.Stat) < upgrade.ItemCap && customer.TrySpendGold(upgrade.Price)) // If the player has money and the cap is not reached
        {
                customer.BoughtItem(upgrade.Stat);
                // Updating Item description
                if (GetCurrentStatInfo(upgrade.Stat) < upgrade.ItemCap)
                {
                    UpdateItemPrice(upgrade);
                    buttons[shopIndex].Find("priceText").GetComponent<TextMeshProUGUI>().SetText(upgrade.Price.ToString());
                    buttons[shopIndex].Find("currentStats").GetComponent<TextMeshProUGUI>().SetText($"{upgrade.UpgradeDescription} {GetCurrentStatInfo(upgrade.Stat)}/{upgrade.ItemCap}");
                }
                else
                {
                    buttons[shopIndex].Find("currentStats").GetComponent<TextMeshProUGUI>().SetText("Maximum limit reaced");
                    buttons[shopIndex].Find("background").GetComponent<Image>().color = Color.red;
                }
        }
        else
        {
            if(GetCurrentStatInfo(upgrade.Stat) < upgrade.ItemCap)
            {
                buttons[shopIndex].Find("currentStats").GetComponent<TextMeshProUGUI>().SetText("Insufficient funds");
                buttons[shopIndex].Find("background").GetComponent<Image>().color = Color.red;
            }
        }
    }

    public abstract void LoadShopUpgrades();
    public void ShowShop(IShopCustomer _customer)
    {
        customer = _customer;
        foreach (var item in buttons)
        {
            item.gameObject.SetActive(true);
        }
    }
    public void HideShop()
    {
        foreach (var item in buttons)
        {
            item.gameObject.SetActive(false);
        }
    }

}
