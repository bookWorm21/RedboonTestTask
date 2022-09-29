using Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Models;

namespace Sources.RedboonTradeTask.Core.Trading.InventoryLogic
{
    public class KitItem
    {
        public SourceItem SourceItem { get; private set; }
        public int Count { get; private set; }

        public KitItem(SourceItem sourceItem, int count)
        {
            SourceItem = sourceItem;
            Count = count;
        }
    }
}