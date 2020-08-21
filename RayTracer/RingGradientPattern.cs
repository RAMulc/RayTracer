using System;

namespace RayTracer
{
    public class RingGradientPattern : Pattern
    {
        public RingGradientPattern(Color a, Color b, bool noise) : base( noise )
        {
            A = a;
            B = b;
        }

        public RingGradientPattern(Pattern pA, Pattern pB, bool noise) : base( noise )
        {
            PatternA = pA;
            PatternB = pB;
        }

        public override Pattern NestedPatternAtShape(Pattern pA, Pattern pB, Shape shape, Point pt)
        {
            RTuple distanceA = pA.B.Subtract( pA.A );
            RTuple distanceB = pB.B.Subtract( pB.A );

            double d = Math.Sqrt( Math.Pow( pt.X, 2 ) + Math.Pow( pt.Z, 2 ) );
            double fraction = d - Math.Floor( d );

            RTuple rA = pA.A + distanceA.Multiply( fraction );
            RTuple rB = pB.A + distanceB.Multiply( fraction );

            return new RingGradientPattern( new Color( rA ), new Color( rB ), Noise );
        }

        public override Color PatternAt(Point pt)
        {
            RTuple distance = B.Subtract( A );
            double d = Math.Sqrt( Math.Pow( pt.X, 2 ) + Math.Pow( pt.Z, 2 ) );
            double fraction = d - Math.Floor(d );
            RTuple r = A + distance.Multiply( fraction );

            return new Color( r );
        }
    }
}
