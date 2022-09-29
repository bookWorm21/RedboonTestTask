using Sources.RedboonTradeTask.Core.Trading.Interfaces;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic;

namespace Sources.RedboonTradeTask.Core.Trading.Models
{
    public class PlayerModel : ITrader
    {
        public Inventory Inventory { get; }

        public Wallet Wallet { get; }

        public PlayerModel(Inventory inventory, Wallet wallet)
        {
            Inventory = inventory;
            Wallet = wallet;
        }
    }
}