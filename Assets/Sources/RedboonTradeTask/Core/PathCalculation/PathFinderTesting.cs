using System.Collections.Generic;
using System.Linq;
using Sources.RedboonTradeTask.Core.PathCalculation.Interfaces;
using UnityEngine;

namespace Sources.RedboonTradeTask.Core.PathCalculation
{
    public class PathFinderTesting : MonoBehaviour
    {
        [SerializeField] private EdgeInputData _templateData;
        [SerializeField] private LineRenderer _rectangleRendererTemplate;
        [SerializeField] private LineRenderer _pathRederer;
        [SerializeField] private Transform _linesContainer;

        private IPathFinder _pathFinder;
        private void Start()
        {
            _pathFinder = new PathFinderService();
            RenderRectangles(_templateData);
            var path = CalculatePath().ToArray();
            RenderPath(path, _templateData);
        }

        private void RenderRectangles(EdgeInputData data)
        {
            float scale = data.ScaleFactor;

            for (int i = 0; i <= data.Edges.Count; ++i)
            {
                var pointsRenderPosition = new Vector3[4];
                var lineRenderer = Instantiate(_rectangleRendererTemplate, _linesContainer);
                lineRenderer.gameObject.SetActive(true);
                
                Rectangle rectangle;
                if (i == data.Edges.Count)
                {
                    rectangle = data.Edges[i - 1].Second;
                }
                else
                {
                    rectangle = data.Edges[i].First;
                }
                
                var leftTopVertex = new Vector3(rectangle.Min.x,0, rectangle.Max.y);
                var rightBottomVertex = new Vector3(rectangle.Max.x,0, rectangle.Min.y);

                pointsRenderPosition[0] = new Vector3(rectangle.Min.x, 0, rectangle.Min.y) / scale;
                pointsRenderPosition[1] = rightBottomVertex/ scale;
                pointsRenderPosition[2] = new Vector3(rectangle.Max.x, 0, rectangle.Max.y)/ scale;
                pointsRenderPosition[3] = leftTopVertex/ scale;

                lineRenderer.positionCount = 4;
                lineRenderer.SetPositions(pointsRenderPosition);
            }
        }

        private void RenderPath(IEnumerable<Vector2> path, EdgeInputData data)
        {
            var arrayPath = path.ToArray();
            _pathRederer.positionCount = arrayPath.Count();
            _pathRederer.SetPositions(arrayPath.Select(p => new Vector3(p.x / data.ScaleFactor, 0, p.y / data.ScaleFactor)).ToArray());
        }

        private IEnumerable<Vector2> CalculatePath()
        {
            return _pathFinder.GetPath(_templateData.A, _templateData.C, _templateData.Edges);
        }
    }
}