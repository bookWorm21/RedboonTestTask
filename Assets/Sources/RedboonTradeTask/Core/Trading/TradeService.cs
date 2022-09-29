using Sources.RedboonTradeTask.Core.Trading.Interfaces;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Models;

namespace Sources.RedboonTradeTask.Core.Trading
{
    public class TradeService : ITradeService
    {
        public bool TryBuy(ITrader player, ITrader trader, ItemModel buyingItem)
        {
            if (!trader.Inventory.CanTake(buyingItem))
            {
                return false;
            }

            if (!player.Inventory.CanTake(buyingItem.Price.NeedItems))
            {
                return false;
            }
            
            trader.Inventory.Take(buyingItem);
            trader.Inventory.Add(buyingItem.Price.NeedItems);
            player.Inventory.Add(buyingItem);
            buyingItem.ChangePrice(buyingItem.AfterBuyingPrice);
            
            return true;
        }

        public bool TrySell(ITrader player, ITrader trader, ItemModel sellingItem)
        {
            if (!player.Inventory.CanTake(sellingItem))
            {
                return false;
            }

            player.Inventory.Take(sellingItem);
            player.Inventory.Add(sellingItem.Price.NeedItems);
            trader.Inventory.Add(sellingItem);
            
            return true;
        }
    }
}