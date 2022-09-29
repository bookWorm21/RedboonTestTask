using UnityEngine;

namespace Sources.RedboonTradeTask.Core.Trading.InventoryLogic.Models
{
    public class SourceItem 
    {
        public Sprite Icon { get; private set; }
        public Price Price { get; private set; }
        public Price AfterBuyingPrice { get; private set; }
        public ItemType ItemType { get; private set; }
        public string Name { get; private set; }
        
        public SourceItem(Price price, Price afterBuyingPrice, string name, ItemType itemType, Sprite icon)
        {
            Icon = icon;
            Price = price;
            AfterBuyingPrice = afterBuyingPrice;
            ItemType = itemType;
            Name = name;
        }
    }
}