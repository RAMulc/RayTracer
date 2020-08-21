using System;

namespace RayTracer
{
    public class StripePattern : Pattern
    {
        public StripePattern(Color a, Color b, bool noise) : base( noise )
        {
            A = a;
            B = b;
        }

        public StripePattern(Pattern pA, Pattern pB, bool noise) : base( noise )
        {
            PatternA = pA;
            PatternB = pB;
        }

        public override Pattern NestedPatternAtShape(Pattern pA, Pattern pB, Shape shape, Point pt)
        {
            Math.DivRem( (int) Math.Floor( pt.X ), 2, out int rem );
            if ( rem == 0 )
                return pA;
            return pB;
        }

        public override Color PatternAt( Point pt)
        {
            Math.DivRem( (int) Math.Floor( pt.X ), 2, out int rem );
            if ( rem == 0 )
                return A;
            return B;
        }
    }
}
