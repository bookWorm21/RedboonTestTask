using System;
using UnityEngine;

namespace Sources.RedboonTradeTask.Core.PathCalculation
{
    [Serializable]
    public struct Edge
    {
        public Rectangle First;
        public Rectangle Second;
        public Vector2 Start;
        public Vector2 End;
    }
}