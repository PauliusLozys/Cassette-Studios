using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI_ShopItem : MonoBehaviour
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
        list.Add(CreateItemButton(Item.ItemType.WeaponUpgrade, "Weapon upgrade", 0));
        list.Add(CreateItemButton(Item.ItemType.ArmourUpgrade, "Armour upgrade", 1));
    }
    private RectTransform CreateItemButton(Item.ItemType item, string itemName, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 90f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("priceText").GetComponent<TextMeshProUGUI>().SetText(Item.GetCost(item).ToString());
        shopItemTransform.Find("itemIcon").GetComponent<Image>().sprite = Item.GetSprite(item);

        shopItemTransform.GetComponent<Button>().onClick.AddListener(() =>
        {
            TryBuyItem(item);
        });
        return shopItemRectTransform;
    }

    private void TryBuyItem(Item.ItemType item)
    {
        // If player has enough money
        if(shopCustomer.TrySpendGold(Item.GetCost(item)))
            shopCustomer.BoughtItem(item);
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
