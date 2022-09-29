using System;
using System.Collections.Generic;
using System.Linq;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Models;

namespace Sources.RedboonTradeTask.Core.Trading.InventoryLogic
{
    public class Inventory
    {
        private HashSet<ItemModel> _items;

        public Inventory()
        {
            _items = new HashSet<ItemModel>();
        }

        public Inventory(IEnumerable<ItemModel> models)
        {
            _items = new HashSet<ItemModel>();
            foreach (var model in models)
            {
                Add(model);
            }
        }

        public bool TryTake(ItemModel item)
        {
            if (CanTake(item))
            {
                Take(item);   
                return true;
            }

            return false;
        }

        public bool TryTake(IEnumerable<ItemModel> items)
        {
            var kitItems = items.ToArray();
            foreach (var item in kitItems)
            {
                if (!CanTake(item))
                {
                    return false;
                }
            }

            Take(kitItems);

            return true;
        }

        public bool CanTake(ItemModel item)
        {
            return _items.Contains(item);
        }
        
        public bool CanTake(IEnumerable<ItemModel> items)
        {
            foreach (var item in items)
            {
                if (!CanTake(item))
                {
                    return false;
                }
            }

            return true;
        }
        
        public void Add(ItemModel item)
        {
            _items.Add(item);
        }

        public void Add(IEnumerable<ItemModel> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public void Take(ItemModel item)
        {
            if (!_items.Contains(item))
            {
                throw new Exception("No required item");
            }
            _items.Remove(item);
        }
        
        public void Take(IEnumerable<ItemModel> items)
        {
            foreach (var item in items)
            {
                Take(item);
            }
        }
    }
}