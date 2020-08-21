using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayTracer.Tests
{
    [TestClass]
    public class SmoothTriangleTest
    {
        [TestMethod]
        public void SmoothTriTest()
        {
            Point p1 = new Point( 0, 1, 0 );
            Point p2 = new Point( -1, 0, 0 );
            Point p3 = new Point( 1, 0, 0 );

            Vector n1 = new Vector( 0, 1, 0 );
            Vector n2 = new Vector( -1, 0, 0 );
            Vector n3 = new Vector( 1, 0, 0 );

            SmoothTriangle tri = new SmoothTriangle( p1, p2, p3, n1, n2, n3 );

            bool b1 = p1.IsEqual( tri.P1 );
            bool b2 = p2.IsEqual( tri.P2 );
            bool b3 = p3.IsEqual( tri.P3 );
            bool b4 = n1.IsEqual( tri.N1 );
            bool b5 = n2.IsEqual( tri.N2 );
            bool b6 = n3.IsEqual( tri.N3 );

            Assert.AreEqual( b1, true );
            Assert.AreEqual( b2, true );
            Assert.AreEqual( b3, true );
            Assert.AreEqual( b4, true );
            Assert.AreEqual( b5, true );
            Assert.AreEqual( b6, true );
        }

        [TestMethod]
        public void Intersections()
        {
            Point p1 = new Point( 0, 1, 0 );
            Point p2 = new Point( -1, 0, 0 );
            Point p3 = new Point( 1, 0, 0 );
            Shape s = new Triangle( p1, p2, p3 );
            Intersection i = Intersection.IntersectionWithUV( 3.5, s, 0.2, 0.4 );

            Assert.AreEqual( i.U, 0.2 );
            Assert.AreEqual( i.V, 0.4 );
        }

        [TestMethod]
        public void SmoothTriIntersect()
        {
            Point p1 = new Point( 0, 1, 0 );
            Point p2 = new Point( -1, 0, 0 );
            Point p3 = new Point( 1, 0, 0 );

            Vector n1 = new Vector( 0, 1, 0 );
            Vector n2 = new Vector( -1, 0, 0 );
            Vector n3 = new Vector( 1, 0, 0 );

            SmoothTriangle tri = new SmoothTriangle( p1, p2, p3, n1, n2, n3 );

            Ray r = new Ray( new Point( -0.2, 0.3, -2 ), new Vector( 0, 0, 1 ) );
            Intersections xs = new Intersections();
            tri.LocalIntersect( xs, r );

            bool b1 = Functions.IsEqual( xs[0].U, 0.45 );
            bool b2 = Functions.IsEqual( xs[0].V, 0.25 );

            Assert.AreEqual( b1, true );
            Assert.AreEqual( b2, true );
        }

        [TestMethod]
        public void LocalNormalAt()
        {
            Point p1 = new Point( 0, 1, 0 );
            Point p2 = new Point( -1, 0, 0 );
            Point p3 = new Point( 1, 0, 0 );

            Vector n1 = new Vector( 0, 1, 0 );
            Vector n2 = new Vector( -1, 0, 0 );
            Vector n3 = new Vector( 1, 0, 0 );

            SmoothTriangle tri = new SmoothTriangle( p1, p2, p3, n1, n2, n3 );
            Intersection i = Intersection.IntersectionWithUV( 1, tri, 0.45, 0.25 );
            Vector n = tri.NormalAt( new Point( 0, 0, 0 ), i );

            bool b1 = Functions.IsEqual( n, new Vector( -0.5547, 0.83205, 0 ) );

            Assert.AreEqual( b1, true );
        }

        [TestMethod]
        public void Normal()
        {
            Point p1 = new Point( 0, 1, 0 );
            Point p2 = new Point( -1, 0, 0 );
            Point p3 = new Point( 1, 0, 0 );

            Vector n1 = new Vector( 0, 1, 0 );
            Vector n2 = new Vector( -1, 0, 0 );
            Vector n3 = new Vector( 1, 0, 0 );

            SmoothTriangle tri = new SmoothTriangle( p1, p2, p3, n1, n2, n3 );
            Ray r = new Ray( new Point( -0.2, 0.3, -2 ), new Vector( 0, 0, 1 ) );
            Intersection i = Intersection.IntersectionWithUV( 1, tri, 0.45, 0.25 );
            Intersections xs = new Intersections();
            xs.Add( i );
            Computations c = xs.PrepareComputations( i, r, xs );

            bool b = Functions.IsEqual( c.NormalVector, new Vector( -0.5547, 0.83205, 0 ) );

            Assert.AreEqual( b, true );
        }
    }
}
