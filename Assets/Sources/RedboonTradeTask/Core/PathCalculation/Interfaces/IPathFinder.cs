using System.Collections.Generic;
using UnityEngine;

namespace Sources.RedboonTradeTask.Core.PathCalculation.Interfaces
{
    public interface IPathFinder
    {
        IEnumerable<Vector2> GetPath(Vector2 a, Vector2 c,
            IEnumerable<Edge> edges);
    }
}