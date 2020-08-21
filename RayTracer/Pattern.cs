using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public abstract class Pattern
    {
        public Color A { get; set; }
        public Color B { get; set; }
        public Pattern PatternA { get; set; }
        public Pattern PatternB { get; set; }
        public Transformation Transform { get; set; }
        public bool Noise { get; set; }

        public Pattern(bool noise)
        {
            Transform = new Transformation();
            Noise = noise;
        }

        public void SetTransform(Transformation t)
        {
            Transform = t;
        }

        public abstract Color PatternAt( Point pt);
        public abstract Pattern NestedPatternAtShape(Pattern pA, Pattern pB, Shape shape, Point pt);

        public static Color PatternAtShape(Pattern pattern, Shape shape, Point worldPoint)
        {
            //Point objectPoint = new Point( shape.Transform.Inverse() * worldPoint );
            Point objectPoint = Shape.WorldToObject( shape, worldPoint );

            if ( pattern.Noise )
            {
                double noise = PerlinNoise.Noise( objectPoint.X, objectPoint.Y, objectPoint.Z ) ;
                objectPoint.X = objectPoint.X + noise;
                objectPoint.Y = objectPoint.X + noise;
                objectPoint.Z = objectPoint.X + noise;
            }

            Pattern p = pattern;
            if ( pattern.PatternA != null && pattern.PatternB != null )
                p = pattern.NestedPatternAtShape( pattern.PatternA, pattern.PatternB, shape, objectPoint );
            Point patternPoint = new Point( p.Transform.Inverse() * objectPoint );
           return p.PatternAt( patternPoint );
            
        }

        public static Pattern BlendedPattern(Pattern pA, Pattern pB)
        {
            switch ( pA )
            {
                case CheckerPattern checkerPattern:
                    return new CheckerPattern( pA.A, pB.B, pA.Noise );
                case GradientPattern gradientPattern:
                    return new GradientPattern( pA.A, pB.B, pA.Noise );
                default:
                    return new SolidPattern( pA.A, pA.Noise );
            }
        }

    }

    public class TestPattern : Pattern
    {
        public TestPattern(bool noise) : base( noise )
        {
        }

        public override Pattern NestedPatternAtShape(Pattern pA, Pattern pB, Shape shape, Point pt)
        {
            return pA;
        }

        public override Color PatternAt(Point pt)
        {
            return new Color( pt.X, pt.Y, pt.Z );
        }
    }
}
