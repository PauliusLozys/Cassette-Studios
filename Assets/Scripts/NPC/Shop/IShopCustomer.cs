public interface IShopCustomer
{
    void BoughtItem(BaseShop.UpgradeStats stats);
    bool TrySpendGold(int goldAmount);
}
