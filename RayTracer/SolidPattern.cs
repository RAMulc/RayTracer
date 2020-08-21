namespace RayTracer
{
    public class SolidPattern : Pattern
    {
        public SolidPattern(Color a, bool noise) : base( noise )
        {
            A = a;
            B = a;
        }

        public override Pattern NestedPatternAtShape(Pattern pA, Pattern pB, Shape shape, Point pt)
        {
            Color a1 = (Color)(pA.A + pA.B + pB.A + pB.B).Multiply(0.25);
            return new SolidPattern(a1, Noise);
        }

        public override Color PatternAt(Point pt)
        {
            return A;
        }
    }
}
