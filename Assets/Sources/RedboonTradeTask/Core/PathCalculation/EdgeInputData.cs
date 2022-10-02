using System.Collections.Generic;
using UnityEngine;

namespace Sources.RedboonTradeTask.Core.PathCalculation
{
    [CreateAssetMenu(fileName = "new edge input data", menuName = "PathFinding/new edge input data")]
    public class EdgeInputData : ScriptableObject
    {
        public float ScaleFactor;
        public Vector2 A;
        public Vector2 C;
        public List<Edge> Edges;
    }
}