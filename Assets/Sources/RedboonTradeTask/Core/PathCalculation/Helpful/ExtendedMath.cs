using System;
using UnityEngine;

namespace Sources.RedboonTradeTask.Core.PathCalculation.Helpful.ExtendedMath
{
    public static class ExtendedMath
    {
        public const double Eps = 1E-9;
        
        public struct Line
        {
            public double A;
            public double B;
            public double C;

            public Line(Vector2 p, Vector2 q)
            {
                A = p.y - q.y;
                B = q.x - p.x;
                C = -A * p.x - B * p.y;
            }

            public void Normalized()
            {
                double z = Math.Sqrt(A * A + B * B);
                if (Math.Abs(z) > Eps)
                {
                    A /= z;
                    B /= z;
                    C /= z;
                }
            }

            public double Dist(Vector2 point)
            {
                return A * point.x + B * point.y + C;
            }
        }

        public struct LineSegment
        {
            public Vector2 A;
            public Vector2 B;

            public LineSegment(Vector2 a, Vector2 b)
            {
                A = a;
                B = b;
            }
        }
        
        private static double Deter(double a, double b, double c, double d)
        {
            return a * d - b * c;
        }

        private static bool IsBetween(float left, float right, float x)
        {
            return Math.Min(left, right) <= x + Eps && x <= Mathf.Max(left, right) + Eps;
        }

        private static bool Intersect1D(float a, float b, float c, float d)
        {
            if (a > b) (a, b) = (b, a);
            if (c > d) (c, d) = (d, c);

            return Math.Max(a, c) <= Math.Min(b, d) + Eps;
        }

        public static bool IsParallel(Line m, Line n)
        {
            return Math.Abs(Deter(m.A, m.B, n.A, n.B)) < Eps;
        }

        public static bool IsEquivalent(Line m, Line n)
        {
            return Math.Abs(Deter(m.A, m.B, n.A, n.B)) < Eps &&
                   Math.Abs(Deter(m.A, m.B, n.A, n.B)) < Eps &&
                   Math.Abs(Deter(m.A, m.B, n.A, n.B)) < Eps;
        }

        public static bool Intersect(Line m, Line n, out Vector2 res)
        {
            double zn = Deter(m.A, m.B, n.A, n.B);
            if (Math.Abs(zn) < Eps)
            {
                res = Vector2.zero;
                return false;
            }
            
            res.x = (float) (-Deter(m.C, m.B, n.C, n.B) / zn);
            res.y = (float) (-Deter(m.A, m.C, n.A, n.C) / zn);
            return true;
        }

        public static bool Intersect(Vector2 a, Vector2 b, Vector2 c, Vector2 d, out Vector2 left, out Vector2 right)
        {
            left = right = Vector2.zero;

            if (!Intersect1D(a.x, b.x, c.x, d.x) || !Intersect1D(a.y, b.y, c.y, d.y))
                return false;

            var m = new Line(a, b);
            var n = new Line(c, d);
            m.Normalized();
            n.Normalized();
            if (!Intersect(m, n, out Vector2 result))
            {
                if (Math.Abs(m.Dist(c)) > Eps || Math.Abs(n.Dist(a)) > Eps)
                {
                    return false;
                }

                if (Math.Abs(a.x - b.x) < Eps && Math.Abs(a.y - b.y) < Eps)
                    (a, b) = (b, a);

                if (Math.Abs(c.x - d.x) < Eps && Math.Abs(c.y - d.y) < Eps)
                    (c, d) = (d, c);

                left = Vector2.Max(a, c);
                right = Vector2.Min(b, d);
                return true;
            }
            else
            {
                left = right = result;
                return IsBetween(a.x, b.x, left.x) &&
                       IsBetween(a.y, b.y, left.y) &&
                       IsBetween(c.x, d.x, left.x) &&
                       IsBetween(c.y, d.y, left.y);
            }
        }
    }
}