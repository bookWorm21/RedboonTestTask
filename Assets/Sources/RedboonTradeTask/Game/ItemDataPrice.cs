using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.RedboonTradeTask.Game
{
    [Serializable]
    public class ItemDataPrice
    {
        [SerializeField] private KitItemData[] _items;

        public IEnumerable<KitItemData> NeedItems => _items;
    }
}