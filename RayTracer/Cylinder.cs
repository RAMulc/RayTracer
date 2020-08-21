using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Cylinder : Shape
    {
        public override Point Origin { get; set; }
        public override int ID { get; }
        public override Ray SavedRay { get; set; }

        public override double Minimum { get; set; }
        public override double Maximum { get; set; }
        public override bool IsClosed { get; set; }

        public Cylinder(double minimum = double.NegativeInfinity, double maximum = double.PositiveInfinity, bool isClosed = false )
        {
            Minimum = minimum;
            Maximum = maximum;
            IsClosed = isClosed;
        }

        public override Intersections LocalIntersect(Intersections xs, Ray ray)
        {
            double a = Math.Pow(ray.Direction.X, 2) + Math.Pow(ray.Direction.Z, 2);
            if ( Math.Abs(a) <= Functions.Epsilon )
                return IntersectCaps( xs, ray );
            
            double b = 2 * ray.Origin.X * ray.Direction.X + 2 * ray.Origin.Z * ray.Direction.Z;
            double c = Math.Pow( ray.Origin.X, 2 ) + Math.Pow( ray.Origin.Z, 2 ) - 1;
            double disc = Math.Pow( b, 2 ) - 4 * a * c;
            if ( disc < 0 )
                return xs;

            double t0 = ( -b - Math.Sqrt( disc ) ) / ( 2 * a );
            double t1 = ( -b + Math.Sqrt( disc ) ) / ( 2 * a );

            if (t0 > t1 )
            {
                double temp = t0;
                t0 = t1;
                t1 = temp;
            }

            double count = 0;
            double y0 = ray.Origin.Y + t0 * ray.Direction.Y;
            if (this.Minimum < y0 && y0 < this.Maximum )
            {
                Intersection i0 = new Intersection( t0, this );
                xs.Add( i0 );
                count++;
            }
            double y1 = ray.Origin.Y + t1 * ray.Direction.Y;
            if ( this.Minimum < y1 && y1 < this.Maximum )
            {
                Intersection i1 = new Intersection( t1, this );
                xs.Add( i1 );
                count++;
            }

            if (count == 2)
                return xs;

            return IntersectCaps( xs, ray );
        }

        public override Vector LocalNormalAt(Point point, Intersection hit)
        {
            double dist = Math.Pow( point.X, 2 ) + Math.Pow( point.Z, 2 );

            if ( dist < 1 && point.Y >= ( this.Maximum - Functions.Epsilon ) )
                return new Vector( 0, 1, 0 );

            if ( dist < 1 && point.Y <= ( this.Minimum + Functions.Epsilon ) )
                return new Vector( 0, -1, 0 );

            return new Vector( point.X, 0, point.Z );
        }

        private Intersections IntersectCaps(Intersections xs, Ray r)
        {
            if (!this.IsClosed || Math.Abs( r.Direction.Y ) <= Functions.Epsilon)
                return xs;

            double t0 = ( this.Minimum - r.Origin.Y ) / r.Direction.Y;
            if ( CheckCap( r, t0 ) )
                xs.Add( new Intersection( t0, this ) );

            double t1 = ( this.Maximum - r.Origin.Y ) / r.Direction.Y;
            if ( CheckCap( r, t1 ) )
                xs.Add( new Intersection( t1, this ) );

            return xs;
        }

        private bool CheckCap(Ray r, double t)
        {
            double x = r.Origin.X + t * r.Direction.X;
            double z = r.Origin.Z + t * r.Direction.Z;

            return ( Math.Pow( x, 2 ) + Math.Pow( z, 2 ) ) <= 1;
        }

        public override BoundingBox BoundsOf()
        {
            BoundingBox b = new BoundingBox();
            b.AddPoint( new Point( -1, Minimum, -1 ) );
            b.AddPoint( new Point( 1, Maximum, 1 ) );
            return b;
        }

        public override void Divide(double threshhold)
        {
        }
    }
}
