using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShopStat : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;
    private IShopCustomer shopCustomer;
    private List<RectTransform> list = new List<RectTransform>();

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        list.Add(CreateItemButton(Item.StatType.HealthUpgrade, "Health upgrade", 0));
    }
    private RectTransform CreateItemButton(Item.StatType stat, string itemName, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 90f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("priceText").GetComponent<TextMeshProUGUI>().SetText(Item.GetCost(stat).ToString());
        shopItemTransform.Find("itemIcon").GetComponent<Image>().sprite = Item.GetSprite(stat);

        shopItemTransform.GetComponent<Button>().onClick.AddListener(() =>
        {
            TryBuyItem(stat);
        });
        return shopItemRectTransform;
    }
    private void TryBuyItem(Item.StatType stat)
    {
        // If player has enough money
        if (shopCustomer.TrySpendGold(Item.GetCost(stat)))
            shopCustomer.BoughtItem(stat);
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
