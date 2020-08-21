using System;

namespace RayTracer
{
    public class GradientPattern : Pattern
    {
        public GradientPattern(Color a, Color b, bool noise) : base( noise )
        {
            A = a;
            B = b;
        }

        public GradientPattern(Pattern pA, Pattern pB, bool noise) : base( noise )
        {
            PatternA = pA;
            PatternB = pB;
        }

        public override Pattern NestedPatternAtShape(Pattern pA, Pattern pB, Shape shape, Point pt)
        {
            RTuple distanceA = pA.A.Subtract( pB.A );
            RTuple distanceB = pA.B.Subtract( pB.B );
            double fraction = pt.X - Math.Floor( pt.X );

            RTuple rA = pA.A + distanceA.Multiply( fraction );
            RTuple rB = pB.B + distanceB.Multiply( fraction );

            return new GradientPattern( new Color( rA ), new Color( rB ), Noise );
        }

        public override Color PatternAt( Point pt)
        {
            RTuple distance = B.Subtract( A );
            double fraction = pt.X - Math.Floor( pt.X );
            RTuple r = A +  distance.Multiply(fraction);

            return new Color( r );
        }
    }
}
