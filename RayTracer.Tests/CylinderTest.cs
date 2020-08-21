using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayTracer.Tests
{
    [TestClass]
    public class CylinderTest
    {
        [TestMethod]
        public void TestRayMiss()
        {
            Shape c = new Cylinder();
            Vector direction = new Vector( 0, 1, 0 );
            Ray r = new Ray( new Point( 1, 0, 0 ), direction );
            Intersections xs = new Intersections();
            xs = c.LocalIntersect( xs, r );

            Assert.AreEqual( xs.Count, 0 );

            direction = new Vector( 0, 1, 0 );
            r = new Ray( new Point( 0, 0, 0 ), direction );
            xs = c.LocalIntersect( xs, r );

            Assert.AreEqual( xs.Count, 0 );

            direction = new Vector( 1, 1, 1 );
            r = new Ray( new Point( 0, 0, -5 ), direction );
            xs = c.LocalIntersect( xs, r );

            Assert.AreEqual( xs.Count, 0 );
        }

        [TestMethod]
        public void TestRayHit()
        {
            Shape c = new Cylinder();
            Vector direction = Vector.Normalize(new Vector( 0, 0, 1 ));
            Ray r = new Ray( new Point( 1, 0, -5 ), direction );
            Intersections xs = new Intersections();
            xs = c.LocalIntersect( xs, r );

            Assert.AreEqual( xs.Count, 2 );
            Assert.AreEqual( xs[0].Tick, 5 );
            Assert.AreEqual( xs[1].Tick, 5 );

            direction = Vector.Normalize( new Vector( 0, 0, 1 ) );
            r = new Ray( new Point( 0, 0, -5 ), direction );
            xs = new Intersections();
            xs = c.LocalIntersect( xs, r );

            Assert.AreEqual( xs.Count, 2 );
            Assert.AreEqual( xs[0].Tick, 4 );
            Assert.AreEqual( xs[1].Tick, 6 );

            direction = Vector.Normalize( new Vector( 0.1, 1, 1 ) );
            r = new Ray( new Point( 0.5, 0, -5 ), direction );
            xs = new Intersections();
            xs = c.LocalIntersect( xs, r );

            bool ie1 = ( Math.Abs( xs[0].Tick - 6.80798 ) - Functions.Epsilon ) < 0;
            bool ie2 = ( Math.Abs( xs[1].Tick - 7.08872 ) - Functions.Epsilon ) < 0;

            Assert.AreEqual( xs.Count, 2 );
            Assert.AreEqual( ie1, true );
            Assert.AreEqual( ie2, true );
        }

        [TestMethod]
        public void NormalVector()
        {
            Shape c = new Cylinder();
            Vector n = c.LocalNormalAt( new Point( 1, 0, 0 ) );
            bool ie1 = n.IsEqual( new Vector( 1, 0, 0 ) );
            Assert.AreEqual( ie1, true );

            n = c.LocalNormalAt( new Point( 0, 5, -1 ) );
            bool ie2 = n.IsEqual( new Vector( 0, 0, -1 ) );
            Assert.AreEqual( ie2, true );

            n = c.LocalNormalAt( new Point( 0, -2, 1 ) );
            bool ie3 = n.IsEqual( new Vector( 0, 0, 1 ) );
            Assert.AreEqual( ie3, true );

            n = c.LocalNormalAt( new Point( -1, 1, 0 ) );
            bool ie4 = n.IsEqual( new Vector( -1, 0, 0 ) );
            Assert.AreEqual( ie4, true );
        }

        [TestMethod]
        public void MinMax()
        {
            Shape c = new Cylinder();

            bool ie1 = double.IsNegativeInfinity( c.Minimum );
            bool ie2 = double.IsPositiveInfinity( c.Maximum );

            //Assert.AreEqual( ie1, true );
            //Assert.AreEqual( ie2, true );
        }

        [TestMethod]
        public void IntersectConstrainedCylinder()
        {
            Shape c = new Cylinder
            {
                Minimum = 1,
                Maximum = 2,
                IsClosed = true
            };

            Vector direction = Vector.Normalize(new Vector( 0, -1, 0 ));
            Ray r = new Ray( new Point( 0, 3, 0 ), direction );
            Intersections xs = new Intersections();
            xs = c.LocalIntersect( xs, r );
            Assert.AreEqual( xs.Count, 2 );

            direction = Vector.Normalize( new Vector( 0, -1, 2 ));
            r = new Ray( new Point( 0, 3, -2 ), direction );
            xs = new Intersections();
            xs = c.LocalIntersect( xs, r );
            Assert.AreEqual( xs.Count, 2 );

            direction = Vector.Normalize( new Vector( 0, -1, 1 ) );
            r = new Ray( new Point( 0, 4, -2 ), direction );
            xs = new Intersections();
            xs = c.LocalIntersect( xs, r );
            Assert.AreEqual( xs.Count, 2 );

            direction = Vector.Normalize( new Vector( 0, 1, 2 ));
            r = new Ray( new Point( 0, 0, -2 ), direction );
            xs = new Intersections();
            xs = c.LocalIntersect( xs, r );
            Assert.AreEqual( xs.Count, 2 );

            direction = Vector.Normalize( new Vector( 0, 1, 1 ) );
            r = new Ray( new Point( 0, -1, -2 ), direction );
            xs = new Intersections();
            xs = c.LocalIntersect( xs, r );
            Assert.AreEqual( xs.Count, 2 );
        }

        [TestMethod]
        public void IsClosed()
        {
            Shape cyl = new Cylinder();

            Assert.AreEqual( cyl.IsClosed, false );
        }

        [TestMethod]
        public void IntersectClosedCylinder()
        {
            Shape c = new Cylinder
            {
                Minimum = 1,
                Maximum = 2,
                IsClosed = false
            };

            Vector direction = Vector.Normalize( new Vector( 0.1, 1, 0 ) );
            Ray r = new Ray( new Point( 0, 1.5, 0 ), direction );
            Intersections xs = new Intersections();
            xs = c.LocalIntersect( xs, r );
            Assert.AreEqual( xs.Count, 0 );

            direction = Vector.Normalize( new Vector( 0, 0, 1 ) );
            r = new Ray( new Point( 0, 3, -5 ), direction );
            xs = new Intersections();
            xs = c.LocalIntersect( xs, r );
            Assert.AreEqual( xs.Count, 0 );

            direction = Vector.Normalize( new Vector( 0, 0, 1 ) );
            r = new Ray( new Point( 0, 0, -5 ), direction );
            xs = new Intersections();
            xs = c.LocalIntersect( xs, r );
            Assert.AreEqual( xs.Count, 0 );

            direction = Vector.Normalize( new Vector( 0, 0, 1 ) );
            r = new Ray( new Point( 0, 2, -5 ), direction );
            xs = new Intersections();
            xs = c.LocalIntersect( xs, r );
            Assert.AreEqual( xs.Count, 0 );

            direction = Vector.Normalize( new Vector( 0, 0, 1 ) );
            r = new Ray( new Point( 0, 1, -5 ), direction );
            xs = new Intersections();
            xs = c.LocalIntersect( xs, r );
            Assert.AreEqual( xs.Count, 0 );

            direction = Vector.Normalize( new Vector( 0, 0, 1 ) );
            r = new Ray( new Point( 0, 1.5, -2 ), direction );
            xs = new Intersections();
            xs = c.LocalIntersect( xs, r );
            Assert.AreEqual( xs.Count, 2 );
        }

        [TestMethod]
        public void NormalVectorEndCap()
        {
            Shape c = new Cylinder
            {
                Minimum = 1,
                Maximum = 2,
                IsClosed = true
            };

            Vector n = c.LocalNormalAt( new Point( 0, 1, 0 ) );
            bool ie0 = n.IsEqual( new Vector( 0, -1, 0 ) );
            Assert.AreEqual( ie0, true );

            n = c.LocalNormalAt( new Point( 0.5, 1, 0 ) );
            ie0 = n.IsEqual( new Vector( 0, -1, 0 ) );
            Assert.AreEqual( ie0, true );

            n = c.LocalNormalAt( new Point( 0, 1, 0.5 ) );
            ie0 = n.IsEqual( new Vector( 0, -1, 0 ) );
            Assert.AreEqual( ie0, true );

            n = c.LocalNormalAt( new Point( 0, 2, 0 ) );
            ie0 = n.IsEqual( new Vector( 0, 1, 0 ) );
            Assert.AreEqual( ie0, true );

            n = c.LocalNormalAt( new Point( 0.5, 2, 0 ) );
            ie0 = n.IsEqual( new Vector( 0, 1, 0 ) );
            Assert.AreEqual( ie0, true );

            n = c.LocalNormalAt( new Point( 0, 2, 0.5 ) );
            ie0 = n.IsEqual( new Vector( 0, 1, 0 ) );
            Assert.AreEqual( ie0, true );
        }
    }
}
