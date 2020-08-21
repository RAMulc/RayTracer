using System;

namespace RayTracer
{
    public class RingPattern : Pattern
    {
        public RingPattern(Color a, Color b, bool noise) : base( noise )
        {
            A = a;
            B = b;
        }

        public RingPattern(Pattern pA, Pattern pB, bool noise) : base( noise )
        {
            PatternA = pA;
            PatternB = pB;
        }

        public override Pattern NestedPatternAtShape(Pattern pA, Pattern pB, Shape shape, Point pt)
        {
            double val = Math.Floor( Math.Sqrt( Math.Pow( pt.X, 2 ) + Math.Pow( pt.Z, 2 ) ) );
            Math.DivRem( (int) val, 2, out int rem );

            if ( rem == 0 )
                return pA;

            return pB;
        }

        public override Color PatternAt(Point pt)
        {
            double val = Math.Floor( Math.Sqrt( Math.Pow( pt.X, 2 ) + Math.Pow( pt.Z, 2 ) ) );
            Math.DivRem( (int)val, 2, out int rem );

            if ( rem == 0 )
                return A;

            return B;
        }
    }
}
