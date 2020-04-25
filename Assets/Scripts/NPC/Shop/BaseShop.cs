using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseShop : MonoBehaviour
{
    protected List<Upgrades> upgrades = new List<Upgrades>();
    protected IShopCustomer customer;
    protected PlayerStats playerStats;
    private Transform container;
    private Transform shopItemTemplate;

    /// <summary>
    /// IMPORTANT !!!!
    /// 
    ///     To add more items to the shop
    ///     You need to add them to "UpgradeStats", "LoadAllUpdates". 
    ///     Make sure they are added IN THE SAME ORDER
    ///     Also update the "GetCurrentStatInfo"
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
        upgrades.Add(new Upgrades("Health upgrade", "Current health:", 420, 300, true, GameAssets.i.HealthIconSprite));
        upgrades.Add(new Upgrades("Defence upgrade", "Current defence:", 600, 30, true, GameAssets.i.ArmourIconSprite));
        upgrades.Add(new Upgrades("Agility upgrade", "Current movement speed:", 420, 15.0f, true, GameAssets.i.AgilityIconSprite));
        upgrades.Add(new Upgrades("Jumping upgrade", "Current number of Jumps:", 6000, 5, true, GameAssets.i.JumpingIconSprite));

        upgrades.Add(new Upgrades("Weapon upgrade", "Current melee damage:", 145, 50, false, GameAssets.i.WeaponIconSprite));
        upgrades.Add(new Upgrades("Bow upgrade", "Current range damage:", 320, 50, false, GameAssets.i.RangedIconSprite));
    }
    protected float GetCurrentStatInfo(UpgradeStats upgrades)
    {
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
    private void Awake()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        container = GameObject.Find("container").GetComponent<Transform>();
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
        LoadAllUpgrades();
    }
    protected Transform CreateButton(Upgrades upgrade, UpgradeStats stat, int shopIndex)
    {
        UpdateItemPrice(upgrade, stat);
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 100f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * shopIndex);

        shopItemTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(upgrade.UpgradeName);
        shopItemTransform.Find("priceText").GetComponent<TextMeshProUGUI>().SetText(upgrade.Price.ToString());
        shopItemTransform.Find("itemIcon").GetComponent<Image>().sprite = upgrade.Sprite;
        if (GetCurrentStatInfo(stat) < upgrade.ItemCap)
            shopItemTransform.Find("currentStats").GetComponent<TextMeshProUGUI>().SetText($"{upgrade.UpgradeDescription} {GetCurrentStatInfo(stat)}/{upgrade.ItemCap}");
        else
        {
            shopItemTransform.Find("background").GetComponent<Image>().color = Color.red;
            shopItemTransform.Find("currentStats").GetComponent<TextMeshProUGUI>().SetText("Maximum limit reaced");
        }
        shopItemTransform.GetComponent<Button>().onClick.AddListener(() =>
        {
            TryBuyUpgrade(upgrade, stat, shopIndex);
        });
        return shopItemTransform;
    }
    protected void UpdateItemPrice(Upgrades upgrade, UpgradeStats stat)
    {
        upgrade.Price = upgrade.BasePrice + (int)(upgrade.BasePrice * GetCurrentStatInfo(stat)/100);
    }
    public abstract void TryBuyUpgrade(Upgrades upgrade, UpgradeStats stat, int shopIndex);
    public abstract void LoadShopUpgrades();
    public abstract void ShopShop(IShopCustomer customer);
    public abstract void HideShop();

}
