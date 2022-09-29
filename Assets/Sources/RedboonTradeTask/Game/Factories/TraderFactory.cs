using System.Linq;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic;
using Sources.RedboonTradeTask.Core.Trading.Models;
using Sources.RedboonTradeTask.Game.Data;

namespace Sources.RedboonTradeTask.Game.Factories
{
    public class TraderFactory
    {
        private ItemFactory _itemFactory;

        public TraderFactory(ItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
        }

        public TraderModel CreateTrader(TraderData traderData)
        {
            var items = traderData.StartItems.Select(p => _itemFactory.CreateItem(p));
            var currencies =
                traderData.StartCurrencyItems.Select(p => 
                    new KitItem(_itemFactory.CreateItem(p.ItemData).SourceItem, p.Count));
            
            var inventory = new Inventory(items);
            var wallet = new Wallet(currencies);

            return new TraderModel(inventory, wallet);
        }
    }
}