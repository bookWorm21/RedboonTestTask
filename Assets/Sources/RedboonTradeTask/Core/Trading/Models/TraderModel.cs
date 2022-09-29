using Sources.RedboonTradeTask.Core.Trading.Interfaces;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic;

namespace Sources.RedboonTradeTask.Core.Trading.Models
{
    public class TraderModel : ITrader
    {
        public Inventory Inventory { get; }

        public Wallet Wallet { get; }

        public TraderModel(Inventory inventory, Wallet wallet)
        {
            Inventory = inventory;
            Wallet = wallet;
        }
    }
}