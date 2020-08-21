using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Plane : Shape
    {
        public override Point Origin { get; set; }
        public override int ID { get; }

        public override Ray SavedRay { get; set; }

        public override BoundingBox BoundsOf()
        {
            BoundingBox b = new BoundingBox();
            b.AddPoint( new Point( double.NegativeInfinity, 0, double.NegativeInfinity ) );
            b.AddPoint( new Point( double.PositiveInfinity, 0, double.PositiveInfinity ) );
            return b;
        }

        public override void Divide(double threshhold)
        {
        }

        public override Intersections LocalIntersect(Intersections xs, Ray ray)
        {
            if ( Math.Abs(ray.Direction.Y) < Functions.Epsilon )
                return xs;
            xs.Add( new Intersection( -ray.Origin.Y / ray.Direction.Y, this ) );
            return xs;
        }

        public override Vector LocalNormalAt(Point point, Intersection hit = null)
        {
            return new Vector( 0, 1, 0 );
        }
    }
}
