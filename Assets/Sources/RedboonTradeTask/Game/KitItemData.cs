using System;
using Sources.RedboonTradeTask.Game.Data;
using UnityEngine;

namespace Sources.RedboonTradeTask.Game
{
    [Serializable]
    public class KitItemData
    {
        [field:SerializeField] public ItemData ItemData { get; private set; }
        [field: SerializeField] public int Count { get; private set; }
    }
}