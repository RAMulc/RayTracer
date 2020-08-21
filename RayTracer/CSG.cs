using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class CSG : Group
    {
        public CSGOperation Operation;
        public new Shape Left;
        public new Shape Right;

        public CSG(CSGOperation operation, Shape s1, Shape s2)
        {
            Operation = operation;
            AddChild( s1 );
            AddChild( s2 );
            Left = s1;
            Right = s2;
        }

        public new void AddChild(Shape s)
        {
            s.Parent = this;
        }

        public override Intersections LocalIntersect(Intersections xs, Ray ray)
        {
            Intersections leftxs = new Intersections();
            leftxs = Left.Intersect( xs, ray );
            Intersections rightxs = new Intersections();
            rightxs = Right.Intersect( xs, ray );

            foreach ( Intersection i in leftxs.IntersectionList )
                xs.Add(new Intersection(i.Tick, i.Shape));
            foreach ( Intersection i in rightxs.IntersectionList )
                xs.Add( new Intersection( i.Tick, i.Shape ) );

            return xs;
        }

        public bool IntersectionAllowed(bool leftHit,  bool inLeft, bool inRight)
        {
            switch ( Operation )
            {
                case CSGOperation.Difference:
                    return ( leftHit && !inRight ) || ( !leftHit && inLeft );
                case CSGOperation.Intersection:
                    return ( leftHit && inRight ) || ( !leftHit && inLeft );
                case CSGOperation.Union:
                    return ( leftHit && !inRight ) || ( !leftHit && !inLeft );
                default:
                    return false;
            }
        }

        public Intersections FilterIntersections(Intersections xs)
        {
            bool inLeft = false;
            bool inRight = false;

            Intersections nXs = new Intersections();

            foreach (Intersection i in xs.IntersectionList )
            {
                bool leftHit = IncludesObject(Left, i.Shape);

                if ( IntersectionAllowed( leftHit, inLeft, inRight ) )
                    nXs.Add( i );

                if ( leftHit )
                    inLeft = !inLeft;
                else
                    inRight = !inRight;
            }
            return nXs;
        }

        private static bool IncludesObject(Shape A, Shape B)
        {
            switch ( B )
            {
                case CSG csg:
                    if ( A == csg.Left || A == csg.Right )
                        return true;
                    return false;
                case Group g:
                    for (int c = 0; c < g.Children.Count; c++ )
                    {
                        switch ( g.Children[c] )
                        {
                            case Group cg:
                                IncludesObject( A, cg );
                                break;
                            default:
                                if ( A == g.Children[c] )
                                    return true;
                                break;
                        }
                    }
                    return false;
                case Shape s:
                    if ( A == s )
                        return true;
                    return false;
                default:
                    return false;
            }
        }
    }
}
