public interface IShopCustomer
{ 
    void BoughtItem(Item.ItemType item);
    void BoughtItem(Item.StatType stat);
    bool TrySpendGold(int goldAmount);
}
