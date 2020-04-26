using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades
{
    public Upgrades(BaseShop.UpgradeStats stat, string upgradeName, string upgradeDescription, int basePrice, float itemCap, bool isDefenceUpgrade, Sprite sprite)
    {
        UpgradeName = upgradeName;
        UpgradeDescription = upgradeDescription;
        BasePrice = Price = basePrice;
        ItemCap = itemCap;
        IsDefenceUpgrade = isDefenceUpgrade;
        Sprite = sprite;
        Stat = stat;
    }

    public string UpgradeName { get;}
    public string UpgradeDescription { get;}
    public int BasePrice { get; set; }
    public int Price { get; set; }
    public float ItemCap { get;}
    public bool IsDefenceUpgrade { get;}
    public Sprite Sprite { get;}
    public BaseShop.UpgradeStats Stat { get; set; }
}
