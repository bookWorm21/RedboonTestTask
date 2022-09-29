using System.Collections.Generic;
using System.Linq;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Models;
using Sources.RedboonTradeTask.Game.Data;

namespace Sources.RedboonTradeTask.Game.Factories
{
    public class ItemFactory
    {
        private Dictionary<ItemData, SourceItem> _allSourceCreatedItems;
        private Dictionary<ItemData, ItemModel> _allValuteItems;

        public ItemFactory(IEnumerable<ItemData> currencyItems)
        {
            _allSourceCreatedItems = new Dictionary<ItemData, SourceItem>();
            foreach(var currency in currencyItems.Where(p=>p.ItemType == ItemType.Resource))
            {
                CreateItem(currency);
            }
        }
        
        public ItemModel CreateItem(ItemData itemData)
        {
            if (itemData.ItemType == ItemType.Resource)
            {
                if(_allSourceCreatedItems.ContainsKey(itemData))
                {
                    return _allValuteItems[itemData];
                }

                Price price = new Price();
                SourceItem source = 
                    new SourceItem(price, price, itemData.Name, itemData.ItemType, itemData.Icon);
                ItemModel model = new ItemModel(source);

                _allSourceCreatedItems.Add(itemData, source);
                _allValuteItems.Add(itemData, model);
                    
                return model;
            }
            else if(itemData.ItemType == ItemType.Tradable)
            {
                if (_allSourceCreatedItems.TryGetValue(itemData, out var source)) 
                    return new ItemModel(source);
                
                var priceKitItems =
                    itemData.Price.NeedItems.Select(p => 
                        new KitItem(_allSourceCreatedItems[p.ItemData], p.Count));
                var afterBuyingKitItems =
                    itemData.AfterBuyingPrice.NeedItems.Select(p =>
                        new KitItem(_allSourceCreatedItems[p.ItemData], p.Count));

                var price = new Price(priceKitItems.ToArray());
                var afterBuyingPrice = new Price(afterBuyingKitItems.ToArray());
                
                source = 
                    new SourceItem(price, afterBuyingPrice, itemData.Name, itemData.ItemType, itemData.Icon);

                _allSourceCreatedItems.Add(itemData, source);

                return new ItemModel(source);
            }

            return null;
        }
    }
}