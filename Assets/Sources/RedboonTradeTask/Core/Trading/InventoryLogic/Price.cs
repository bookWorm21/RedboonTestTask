using System.Collections.Generic;

namespace Sources.RedboonTradeTask.Core.Trading.InventoryLogic
{
    public struct Price
    {
        private KitItem[] _items;

        public IEnumerable<KitItem> NeedItems => _items;

        public Price(KitItem[] items)
        {
            _items = items;
        }
    }
}