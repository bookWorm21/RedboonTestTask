using System;
using System.Collections.Generic;
using System.Linq;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Extension;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Models;

namespace Sources.RedboonTradeTask.Core.Trading.InventoryLogic
{
    public class Wallet
    {
        private Dictionary<SourceItem, int> _items;

        public Wallet()
        {
            _items = new Dictionary<SourceItem, int>();
        }
        
        public Wallet(IEnumerable<KitItem> items)
        {
            _items = items.ToDictionaryItems();
        }

        public bool TryTake(SourceItem item, int count = 1)
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
                if (!CanTake(item.SourceItem, item.Count))
                {
                    return false;
                }
            }

            Take(kitItems);

            return true;
        }

        public bool CanTake(SourceItem item, int count = 1)
        {
            int current = _items[item];
            return current >= count;
        }
        
        public bool CanTake(IEnumerable<KitItem> items)
        {
            foreach (var  item in items)
            {
                if (!CanTake(item.SourceItem, item.Count))
                {
                    return false;
                }
            }

            return true;
        }
        
        public void Add(SourceItem item, int count = 1)
        {
            int oldCount = _items[item];
            oldCount += count;
            _items[item] = oldCount;
        }

        public void Add(IEnumerable<KitItem> items)
        {
            foreach (var item in items)
            {
                Add(item.SourceItem, item.Count);
            }
        }

        public void Take(SourceItem item, int count = 1)
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
                Take(item.SourceItem, item.Count);
            }
        }
    }
}