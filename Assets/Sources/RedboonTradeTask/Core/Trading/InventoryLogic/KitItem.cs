using Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Models;

namespace Sources.RedboonTradeTask.Core.Trading.InventoryLogic
{
    public class KitItem
    {
        public ItemModel ItemModel { get; private set; }
        public int Count { get; private set; }

        public KitItem(ItemModel itemModel, int count)
        {
            ItemModel = itemModel;
            Count = count;
        }
    }
}