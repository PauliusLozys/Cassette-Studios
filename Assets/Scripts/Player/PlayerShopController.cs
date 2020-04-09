using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShopController : MonoBehaviour, IShopCustomer
{
    public Transform ShopUI;
    private UI_ShopItem itemShop;
    private UI_ShopStat statshop;

    private PlayerCombatController playerCombat;

    private Interactable currentInteractableObject = null;
    private bool IsShopOpen = false;

    private void Start()
    {
        itemShop = ShopUI.GetComponent<UI_ShopItem>();
        statshop = ShopUI.GetComponent<UI_ShopStat>();
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
            if(currentInteractableObject.shopType == Interactable.ShopType.WeaponShop)
            {
                itemShop.Show(this);
            }
            else if(currentInteractableObject.shopType == Interactable.ShopType.StatusShop)
            {
                statshop.Show(this);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Q) && IsShopOpen)
        {
            Debug.Log("Shop closed");
            itemShop.Hide();
            statshop.Hide();
            IsShopOpen = false;
            playerCombat.SetCombat(true);
        }
    }
    public void BoughtItem(Item.StatType stat)
    {
        Debug.Log($"Bought Health upgrade");
    }

    public void BoughtItem(Item.ItemType item)
    {
        Debug.Log("Bought one of two items");
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
            itemShop.Hide();
            statshop.Hide();
            IsShopOpen = false;
            playerCombat.SetCombat(true);
            currentInteractableObject.hidePopup();
            currentInteractableObject = null;
        }
    }


}
