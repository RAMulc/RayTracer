using System;
using F=RayTracer.Functions;

namespace RayTracer
{
    public class RTuple
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double W { get; set; }

        public RTuple(){}
        
        public RTuple(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static RTuple operator -(RTuple t)
        {
            return F.Negate(t);
        }

        public static RTuple operator +(RTuple t1, RTuple t2)
        {
            return F.Add(t1, t2);
        }

        public static RTuple operator *(RTuple t, double d)
        {
            return F.Multiply(t, d);
        }

        public static RTuple operator /(RTuple t, double d)
        {
            return F.Division(t, d);
        }

        public static bool operator == (RTuple t1, RTuple t2)
        {
            return F.IsEqual( this, t );
        }

        public static bool operator !=(RTuple t1, RTuple t2)
        {
            return !F.IsEqual( this, t );
        }

        public double Magnitude
        {
            get { return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2) + Math.Pow(W, 2)); }
        }

        public RTuple Add(RTuple t)
        {
            return F.Add(this, t);
        }

        public RTuple Subtract(RTuple t)
        {
            return F.Subtract(this, t);
        }

        public RTuple Multiply(double scalar)
        {
            return F.Multiply(this, scalar);
        }

        public RTuple Divide(double scalar)
        {
            return F.Division(this, scalar);
        }

        public bool IsEqual(RTuple t)
        {
            return F.IsEqual(this, t);
        }
    }
}
