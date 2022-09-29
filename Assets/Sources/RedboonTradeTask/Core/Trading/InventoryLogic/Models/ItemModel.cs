using UnityEngine;

namespace Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Models
{
    public class ItemModel
    {
        public Sprite Icon { get; private set; }
        public Price Price { get; private set; }

        public Price AfterBuyingPrice { get; private set; }
        public ItemType ItemType { get; private set; }
        public string Name { get; private set; }

        public ItemModel(Price price, Price afterBuyingPrice, string name, ItemType itemType, Sprite icon)
        {
            Icon = icon;
            Price = price;
            AfterBuyingPrice = afterBuyingPrice;
            ItemType = itemType;
            Name = name;
        }

        public void ChangePrice(Price price)
        {
            Price = price;
        }
    }
}