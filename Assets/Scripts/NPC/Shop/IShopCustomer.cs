public interface IShopCustomer
{
    void BoughtItem(Item.DefenceStat stat);
    void BoughtItem(Item.OffenceStat stat);
    bool TrySpendGold(int goldAmount);
    bool IsStatusCapReaced(Item.DefenceStat stat);
    bool IsStatusCapReaced(Item.OffenceStat stat);
}
