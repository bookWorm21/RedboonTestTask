using System;
using System.Collections.Generic;
using System.Linq;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Extension;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Models;

namespace Sources.RedboonTradeTask.Core.Trading.InventoryLogic
{
    public class Inventory
    {
        private Dictionary<ItemModel, int> _items;

        public Inventory()
        {
            _items = new Dictionary<ItemModel, int>();
        }
        
        public Inventory(IEnumerable<KitItem> items)
        {
            _items = items.ToDictionaryItems();
        }

        public bool TryTake(ItemModel item, int count = 1)
        {
            if (CanTake(item, count))
            {
                Take(item, count);   
                return true;
            }

            return false;
        }

        public bool TryTake(IEnumerable<KitItem> items)
        {
            var kitItems = items.ToArray();
            foreach (var item in kitItems)
            {
                if (!CanTake(item.ItemModel, item.Count))
                {
                    return false;
                }
            }

            Take(kitItems);

            return true;
        }

        public bool CanTake(ItemModel item, int count = 1)
        {
            int current = _items[item];
            return current >= count;
        }
        
        public bool CanTake(IEnumerable<KitItem> items)
        {
            foreach (var  item in items)
            {
                if (!CanTake(item.ItemModel, item.Count))
                {
                    return false;
                }
            }

            return true;
        }
        
        public void Add(ItemModel item, int count = 1)
        {
            int oldCount = _items[item];
            oldCount += count;
            _items[item] = oldCount;
        }

        public void Add(IEnumerable<KitItem> items)
        {
            foreach (var item in items)
            {
                Add(item.ItemModel, item.Count);
            }
        }

        public void Take(ItemModel item, int count = 1)
        {
            int oldCount = _items[item];
            oldCount -= count;

            if (oldCount < 0)
            {
                throw new Exception("Trying to take more");
            }
            
            if (oldCount == 0)
            {
                _items.Remove(item);
            }
        }
        
        public void Take(IEnumerable<KitItem> items)
        {
            foreach (var item in items)
            {
                Take(item.ItemModel, item.Count);
            }
        }
    }
}