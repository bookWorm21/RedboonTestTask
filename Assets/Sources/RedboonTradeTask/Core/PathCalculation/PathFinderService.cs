using System.Collections.Generic;
using System.Linq;
using Sources.RedboonTradeTask.Core.PathCalculation.Helpful.ExtendedMath;
using Sources.RedboonTradeTask.Core.PathCalculation.Interfaces;
using UnityEngine;

namespace Sources.RedboonTradeTask.Core.PathCalculation
{
    public class PathFinderService : IPathFinder
    {
        private enum EdgeHaving : byte
        {
            First,
            Second,
            Both
        }

        private struct PointInfo
        {
            public Vector2 Point;
            public Edge FirstEdge;
            public Edge SecondEdge;
            public EdgeHaving EdgeHaving;
        }
        
        private const float StepSize = 1.0f;

        public IEnumerable<Vector2> GetPath(Vector2 a, Vector2 c, IEnumerable<Edge> edges)
        {
            var edgesArray = edges.ToArray();

            if (!Valudate(a, c, edgesArray))
            {
                return new List<Vector2>();
            }

            var infoPath = new List<PointInfo>(edgesArray.Length)
            {
                new PointInfo
                {
                    Point = a,
                    EdgeHaving = EdgeHaving.First,
                    FirstEdge = edgesArray.First(),
                }
            };

            var currentPoint = a;

            for (var i = 0; i < edgesArray.Length; i++)
            {
                EdgeHaving edgeHaving = EdgeHaving.Second;
                Edge secondEdge = edgesArray[i];
                var firstEdge = new Edge();

                if (i != edgesArray.Length - 1)
                {
                    firstEdge = edgesArray[i + 1];
                    edgeHaving = EdgeHaving.Both;

                    if (IsEdgesOnOneSide(edgesArray[i], edgesArray[i + 1]))
                    {
                        currentPoint = NearestPoint(currentPoint, edgesArray[i].Second);
                        infoPath.Add(
                            new PointInfo
                            {
                                Point = currentPoint,
                                EdgeHaving = edgeHaving,
                                FirstEdge = firstEdge,
                                SecondEdge = secondEdge,
                            });
                    }
                }

                currentPoint = NearestPoint(currentPoint, edgesArray[i]);
                infoPath.Add(
                    new PointInfo
                    {
                        Point = currentPoint,
                        EdgeHaving = edgeHaving,
                        FirstEdge = firstEdge,
                        SecondEdge = secondEdge,
                    });
            }

            infoPath.Add(
                new PointInfo
                {
                    Point = c,
                    EdgeHaving = EdgeHaving.Second,
                    SecondEdge = edgesArray.Last(),
                });

            ReductionPathPointsWithDeletePoint(infoPath);
            ReductionPathPointsWithUnionTwoPoint(infoPath);

            return infoPath.Select(p => p.Point);
        }

        private bool Valudate(Vector2 a, Vector2 c, Edge[] edges)
        {
            if (!edges.Any())
            {
                return false;
            }

            if (!PointIntoRectangle(a, edges.First().First))
            {
                return false;
            }

            if (!PointIntoRectangle(c, edges.Last().Second))
            {
                return false;
            }

            for (int i = 0; i < edges.Length - 1; ++i)
            {
                //edges equal
                if(System.Math.Abs(edges[i].Start.x - edges[i+1].Start.x) < ExtendedMath.Eps &&
                   System.Math.Abs(edges[i].Start.y - edges[i+1].Start.y) < ExtendedMath.Eps &&
                   System.Math.Abs(edges[i].End.x - edges[i+1].End.x) < ExtendedMath.Eps &&
                   System.Math.Abs(edges[i].End.y - edges[i+1].End.y) < ExtendedMath.Eps)
                {
                    return false;
                }
            }
            
            return true;
        }

        private void ReductionPathPointsWithDeletePoint(List<PointInfo> path)
        {
            for (int i = 0; i < path.Count; ++i)
            {
                if (i + 2 > path.Count - 1) break;

                var point1 = path[i];
                var point2 = path[i + 2];

                var lineSegment1 = new ExtendedMath.LineSegment(point1.Point, point2.Point);
                var lineSegment2 = new ExtendedMath.LineSegment(point1.FirstEdge.Start, point1.FirstEdge.End);
                var lineSegment3 = new ExtendedMath.LineSegment(point2.SecondEdge.Start, point2.SecondEdge.End);
                
                if ( ExtendedMath.Intersect(lineSegment1.A, lineSegment1.B, lineSegment2.A, lineSegment2.B, 
                        out _, out _) &&
                     ExtendedMath.Intersect(lineSegment1.A, lineSegment1.B, lineSegment3.A, lineSegment3.B, 
                         out _, out _))
                {
                    path.RemoveAt(i + 1);
                    --i;
                }
            }
        }

        private bool ReductionPathPointsWithUnionTwoPoint(List<PointInfo> path)
        {
            bool isUseReduction = false;
            for (int i = 0; i < path.Count; ++i)
            {
                if (i + 3 > path.Count - 1) break;

                var point1 = path[i];
                var point2 = path[i + 1];
                var point3 = path[i + 2];
                var point4 = path[i + 3];

                var line1_2 = new ExtendedMath.Line(point1.Point, point2.Point);
                var line3_4 = new ExtendedMath.Line(point3.Point, point4.Point);

                if (ExtendedMath.Intersect(line1_2, line3_4, out Vector2 result))
                {
                    PointInfo template = point2;
                    bool haveTemplate = false;

                    if (PointIntoRectangle(result, point2.FirstEdge.First))
                    {
                        template = point2;
                        haveTemplate = true;
                    }
                    else if (PointIntoRectangle(result, point3.SecondEdge.Second))
                    {
                        template = point3;
                        haveTemplate = true;
                    }
                     
                    if(haveTemplate)
                    {
                        path.RemoveAt(i + 2);
                        path[i + 1] = new PointInfo()
                        {
                            Point = result, 
                            EdgeHaving = template.EdgeHaving, 
                            FirstEdge = template.FirstEdge,
                            SecondEdge =  template.SecondEdge,
                        };

                        --i;

                        isUseReduction = true;
                    }
                }

            }

            return isUseReduction;
        }

        private bool IsEdgesOnOneSide(Edge first, Edge second)
        {
            return ExtendedMath.IsEquivalent(new ExtendedMath.Line(first.Start, first.End), new ExtendedMath.Line(second.Start, second.End));
        }

        private Vector2 NearestPoint(Vector2 point, Rectangle rectangle)
        {
            Vector2 center = (rectangle.Max + rectangle.Min) / 2;
            Vector2 bestPoint = center;
            float minDistance = Vector2.Distance(point, center);

            var leftTopVertex = new Vector2(rectangle.Min.x, rectangle.Max.y);
            var rightBottomVertex = new Vector2(rectangle.Max.x, rectangle.Min.y);

            Vector2 point1 = (leftTopVertex + rectangle.Min) * 0.25f + center * 0.5f;
            Vector2 point2 = (rightBottomVertex + rectangle.Max) * 0.25f + center * 0.5f;
            Vector2 point3 = (rectangle.Min + rightBottomVertex) * 0.25f+ center * 0.5f;
            Vector2 point4 = (leftTopVertex + rectangle.Max) * 0.25f + center * 0.5f;

            CheckForBestPoint(point1);
            CheckForBestPoint(point2);
            CheckForBestPoint(point3);
            CheckForBestPoint(point4);

            return bestPoint;

            void CheckForBestPoint(Vector2 currentPoint)
            {
                float distance = Vector2.Distance(currentPoint, point);
                if (distance < minDistance)
                {
                    bestPoint = currentPoint;
                    minDistance = distance;
                }
            }
        }

        private Vector2 NearestPoint(Vector2 point, Edge firstEdge)
        {
            Vector2 bestPoint = firstEdge.Start;

            var minDistance = float.MaxValue;
            var direction = (firstEdge.End - firstEdge.Start).normalized;
            var magnitude = (firstEdge.End - firstEdge.Start).magnitude;
            for (float i = StepSize; i < magnitude; i += StepSize)
            {
                var currentPoint = firstEdge.Start + direction * i;
                var distance = Vector2.Distance(point, firstEdge.Start + direction * i);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    bestPoint = currentPoint;
                }
            }

            return bestPoint;
        }

        private bool PointIntoRectangle(Vector2 point, Rectangle rectangle)
        {
            return point.x >= rectangle.Min.x &&
                   point.y >= rectangle.Min.y &&
                   point.x <= rectangle.Max.x &&
                   point.y <= rectangle.Max.y;
        }
    }
}