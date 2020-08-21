using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Triangle : Shape
    {
        public override Point Origin { get; set; }
        public override int ID { get; }
        public override Ray SavedRay { get; set; }

        public Point P1 { get; set; }
        public Point P2 { get; set; }
        public Point P3 { get; set; }

        public Vector E1 { get; }
        public Vector E2 { get; }

        public Vector Normal { get; }

        public Triangle(Point p1, Point p2, Point p3)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;

            E1 = P2 - P1;
            E2 = P3 - P1;
            Normal = Vector.Normalize( Functions.CrossProduct( E2, E1 ) );
        }

        public override BoundingBox BoundsOf()
        {
            BoundingBox b = new BoundingBox();
            b.AddPoint( P1 );
            b.AddPoint( P2 );
            b.AddPoint( P3 );
            return b;
        }

        public override void Divide(double threshhold)
        {
        }

        public override Intersections LocalIntersect(Intersections xs, Ray ray)
        {
            Vector dirCrossE2 = Functions.CrossProduct( ray.Direction, this.E2 );
            double det = Functions.DotProduct( this.E1, dirCrossE2 );
            if ( Math.Abs( det ) < Functions.Epsilon )
                return xs;

            double f = 1 / det;
            Vector plToOrigin = ray.Origin - this.P1;
            double u = f * Functions.DotProduct( plToOrigin, dirCrossE2 );
            if ( u < 0 || u > 1 )
                return xs;

            Vector originCrossE1 = Functions.CrossProduct( plToOrigin, this.E1 );
            double v = f * Functions.DotProduct( ray.Direction, originCrossE1 );
            if ( v < 0 || ( u + v ) > 1 )
                return xs;

            double t = f * Functions.DotProduct( this.E2, originCrossE1 );
            Intersection i = new Intersection( t, this );
            xs.Add( i );
            return xs;
        }

        public override Vector LocalNormalAt(Point point, Intersection hit)
        {
            return Normal;
        }
    }
}
