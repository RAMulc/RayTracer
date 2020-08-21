using System;
using F = RayTracer.Functions;

namespace RayTracer
{
    public class Vector :RTuple
    {
        public Vector(double x, double y, double z) : base(x, y, z, 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector(RTuple rTuple) : base(rTuple.X, rTuple.Y, rTuple.Z, rTuple.W)
        {
            X = rTuple.X;
            Y = rTuple.Y;
            Z = rTuple.Z;
        }

        new public double Magnitude
        {
            get { return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2)); }
        }

        public void Normalize()
        {
            double mag = Magnitude;
            X = X / mag;
            Y = Y / mag;
            Z = Z / mag;
        }

        public static Vector Normalize(Vector v)
        {
            return F.Normalize(v);
        }

        public Vector Reflect(Vector normal)
        {
            return this.Subtract(normal.Multiply(2 * Dot(normal)));
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return F.Subtract(v1,v2);
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return F.Add(v1, v2);
        }

        public static Vector operator *(Vector v, double d)
        {
            return F.Multiply(v, d);
        }

        public static Vector operator /(Vector v, double d)
        {
            return F.Multiply(v, 1 / d);
        }

        public Vector Add(Vector v)
        {
            return F.Add(this, v);
        }

        public Vector Subtract(Vector v)
        {
            return F.Subtract(this, v);
        }

        new public Vector Multiply(double scalar)
        {
            return F.Multiply(this, scalar);
        }

        public double Dot(Vector v)
        {
            return F.DotProduct(this, v);
        }

        public Vector Cross(Vector v)
        {
            return F.CrossProduct(this, v);
        }

        public bool IsEqual(Vector v)
        {
            return F.IsEqual(this, v);
        }

        public static Vector Negate(Vector v)
        {
            return new Vector( -v.X, -v.Y, -v.Z );
        }
        
    }
}
