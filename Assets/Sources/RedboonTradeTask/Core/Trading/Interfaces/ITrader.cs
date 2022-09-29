using Sources.RedboonTradeTask.Core.Trading.InventoryLogic;

namespace Sources.RedboonTradeTask.Core.Trading.Interfaces
{
    public interface ITrader
    {
        public Inventory Inventory { get; }
    }
}