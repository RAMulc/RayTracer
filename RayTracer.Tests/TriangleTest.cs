using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayTracer.Tests
{
    [TestClass]
    public class TriangleTest
    {
        [TestMethod]
        public void CreateTriangle()
        {
            Point p1 = new Point( 0, 1, 0 );
            Point p2 = new Point( -1, 0, 0 );
            Point p3 = new Point( 1, 0, 0 );

            Triangle t = new Triangle( p1, p2, p3 );

            bool b1 = Functions.IsEqual( p1, t.P1 );
            bool b2 = Functions.IsEqual( p2, t.P2 );
            bool b3 = Functions.IsEqual( p3, t.P3 );

            Assert.AreEqual( true, b1 );
            Assert.AreEqual( true, b2 );
            Assert.AreEqual( true, b3 );
        }

        [TestMethod]
        public void FindNormal()
        {
            Point p1 = new Point( 0, 1, 0 );
            Point p2 = new Point( -1, 0, 0 );
            Point p3 = new Point( 1, 0, 0 );

            Triangle t = new Triangle( p1, p2, p3 );

            Vector n1 = t.NormalAt( new Point( 0, 0.5, 0 ) );
            Vector n2 = t.NormalAt( new Point( -0.5, 0.75, 0 ) );
            Vector n3 = t.NormalAt( new Point( 0.5, 0.25, 0 ) );

            bool b1 = Functions.IsEqual( t.Normal, n1 );
            bool b2 = Functions.IsEqual( t.Normal, n2 );
            bool b3 = Functions.IsEqual( t.Normal, n3 );

            Assert.AreEqual( true, b1 );
            Assert.AreEqual( true, b2 );
            Assert.AreEqual( true, b3 );
        }

        [TestMethod]
        public void ParallelRayIntersect()
        {
            Point p1 = new Point( 0, 1, 0 );
            Point p2 = new Point( -1, 0, 0 );
            Point p3 = new Point( 1, 0, 0 );

            Triangle t = new Triangle( p1, p2, p3 );

            Ray r = new Ray( p1, t.E2 );

            Intersections xs = new Intersections();
            xs = t.LocalIntersect( xs, r );

            Assert.AreEqual( 0, xs.Count );
        }

        [TestMethod]
        public void RayIntersectMissp1p3()
        {
            Point p1 = new Point( 0, 1, 0 );
            Point p2 = new Point( -1, 0, 0 );
            Point p3 = new Point( 1, 0, 0 );

            Triangle t = new Triangle( p1, p2, p3 );

            Ray r = new Ray( new Point( 1, 1, -2 ), new Vector( 0, 0, 1 ) );

            Intersections xs = new Intersections();
            xs = t.LocalIntersect( xs, r );

            Assert.AreEqual( 0, xs.Count );
        }

        [TestMethod]
        public void RayIntersectMissp1p2()
        {
            Point p1 = new Point( 0, 1, 0 );
            Point p2 = new Point( -1, 0, 0 );
            Point p3 = new Point( 1, 0, 0 );

            Triangle t = new Triangle( p1, p2, p3 );

            Ray r = new Ray( new Point( -1, 1, -2 ), new Vector( 0, 0, 1 ) );

            Intersections xs = new Intersections();
            xs = t.LocalIntersect( xs, r );

            Assert.AreEqual( 0, xs.Count );
        }

        [TestMethod]
        public void RayIntersectMissp2p3()
        {
            Point p1 = new Point( 0, 1, 0 );
            Point p2 = new Point( -1, 0, 0 );
            Point p3 = new Point( 1, 0, 0 );

            Triangle t = new Triangle( p1, p2, p3 );

            Ray r = new Ray( new Point( 0, -1, -2 ), new Vector( 0, 0, 1 ) );

            Intersections xs = new Intersections();
            xs = t.LocalIntersect( xs, r );

            Assert.AreEqual( 0, xs.Count );
        }

        [TestMethod]
        public void RayIntersectStrike()
        {
            Point p1 = new Point( 0, 1, 0 );
            Point p2 = new Point( -1, 0, 0 );
            Point p3 = new Point( 1, 0, 0 );

            Triangle t = new Triangle( p1, p2, p3 );

            Ray r = new Ray( new Point( 0, 0.5, -2 ), new Vector( 0, 0, 1 ) );

            Intersections xs = new Intersections();
            xs = t.LocalIntersect( xs, r );

            Assert.AreEqual( 1, xs.Count );
            Assert.AreEqual( 2, xs[0].Tick );
        }
    }
}
