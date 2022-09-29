using UnityEngine;

namespace Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Models
{
    public class ItemModel
    {
        public SourceItem SourceItem { get; private set; }
        public Sprite Icon { get; private set; }
        public Price Price { get; private set; }
        public Price AfterBuyingPrice { get; private set; }
        public ItemType ItemType { get; private set; }
        public string Name { get; private set; }

        public ItemModel(SourceItem sourceItem)
        {
            SourceItem = sourceItem;
            Icon = sourceItem.Icon;
            Price = sourceItem.Price;
            AfterBuyingPrice = sourceItem.AfterBuyingPrice;
            ItemType = sourceItem.ItemType;
            Name = sourceItem.Name;
        }

        public void ChangePrice(Price price)
        {
            Price = price;
        }
    }
}