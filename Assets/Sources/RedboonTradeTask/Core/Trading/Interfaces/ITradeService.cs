using Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Models;

namespace Sources.RedboonTradeTask.Core.Trading.Interfaces
{
    public interface ITradeService
    {
        public bool TryBuy(ITrader player, ITrader trader, ItemModel buyingItem);
        public bool TrySell(ITrader player, ITrader trader, ItemModel sellingItem);
    }
}