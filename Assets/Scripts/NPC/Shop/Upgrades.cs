using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades
{
    public Upgrades(string upgradeName, string upgradeDescription, int basePrice, float itemCap, bool isDefenceUpgrade, Sprite sprite)
    {
        UpgradeName = upgradeName;
        UpgradeDescription = upgradeDescription;
        BasePrice = Price = basePrice;
        ItemCap = itemCap;
        IsDefenceUpgrade = isDefenceUpgrade;
        Sprite = sprite;
    }

    public string UpgradeName { get;}
    public string UpgradeDescription { get;}
    public int BasePrice { get; set; }
    public int Price { get; set; }
    public float ItemCap { get;}
    public bool IsDefenceUpgrade { get;}
    public Sprite Sprite { get;}
}
