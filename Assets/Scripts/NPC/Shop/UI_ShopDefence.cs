using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI_ShopDefence : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;
    private IShopCustomer shopCustomer;
    private PlayerStats playerStats;
    private List<Transform> list = new List<Transform>();
    private void Awake()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        list.Add(CreateItemButton(Item.DefenceStat.HealthUpgrade, "Health upgrade", 0));
        list.Add(CreateItemButton(Item.DefenceStat.ArmourUpgrade, "Defence upgrade", 1));
        list.Add(CreateItemButton(Item.DefenceStat.AgilityUpgrade, "Agility upgrade", 2));
        list.Add(CreateItemButton(Item.DefenceStat.JumpingUpgrade, "Jumping upgrade", 3));

    }
    private Transform CreateItemButton(Item.DefenceStat stat, string itemName, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 100f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("priceText").GetComponent<TextMeshProUGUI>().SetText(Item.GetCost(stat).ToString());
        shopItemTransform.Find("itemIcon").GetComponent<Image>().sprite = Item.GetSprite(stat);
        shopItemTransform.Find("currentStats").GetComponent<TextMeshProUGUI>().SetText(GetInfo(stat));

        shopItemTransform.GetComponent<Button>().onClick.AddListener(() =>
        {
            TryBuyItem(stat, positionIndex);
        });
        return shopItemTransform;
    }
    public string GetInfo(Item.DefenceStat stat)
    {
        switch (stat)
        {
            default:
            case Item.DefenceStat.ArmourUpgrade:
                if (playerStats.GetPlayerDefence() >= playerStats.GetStatusCaps(stat))
                    return "Maximum limit reaced";
                else
                    return $"Current defence {playerStats.GetPlayerDefence()}/{playerStats.GetStatusCaps(stat)}";

            case Item.DefenceStat.HealthUpgrade:
                if (playerStats.GetPlayerMaxHealth() >= playerStats.GetStatusCaps(stat))
                    return "Maximum limit reaced";
                else
                    return $"Current maximum HP {playerStats.GetPlayerMaxHealth()}/{playerStats.GetStatusCaps(stat)}";

            case Item.DefenceStat.AgilityUpgrade:
                if (playerStats.GetPlayerMovementSpeed() >= playerStats.GetStatusCaps(stat))
                    return "Maximum limit reaced";
                else
                    return $"Current movement speed {playerStats.GetPlayerMovementSpeed()}/{playerStats.GetStatusCaps(stat)}";

            case Item.DefenceStat.JumpingUpgrade:
                if (playerStats.GetPlayerNumberOfJumps() >= playerStats.GetStatusCaps(stat))
                    return "Maximum limit reaced";
                else
                    return $"Current number of jumps {playerStats.GetPlayerNumberOfJumps()}/{playerStats.GetStatusCaps(stat)}";
        }
    }
    private void TryBuyItem(Item.DefenceStat stat, int itemIndex)
    {
        // If player has enough money
        if (shopCustomer.TrySpendGold(Item.GetCost(stat)))
        {
            if(!shopCustomer.IsStatusCapReaced(stat))
            {
                shopCustomer.BoughtItem(stat);
                // Updating Item description
                if (!shopCustomer.IsStatusCapReaced(stat))
                    list[itemIndex].Find("currentStats").GetComponent<TextMeshProUGUI>().SetText(GetInfo(stat));
                else 
                    list[itemIndex].Find("currentStats").GetComponent<TextMeshProUGUI>().SetText("Maximum limit reaced");
            }
        }
        else
        {
            list[itemIndex].Find("currentStats").GetComponent<TextMeshProUGUI>().SetText("Insufficient funds");
        }
    }

    public void Show(IShopCustomer _shopCustomer)
    {
        shopCustomer = _shopCustomer;
        foreach (var item in list)
        {
            item.gameObject.SetActive(true);
        }
    }
    public void Hide()
    {
        foreach (var item in list)
        {
            item.gameObject.SetActive(false);
        }
    }

}
