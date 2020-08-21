using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayTracer.Tests
{
    [TestClass]
    public class PlaneTest
    {
        [TestMethod]
        public void NormalOfPlane()
        {
            Plane p1 = new Plane();
            Vector n1 = p1.LocalNormalAt( new Point( 0, 0, 0 ) );
            Vector n2 = p1.LocalNormalAt( new Point( 10, 0, -10 ) );
            Vector n3 = p1.LocalNormalAt( new Point( -5, 0, 150 ) );

            bool ie1 = n1.IsEqual( new Vector( 0, 1, 0 ) );
            bool ie2 = n1.IsEqual( new Vector( 0, 1, 0 ) );
            bool ie3 = n1.IsEqual( new Vector( 0, 1, 0 ) );

            Assert.AreEqual( ie1, true );
            Assert.AreEqual( ie2, true );
            Assert.AreEqual( ie3, true );
        }

        [TestMethod]
        public void IntersectParallelToPlane()
        {
            Plane p1 = new Plane();
            Ray r = new Ray( new Point( 0, 10, 0 ), new Vector( 0, 0, 1 ) );
            Intersections xs = new Intersections();
            xs = p1.LocalIntersect( xs, r );

            double l = xs.Count;

            Assert.AreEqual( l, 0 );
        }

        [TestMethod]
        public void IntersectCoPlanarToPlane()
        {
            Plane p1 = new Plane();
            Ray r = new Ray( new Point( 0, 0, 0 ), new Vector( 0, 0, 1 ) );
            Intersections xs = new Intersections();
            xs = p1.LocalIntersect( xs, r );

            double l = xs.Count;

            Assert.AreEqual( l, 0 );
        }

        [TestMethod]
        public void IntersectPlaneFromAbove()
        {
            Plane p1 = new Plane();
            Ray r = new Ray( new Point( 0, 1, 0 ), new Vector( 0, -1, 0 ) );
            Intersections xs = new Intersections();
            xs = p1.LocalIntersect( xs, r );

            double l = xs.Count;

            Assert.AreEqual( l, 1 );
            Assert.AreEqual( xs[0].Tick, 1 );
            Assert.AreEqual( p1, xs[0].Shape );
        }

        [TestMethod]
        public void IntersectPlaneFromBelow()
        {
            Plane p1 = new Plane();
            Ray r = new Ray( new Point( 0, -1, 0 ), new Vector( 0, 1, 0 ) );
            Intersections xs = new Intersections();
            xs = p1.LocalIntersect( xs, r );

            double l = xs.Count;

            Assert.AreEqual( l, 1 );
            Assert.AreEqual( xs[0].Tick, 1 );
            Assert.AreEqual( p1, xs[0].Shape );
        }
    }
}
