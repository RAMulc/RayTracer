using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayTracer.Tests
{
    [TestClass]
    public class PatternTest
    {
        [TestMethod]
        public void TestStripePattern()
        {
            Color black = new Color( 0, 0, 0 );
            Color white = new Color( 1, 1, 1 );

            Pattern pattern = new StripePattern(black, white, false);

            bool ie1 = pattern.A.IsEqual( new Color( 0, 0, 0 ) );
            bool ie2 = pattern.B.IsEqual( new Color( 1, 1, 1 ) );

            Assert.AreEqual( ie1, true );
            Assert.AreEqual( ie2, true );
        }

        [TestMethod]
        public void TestStripePatternAtPointy()
        {
            Color black = new Color( 0, 0, 0 );
            Color white = new Color( 1, 1, 1 );

            Pattern pattern = new StripePattern( white, black, false );

            bool ie1 = pattern.PatternAt( new Point( 0, 0, 0 ) ).IsEqual( white );
            bool ie2 = pattern.PatternAt( new Point( 0, 1, 0 ) ).IsEqual( white );
            bool ie3 = pattern.PatternAt( new Point( 0, 2, 0 ) ).IsEqual( white );

            Assert.AreEqual( ie1, true );
            Assert.AreEqual( ie2, true );
            Assert.AreEqual( ie3, true );
        }

        [TestMethod]
        public void TestStripePatternAtPointz()
        {
            Color black = new Color( 0, 0, 0 );
            Color white = new Color( 1, 1, 1 );

            Pattern pattern = new StripePattern( white, black, false );

            bool ie1 = pattern.PatternAt( new Point( 0, 0, 0 ) ).IsEqual( white );
            bool ie2 = pattern.PatternAt( new Point( 0, 0, 1 ) ).IsEqual( white );
            bool ie3 = pattern.PatternAt( new Point( 0, 0, 2 ) ).IsEqual( white );

            Assert.AreEqual( ie1, true );
            Assert.AreEqual( ie2, true );
            Assert.AreEqual( ie3, true );
        }

        [TestMethod]
        public void TestStripePatternAtPointx()
        {
            Color black = new Color( 0, 0, 0 );
            Color white = new Color( 1, 1, 1 );

            Pattern pattern = new StripePattern( white, black, false );

            bool ie1 = pattern.PatternAt( new Point( 0, 0, 0 ) ).IsEqual( white );
            bool ie2 = pattern.PatternAt( new Point( 0.9, 0, 0 ) ).IsEqual( white );
            bool ie3 = pattern.PatternAt( new Point( 1, 0, 0 ) ).IsEqual( black );
            bool ie4 = pattern.PatternAt( new Point( -0.1, 0, 0 ) ).IsEqual( black );
            bool ie5 = pattern.PatternAt( new Point( -1, 0, 0 ) ).IsEqual( black );
            bool ie6 = pattern.PatternAt( new Point( -1.1, 0, 0 ) ).IsEqual( white );

            Assert.AreEqual( ie1, true );
            Assert.AreEqual( ie2, true );
            Assert.AreEqual( ie3, true );
            Assert.AreEqual( ie4, true );
            Assert.AreEqual( ie5, true );
            Assert.AreEqual( ie6, true );
        }

        [TestMethod]
        public void StripeObjectTransform()
        {
            Color black = new Color( 0, 0, 0 );
            Color white = new Color( 1, 1, 1 );

            Shape shape = new Sphere();
            shape.Transform.Scaling( 2, 2, 2 );
            Pattern pattern = new TestPattern( false );
            Color c = Pattern.PatternAtShape( pattern, shape, new Point( 2, 3, 4 ) );

            bool ie = c.IsEqual( new Color(1, 1.5, 2 ) );

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void StripePatternTransform()
        {
            Color black = new Color( 0, 0, 0 );
            Color white = new Color( 1, 1, 1 );

            Shape shape = new Sphere();
            //shape.Transform.Scaling( 2, 2, 2 );
            Pattern pattern = new TestPattern(false );
            pattern.Transform.Scaling( 2, 2, 2 );
            Color c = Pattern.PatternAtShape( pattern, shape, new Point( 2, 3, 4 ) );

            bool ie = c.IsEqual( new Color( 1, 1.5, 2 ) );

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void StripeShapePatternTransform()
        {
            Color black = new Color( 0, 0, 0 );
            Color white = new Color( 1, 1, 1 );

            Shape shape = new Sphere();
            shape.Transform.Scaling( 2, 2, 2 );
            Pattern pattern = new TestPattern(false );
            pattern.Transform.Translation( 0.5, 1, 1.5 );
            Color c = Pattern.PatternAtShape( pattern, shape, new Point( 2.5, 3, 3.5 ) );

            bool ie = c.IsEqual( new Color( 0.75, 0.5, 0.25 ) );

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void DefaultTransform()
        {
            Pattern pattern = new TestPattern(false);

            Matrix identity = Matrix.Identity( 4 );

            bool ie = pattern.Transform.TMatrix.IsEqual( identity );

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void AssignTransform()
        {
            Pattern pattern = new TestPattern(false);
            pattern.Transform.Translation( 1, 2, 3 );

            Transformation tr = new Transformation();
            tr.Translation( 1, 2, 3 );

            bool ie = pattern.Transform.IsEqual( tr );

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void Gradient()
        {
            Color black = new Color( 0, 0, 0 );
            Color white = new Color( 1, 1, 1 );

            Pattern pattern = new GradientPattern( white, black, false );

            Color a0 = pattern.PatternAt( new Point( 0, 0, 0 ) );
            Color a = pattern.PatternAt( new Point( 0.25, 0, 0 ) );
            Color b = pattern.PatternAt( new Point( 0.5, 0, 0 ) );
            Color c = pattern.PatternAt( new Point( 0.75, 0, 0 ) );

            bool ieA0 = a0.IsEqual( white );
            bool ieA = a.IsEqual( new Color( 0.75, 0.75, 0.75 ) );
            bool ieB = b.IsEqual( new Color( 0.5, 0.5, 0.5 ) );
            bool ieC = c.IsEqual( new Color( 0.25, 0.25, 0.25 ) );

            Assert.AreEqual( ieA0, true );
            Assert.AreEqual( ieA, true );
            Assert.AreEqual( ieB, true );
            Assert.AreEqual( ieC, true );
        }

        [TestMethod]
        public void Ring()
        {
            Color black = new Color( 0, 0, 0 );
            Color white = new Color( 1, 1, 1 );

            Pattern pattern = new RingPattern( white, black, false );

            Color a0 = pattern.PatternAt( new Point( 0, 0, 0 ) );
            Color a = pattern.PatternAt( new Point( 1, 0, 0 ) );
            Color b = pattern.PatternAt( new Point( 0, 0, 1 ) );
            Color c = pattern.PatternAt( new Point( 0.708, 0, 0.708 ) );

            bool ieA0 = a0.IsEqual( white );
            bool ieA = a.IsEqual( black );
            bool ieB = b.IsEqual( black );
            bool ieC = c.IsEqual( black );

            Assert.AreEqual( ieA0, true );
            Assert.AreEqual( ieA, true );
            Assert.AreEqual( ieB, true );
            Assert.AreEqual( ieC, true );
        }

        [TestMethod]
        public void CheckerX()
        {
            Color black = new Color( 0, 0, 0 );
            Color white = new Color( 1, 1, 1 );

            Pattern pattern = new CheckerPattern( white, black, false );

            Color a0 = pattern.PatternAt( new Point( 0, 0, 0 ) );
            Color a = pattern.PatternAt( new Point( 0.99, 0, 0 ) );
            Color c = pattern.PatternAt( new Point( 1.01, 0, 0 ) );

            bool ieA0 = a0.IsEqual( white );
            bool ieA = a.IsEqual( white );
            bool ieC = c.IsEqual( black );

            Assert.AreEqual( ieA0, true );
            Assert.AreEqual( ieA, true );
            Assert.AreEqual( ieC, true );
        }

        [TestMethod]
        public void CheckerY()
        {
            Color black = new Color( 0, 0, 0 );
            Color white = new Color( 1, 1, 1 );

            Pattern pattern = new CheckerPattern( white, black, false );

            Color a0 = pattern.PatternAt( new Point( 0, 0, 0 ) );
            Color a = pattern.PatternAt( new Point( 0, 0.99, 0 ) );
            Color c = pattern.PatternAt( new Point( 0, 1.01, 0 ) );

            bool ieA0 = a0.IsEqual( white );
            bool ieA = a.IsEqual( white );
            bool ieC = c.IsEqual( black );

            Assert.AreEqual( ieA0, true );
            Assert.AreEqual( ieA, true );
            Assert.AreEqual( ieC, true );
        }

        [TestMethod]
        public void CheckerZ()
        {
            Color black = new Color( 0, 0, 0 );
            Color white = new Color( 1, 1, 1 );

            Pattern pattern = new CheckerPattern( white, black, false );

            Color a0 = pattern.PatternAt( new Point( 0, 0, 0 ) );
            Color a = pattern.PatternAt( new Point( 0, 0, 0.99 ) );
            Color c = pattern.PatternAt( new Point( 0, 0, 1.01 ) );

            bool ieA0 = a0.IsEqual( white );
            bool ieA = a.IsEqual( white );
            bool ieC = c.IsEqual( black );

            Assert.AreEqual( ieA0, true );
            Assert.AreEqual( ieA, true );
            Assert.AreEqual( ieC, true );
        }
    }
}
