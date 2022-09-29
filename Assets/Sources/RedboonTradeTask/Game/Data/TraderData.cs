using System.Collections.Generic;
using UnityEngine;

namespace Sources.RedboonTradeTask.Game.Data
{
    [CreateAssetMenu(fileName = "new trader data", menuName = "GameData/TraderData")]
    public class TraderData : ScriptableObject
    {
        [SerializeField] private ItemData[] _startItems;
        [SerializeField] private KitItemData[] _startCurrencyItems;

        public IEnumerable<ItemData> StartItems => _startItems;
        public IEnumerable<KitItemData> StartCurrencyItems => _startCurrencyItems;
    }
}