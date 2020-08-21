using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class TestShape : Shape
    {
        public override Point Origin { get; set; }

        public override int ID { get; }
        public override Ray SavedRay { get; set; }

        public TestShape()
        {
            Origin = new Point(0, 0, 0);
            ID = Count;
        }

        public override Intersections LocalIntersect(Intersections xs, Ray ray)
        {
            SavedRay = ray;
            return new Intersections();
        }

        public override Vector LocalNormalAt(Point point, Intersection hit)
        {
            return new Vector( point.X, point.Y, point.Z );
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
