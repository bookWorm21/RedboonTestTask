using System;
using System.Collections.Generic;
using System.Linq;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Models;
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
            var items = new List<ItemModel>();

            foreach (var kitItem in playerData.StartItems)
            {
                for (int i = 0; i < kitItem.Count; ++i)
                {
                    items.Add(_itemFactory.CreateItem(kitItem.ItemData));
                }
            }
            
            var currencies =
                playerData.StartCurrencyItems.Select(p => 
                    new KitItem(_itemFactory.CreateItem(p.ItemData).SourceItem, p.Count));
            
            var inventory = new Inventory(items);
            var wallet = new Wallet(currencies);

            return new PlayerModel(inventory, wallet);
        }
    }
}