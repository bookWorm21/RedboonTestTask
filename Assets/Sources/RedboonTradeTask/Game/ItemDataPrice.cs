using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.RedboonTradeTask.Game
{
    [Serializable]
    public class ItemDataPrice
    {
        [SerializeField] private List<KitItemData> _items = new List<KitItemData>();

        public IEnumerable<KitItemData> NeedItems => _items;
    }
}