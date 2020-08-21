using System;

namespace RayTracer
{
    public class CheckerPattern : Pattern
    {
        public CheckerPattern(Color a, Color b, bool noise) : base( noise )
        {
            A = a;
            B = b;
        }

        public CheckerPattern(Pattern pA, Pattern pB, bool noise) : base( noise )
        {
            PatternA = pA;
            PatternB = pB;
        }

        public override Color PatternAt(Point pt)
        {
            Math.DivRem( (int) (Math.Floor( pt.X ) + Math.Floor( pt.Y ) + Math.Floor( pt.Z ) ), 2, out int rem );
            if ( rem == 0 )
                return A;
            return B;
        }

        public override Pattern NestedPatternAtShape(Pattern pA, Pattern pB, Shape shape, Point pt)
        {
            Math.DivRem( (int) ( Math.Floor( pt.X ) + Math.Floor( pt.Y ) + Math.Floor( pt.Z ) ), 2, out int rem );
            if ( rem == 0 )
                return pA;
            return pB;
        }
    }
}
