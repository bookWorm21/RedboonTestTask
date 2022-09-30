using System.Collections.Generic;
using Sources.RedboonTradeTask.Core.Trading.InventoryLogic;
using UnityEngine;

namespace Sources.RedboonTradeTask.Game.Data
{
    [CreateAssetMenu(fileName = "new trader data", menuName = "GameData/TraderData")]
    public class TraderData : ScriptableObject
    {
        [SerializeField] private List<KitItemData> _startItems = new List<KitItemData>();
        [SerializeField] private List<KitItemData> _startCurrencyItems;

        public IEnumerable<KitItemData> StartItems => _startItems;
        public IEnumerable<KitItemData> StartCurrencyItems => _startCurrencyItems;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            for (int i = 0; i < _startCurrencyItems.Count; i++)
            {
                if(_startCurrencyItems[i].ItemData == null) continue;
                
                if (_startCurrencyItems[i].ItemData.ItemType == ItemType.Tradable)
                {
                    Debug.LogWarning("Item data on currency items should be resource type");
                    _startCurrencyItems.RemoveAt(i);

                    if (_startCurrencyItems.Count == 0)
                    {
                        break;
                    }
                }
            }
        }
#endif
    }
}