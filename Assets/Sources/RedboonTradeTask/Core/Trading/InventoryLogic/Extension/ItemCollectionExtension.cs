using System.Collections.Generic;
using System.Linq;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Models;

namespace Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Extension
{
    public static class ItemCollectionExtension
    {
        public static Dictionary<SourceItem, int> ToDictionaryItems(this IEnumerable<KitItem> items)
        {
            return items.ToDictionary(it => it.SourceItem, it => it.Count);
        }

        public static IEnumerable<KitItem> ToKitItems(this Dictionary<SourceItem, int> dictionary)
        {
            return dictionary.Select(p => new KitItem(p.Key, p.Value));
        }
    }
}