using System.Linq;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic;
using Sources.RedboonTradeTask.Core.Trading.Models;
using Sources.RedboonTradeTask.Game.Data;

namespace Sources.RedboonTradeTask.Game.Factories
{
    public class PlayerFactory
    {
        private ItemFactory _itemFactory;

        public PlayerFactory(ItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
        }

        public PlayerModel CreatePlayer(PlayerData playerData)
        {
            var items = playerData.StartItems.Select(p => _itemFactory.CreateItem(p));
            var currencies =
                playerData.StartCurrencyItems.Select(p => 
                    new KitItem(_itemFactory.CreateItem(p.ItemData).SourceItem, p.Count));
            
            var inventory = new Inventory(items);
            var wallet = new Wallet(currencies);

            return new PlayerModel(inventory, wallet);
        }
    }
}