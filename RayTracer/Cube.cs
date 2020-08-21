using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Cube : Shape
    {
        public override Point Origin { get; set; }
        public override int ID { get; }
        public override Ray SavedRay { get; set; }

        public override Intersections LocalIntersect(Intersections xs, Ray ray)
        {
            List<double> t = new List<double>();
            if (BoundingBox.Intersects(this.BoundsOf(), ray, ref t ) )
            {
                Intersection i1 = new Intersection( t[0], this );
                Intersection i2 = new Intersection( t[1], this );

                xs.Add( i1 );
                xs.Add( i2 );
            }
            return xs;
        }

        public override Vector LocalNormalAt(Point point, Intersection hit)
        {
            double maxC = BoundingBox.MaxVal( Math.Abs( point.X ), Math.Abs( point.Y ), Math.Abs( point.Z ) );

            if ( maxC == Math.Abs( point.X ) )
                return new Vector( point.X, 0, 0 );
            else if ( maxC == Math.Abs( point.Y ) )
                return new Vector( 0, point.Y, 0 );
            return new Vector( 0, 0, point.Z );
        }

        public override BoundingBox BoundsOf()
        {
            BoundingBox b = new BoundingBox();
            b.AddPoint( new Point( -1, -1, -1 ) );
            b.AddPoint( new Point( 1, 1, 1 ) );
            return b;
        }

        public override void Divide(double threshhold)
        {
        }
    }
}
