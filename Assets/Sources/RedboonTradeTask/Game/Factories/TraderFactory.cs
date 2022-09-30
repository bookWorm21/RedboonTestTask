using System.Collections.Generic;
using System.Linq;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Models;
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
            var items = new List<ItemModel>();
            
            foreach (var kitItem in traderData.StartItems)
            {
                for (int i = 0; i < kitItem.Count; ++i)
                {
                    items.Add(_itemFactory.CreateItem(kitItem.ItemData));
                }
            }
            
            var currencies =
                traderData.StartCurrencyItems.Select(p => 
                    new KitItem(_itemFactory.CreateItem(p.ItemData).SourceItem, p.Count));
            
            var inventory = new Inventory(items);
            var wallet = new Wallet(currencies, true);

            return new TraderModel(inventory, wallet);
        }
    }
}