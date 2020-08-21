using System;
using System.Security.Cryptography;
using F = RayTracer.Functions;

namespace RayTracer
{
    public class Point :RTuple
    {
        public Point(double x, double y, double z) : base(x, y, z, 1)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point(RTuple rTuple) : base(rTuple.X, rTuple.Y, rTuple.Z, rTuple.W)
        {
            X = rTuple.X;
            Y = rTuple.Y;
            Z = rTuple.Z;
        }

        public static Vector operator -(Point p1, Point p2)
        {
            return F.Subtract(p1, p2);
        }

        public Vector Subtract(Point p)
        {
            return F.Subtract(this, p);
        }

        public bool IsEqual(Point p1)
        {
            return F.IsEqual(this, p1);
        }

        public bool IsEqual(Point p1, int places)
        {
            return F.IsEqual( Point.RoundedPoint(this, places), Point.RoundedPoint(p1,places) );
        }

        public static Point RoundedPoint(Point p, int places)
        {
            return new Point( Math.Round( p.X, places ), Math.Round( p.Y, places ), Math.Round( p.Z, places ) );
        }
    }
}
