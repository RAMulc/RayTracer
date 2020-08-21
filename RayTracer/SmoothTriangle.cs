using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class SmoothTriangle : Triangle
    {
        public Vector N1 { get; set; }
        public Vector N2 { get; set; }
        public Vector N3 { get; set; }

        public SmoothTriangle(Point p1, Point p2, Point p3, Vector n1, Vector n2, Vector n3) : base( p1, p2, p3 )
        {
            N1 = n1;
            N2 = n2;
            N3 = n3;
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
            Intersection i = Intersection.IntersectionWithUV( t, this, u, v );
            xs.Add( i );
            return xs;
        }

        public override Vector LocalNormalAt(Point point, Intersection hit)
        {
            return this.N2 * hit.U + this.N3 * hit.V + this.N1 * ( 1 - hit.U - hit.V );
        }

    }
}
