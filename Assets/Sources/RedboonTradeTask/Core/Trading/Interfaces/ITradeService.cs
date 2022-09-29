using Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Models;

namespace Sources.RedboonTradeTask.Core.Trading.Interfaces
{
    public interface ITradeService
    {
        public ITrader Player { get; }
        public ITrader Trader { get; 
    }
        public bool TryBuy(ItemModel buyingItem);
        public bool TrySell(ItemModel sellingItem);
    }
}