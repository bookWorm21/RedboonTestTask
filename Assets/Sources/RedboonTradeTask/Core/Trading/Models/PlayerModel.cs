using Sources.RedboonTradeTask.Core.Trading.Interfaces;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic;

namespace Sources.RedboonTradeTask.Core.Trading.Models
{
    public class PlayerModel : ITrader
    {
        private Inventory _inventory;

        public Inventory Inventory => _inventory;

        public PlayerModel(Inventory inventory)
        {
            _inventory = inventory;
        }
    }
}