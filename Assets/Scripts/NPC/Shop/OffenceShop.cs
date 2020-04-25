using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OffenceShop : BaseShop
{
    List<Transform> buttons = new List<Transform>();

    private void Start()
    {
        LoadShopUpgrades();
    }

    public override void LoadShopUpgrades()
    {
        int shopIndex = 0;
        for (int i = 0; i < upgrades.Count; i++)
        {
            if (!upgrades[i].IsDefenceUpgrade)
            {
                buttons.Add(CreateButton(upgrades[i], (UpgradeStats)i, shopIndex));
                shopIndex++;
            }
        }
    }

    public override void ShopShop(IShopCustomer _customer)
    {
        customer = _customer;
        foreach (var item in buttons)
        {
            item.gameObject.SetActive(true);
        }
    }

    public override void HideShop()
    {
        foreach (var item in buttons)
        {
            item.gameObject.SetActive(false);
        }
    }

    public override void TryBuyUpgrade(Upgrades upgrade, UpgradeStats stat, int shopIndex)
    {
        // If player has enough money
        if (customer.TrySpendGold(upgrade.Price))
        {
            if (GetCurrentStatInfo(stat) < upgrade.ItemCap) // If cap is reached shop doenst work
            {
                customer.BoughtItem(stat);
                // Updating Item description
                if (GetCurrentStatInfo(stat) < upgrade.ItemCap)
                {
                    UpdateItemPrice(upgrade, stat);
                    buttons[shopIndex].Find("priceText").GetComponent<TextMeshProUGUI>().SetText(upgrade.Price.ToString());
                    buttons[shopIndex].Find("currentStats").GetComponent<TextMeshProUGUI>().SetText($"{upgrade.UpgradeDescription} {GetCurrentStatInfo(stat)}/{upgrade.ItemCap}");
                }
                else
                {
                    buttons[shopIndex].Find("currentStats").GetComponent<TextMeshProUGUI>().SetText("Maximum limit reaced");
                    buttons[shopIndex].Find("background").GetComponent<Image>().color = Color.red;
                }
            }
        }
        else
        {
            buttons[shopIndex].Find("currentStats").GetComponent<TextMeshProUGUI>().SetText("Insufficient funds");
        }
    }
}
