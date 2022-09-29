using System.Collections.Generic;
using System.Linq;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Models;

namespace Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Extension
{
    public static class ItemCollectionExtension
    {
        public static Dictionary<ItemModel, int> ToDictionaryItems(this IEnumerable<KitItem> items)
        {
            return items.ToDictionary(it => it.ItemModel, it => it.Count);
        }

        public static IEnumerable<KitItem> ToKitItems(this Dictionary<ItemModel, int> dictionary)
        {
            return dictionary.Select(p => new KitItem(p.Key, p.Value));
        }
    }
}