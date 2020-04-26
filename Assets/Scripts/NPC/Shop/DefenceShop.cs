using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefenceShop : BaseShop
{
    public override void LoadShopUpgrades()
    {
        int shopIndex = 0;
        foreach (var upgrade in upgrades)
        {
            if (upgrade.IsDefenceUpgrade)
            {
                buttons.Add(CreateButton(upgrade, shopIndex));
                shopIndex++;
            }
        }     
    }
}
