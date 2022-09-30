using Sources.RedboonTradeTask.Core.Trading.InventoryLogic;
using UnityEngine;

namespace Sources.RedboonTradeTask.Game.Data
{
    [CreateAssetMenu(fileName = "new item data", menuName = "GameData/ItemData")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public ItemDataPrice Price { get; private set; }

        [field: SerializeField] public ItemDataPrice AfterBuyingPrice { get; private set; }
        [field: SerializeField] public ItemType ItemType { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
    }
}