using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class BoundingBox
    {
        public Point Min { get; set; }
        public Point Max { get; set; }

        public BoundingBox Left
        {
            get { return SplitBounds( this )[0]; }
        }
        public BoundingBox Right
        {
            get { return SplitBounds( this )[1]; }
        }

        public BoundingBox()
        {
            Min = new Point( double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity );
            Max = new Point( double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity );
        }

        public BoundingBox(Point min, Point max)
        {
            Min = min;
            Max = max;
        }

        public BoundingBox(Shape s)
        {
            Min = s.ShapeBounds.Min;
            Max = s.ShapeBounds.Max;
        }

        public void AddPoint(Point point)
        {
            this.Min.X = Math.Min( this.Min.X, point.X );
            this.Min.Y = Math.Min( this.Min.Y, point.Y );
            this.Min.Z = Math.Min( this.Min.Z, point.Z );

            this.Max.X = Math.Max( this.Max.X, point.X );
            this.Max.Y = Math.Max( this.Max.Y, point.Y );
            this.Max.Z = Math.Max( this.Max.Z, point.Z );
        }

        public void AddBoundingBox(BoundingBox b)
        {
            this.AddPoint( b.Min );
            this.AddPoint( b.Max );
        }

        public bool ContainsPoint(Point p)
        {
            if ( p.X >= this.Min.X && p.X <= this.Max.X )
                if ( p.Y >= this.Min.Y && p.Y <= this.Max.Y )
                    if ( p.Z >= this.Min.Z && p.Z <= this.Max.Z )
                        return true;
            return false;
        }

        public bool ContainsBoundingBox(BoundingBox b)
        {
            if ( this.ContainsPoint( b.Min ) && this.ContainsPoint( b.Max ) )
                return true;
            return false;
        }

        public static BoundingBox Transform(BoundingBox bIn, Transformation m)
        {
            Point[] p = new Point[8];

            p[0] = bIn.Min;
            p[1] = new Point( bIn.Min.X, bIn.Min.Y, bIn.Max.Z );
            p[2] = new Point( bIn.Min.X, bIn.Max.Y, bIn.Min.Z );
            p[3] = new Point( bIn.Min.X, bIn.Max.Y, bIn.Max.Z );
            p[4] = new Point( bIn.Max.X, bIn.Min.Y, bIn.Min.Z );
            p[5] = new Point( bIn.Max.X, bIn.Min.Y, bIn.Max.Z );
            p[6] = new Point( bIn.Max.X, bIn.Max.Y, bIn.Min.Z );
            p[7] = bIn.Max;

            BoundingBox bOut = new BoundingBox();
            foreach ( Point point in p )
                bOut.AddPoint( new Point( m.TMatrix * point ) );

            return bOut;
        }

        public static bool Intersects(BoundingBox b, Ray ray, ref List<double> t)
        {
            t = new List<double>();
            if ( !BoxHasIntersection( ray.Origin, ray.Direction, b.Min, b.Max ) )
                return false;

            double[] xt = CheckAxis( ray.Origin.X, ray.Direction.X, b.Min.X, b.Max.X );
            double[] yt = CheckAxis( ray.Origin.Y, ray.Direction.Y, b.Min.Y, b.Max.Y );
            double[] zt = CheckAxis( ray.Origin.Z, ray.Direction.Z, b.Min.Z, b.Max.Z );

            double tMin = MaxVal( xt[0], yt[0], zt[0] );
            double tMax = MinVal( xt[1], yt[1], zt[1] );

            if ( tMin > tMax )
                return false;

            t.Add( tMin );
            t.Add( tMax );
            return true;
        }

        private static bool BoxHasIntersection(Point origin, Vector direction, Point min, Point max)
        {
            if ( ( origin.X < min.X && direction.X < 0 ) || ( origin.Y < min.Y && direction.Y < 0 ) || ( origin.Z < min.Z && direction.Z < 0 ) )
                return false;
            if ( ( origin.X > max.X && direction.X > 0 ) || ( origin.Y > max.Y && direction.Y > 0 ) || ( origin.Z > max.Z && direction.Z > 0 ) )
                return false;
            if ( Math.Abs( direction.X ) >= Functions.Epsilon ||
                    Math.Abs( direction.Y ) >= Functions.Epsilon ||
                    Math.Abs( direction.Z ) >= Functions.Epsilon )
                return true;
            return false;
        }

        private static double[] CheckAxis(double origin, double direction, double min, double max)
        {
            double tMinNumerator = min - origin;
            double tMaxNumerator = max - origin;

            double tMin;
            double tMax;
            if ( Math.Abs( direction ) >= Functions.Epsilon )
            {
                tMin = tMinNumerator / direction;
                tMax = tMaxNumerator / direction;
            }
            else
            {
                tMin = tMinNumerator * double.PositiveInfinity;
                tMax = tMaxNumerator * double.PositiveInfinity;
            }

            if ( tMin > tMax )
            {
                double temp = tMin;
                tMin = tMax;
                tMax = temp;
            }

            return new double[2] { tMin, tMax };
        }

        public static double MinVal(double a, double b, double c)
        {
            double d = Math.Min( a, b );
            return Math.Min( c, d );
        }

        public static double MaxVal(double a, double b, double c)
        {
            double d = Math.Max( a, b );
            return Math.Max( c, d );
        }

        public static List<BoundingBox> SplitBounds(BoundingBox box)
        {
            double dx = box.Max.X - box.Min.X;
            double dy = box.Max.Y - box.Min.Y;
            double dz = box.Max.Z - box.Min.Z;

            double max = MaxVal( dx, dy, dz );

            Point p0 = new Point( box.Min.X, box.Min.Y, box.Min.Z );
            Point p1 = new Point( box.Max.X, box.Max.Y, box.Max.Z );

            if ( ( max - dx ) < Functions.Epsilon )
            {
                p1.X = p0.X + dx / 2;
                p0.X = p1.X;
            }
            else if ( ( max - dy ) < Functions.Epsilon )
            {
                p1.Y = p0.Y + dy / 2;
                p0.Y = p1.Y;
            }
            else
            {
                p1.Z = p0.Z + dz / 2;
                p0.Z = p1.Z;
            }

            List<BoundingBox> boundingBoxes = new List<BoundingBox>
            {
                new BoundingBox( box.Min, p1 ),
                new BoundingBox( p0, box.Max )
            };

            return boundingBoxes;
        }
    }
}
