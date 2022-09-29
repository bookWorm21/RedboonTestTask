using Sources.RedboonTradeTask.Core.Trading.Interfaces;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Models;

namespace Sources.RedboonTradeTask.Core.Trading
{
    public class TradeService : ITradeService
    {
        public ITrader Player { get; }

        public ITrader Trader { get; }

        public TradeService(ITrader player, ITrader trader)
        {
            Player = player;
            Trader = trader;
        }

        public bool TryBuy(ItemModel buyingItem)
        {
            if (!Trader.Inventory.CanTake(buyingItem))
            {
                return false;
            }

            if (!Player.Wallet.CanTake(buyingItem.Price.NeedItems))
            {
                return false;
            }
            
            Trader.Inventory.Take(buyingItem);
            Trader.Wallet.Add(buyingItem.Price.NeedItems);
            Player.Inventory.Add(buyingItem);
            buyingItem.ChangePrice(buyingItem.AfterBuyingPrice);
            
            return true;
        }

        public bool TrySell(ItemModel sellingItem)
        {
            if (!Player.Inventory.CanTake(sellingItem))
            {
                return false;
            }

            Player.Inventory.Take(sellingItem);
            Player.Wallet.Add(sellingItem.Price.NeedItems);
            Trader.Inventory.Add(sellingItem);
            
            return true;
        }
    }
}