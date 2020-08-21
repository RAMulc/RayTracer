using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayTracer.Tests
{
    [TestClass]
    public class CubeTest
    {
        [TestMethod]
        public void RayCubeIntersection()
        {
            Shape s = new Cube();
            Intersections xs = new Intersections();
            Ray r1 = new Ray( new Point( 5, 0.5, 0 ), new Vector( -1, 0, 0 ) );
            Intersection i1 = new Intersection();
            xs = s.LocalIntersect( xs, r1 );
            Assert.AreEqual( xs[0].Tick, 4 );
            Assert.AreEqual( xs[1].Tick, 6 );

            xs = new Intersections();
            r1 = new Ray( new Point( -5, 0.5, 0 ), new Vector( 1, 0, 0 ) );
            i1 = new Intersection();
            xs = s.LocalIntersect( xs, r1 );
            Assert.AreEqual( xs[0].Tick, 4 );
            Assert.AreEqual( xs[1].Tick, 6 );

            xs = new Intersections();
            r1 = new Ray( new Point( 0.5, 5, 0 ), new Vector( 0, -1, 0 ) );
            i1 = new Intersection();
            xs = s.LocalIntersect( xs, r1 );
            Assert.AreEqual( xs[0].Tick, 4 );
            Assert.AreEqual( xs[1].Tick, 6 );

            xs = new Intersections();
            r1 = new Ray( new Point( 0.5, -5, 0 ), new Vector( 0, 1, 0 ) );
            i1 = new Intersection();
            xs = s.LocalIntersect( xs, r1 );
            Assert.AreEqual( xs[0].Tick, 4 );
            Assert.AreEqual( xs[1].Tick, 6 );

            xs = new Intersections();
            r1 = new Ray( new Point( 0.5, 0, 5 ), new Vector( 0, 0, -1 ) );
            i1 = new Intersection();
            xs = s.LocalIntersect( xs, r1 );
            Assert.AreEqual( xs[0].Tick, 4 );
            Assert.AreEqual( xs[1].Tick, 6 );

            xs = new Intersections();
            r1 = new Ray( new Point( 0.5, 0, -5 ), new Vector( 0, 0, 1 ) );
            i1 = new Intersection();
            xs = s.LocalIntersect( xs, r1 );
            Assert.AreEqual( xs[0].Tick, 4 );
            Assert.AreEqual( xs[1].Tick, 6 );

            xs = new Intersections();
            r1 = new Ray( new Point( 0, 0.5, 0 ), new Vector( 0, 0, 1 ) );
            i1 = new Intersection();
            xs = s.LocalIntersect( xs, r1 );
            Assert.AreEqual( xs[0].Tick, -1 );
            Assert.AreEqual( xs[1].Tick, 1 );
        }

        [TestMethod]
        public void RayCubeIntersectionMiss()
        {
            Shape s = new Cube();
            Intersections xs = new Intersections();
            Ray r1 = new Ray( new Point( -2, 0, 0 ), new Vector( 0.2673, 0.5345, 0.8018 ) );
            Intersection i1 = new Intersection();
            xs = s.LocalIntersect( xs, r1 );
            Assert.AreEqual( xs.Count, 0 );

            xs = new Intersections();
            r1 = new Ray( new Point( 0, -2, 0 ), new Vector( 0.8018, 0.2673, 0.5345 ) );
            i1 = new Intersection();
            xs = s.LocalIntersect( xs, r1 );
            Assert.AreEqual( xs.Count, 0 );

            xs = new Intersections();
            r1 = new Ray( new Point( 0, 0, -2 ), new Vector( 0.5345, 0.8018, 0.2673 ) );
            i1 = new Intersection();
            xs = s.LocalIntersect( xs, r1 );
            Assert.AreEqual( xs.Count, 0 );

            xs = new Intersections();
            r1 = new Ray( new Point( 2, 0, 2 ), new Vector( 0, 0, -1 ) );
            i1 = new Intersection();
            xs = s.LocalIntersect( xs, r1 );
            Assert.AreEqual( xs.Count, 0 );

            xs = new Intersections();
            r1 = new Ray( new Point( 0, 2, 2 ), new Vector( 0, -1, 0 ) );
            i1 = new Intersection();
            xs = s.LocalIntersect( xs, r1 );
            Assert.AreEqual( xs.Count, 0 );

            xs = new Intersections();
            r1 = new Ray( new Point( 2, 2, 0 ), new Vector( -1, 0, 0 ) );
            i1 = new Intersection();
            xs = s.LocalIntersect( xs, r1 );
            Assert.AreEqual( xs.Count, 0 );
        }

        [TestMethod]
        public void NormalToCube()
        {
            Shape s = new Cube();
            Point p = new Point( 1, 0.5, -0.8 );
            Vector norm = s.LocalNormalAt( p );
            bool ie = norm.IsEqual( new Vector( 1, 0, 0 ) );
            Assert.AreEqual( ie, true );

            p = new Point( -1, -0.2, 0.9 );
            norm = s.LocalNormalAt( p );
            ie = norm.IsEqual( new Vector( -1, 0, 0 ) );
            Assert.AreEqual( ie, true );

            p = new Point( -0.4, 1, -0.1 );
            norm = s.LocalNormalAt( p );
            ie = norm.IsEqual( new Vector( 0, 1, 0 ) );
            Assert.AreEqual( ie, true );

            p = new Point( 0.3, -1, -0.7 );
            norm = s.LocalNormalAt( p );
            ie = norm.IsEqual( new Vector( 0, -1, 0 ) );
            Assert.AreEqual( ie, true );

            p = new Point( -0.6, 0.3, 1 );
            norm = s.LocalNormalAt( p );
            ie = norm.IsEqual( new Vector( 0, 0, 1 ) );
            Assert.AreEqual( ie, true );

            p = new Point( 0.4, 0.4, -1 );
            norm = s.LocalNormalAt( p );
            ie = norm.IsEqual( new Vector( 0, 0, -1 ) );
            Assert.AreEqual( ie, true );

            p = new Point( 1, 1, 1 );
            norm = s.LocalNormalAt( p );
            ie = norm.IsEqual( new Vector( 1, 0, 0 ) );
            Assert.AreEqual( ie, true );

            p = new Point( -1, -1, -1 );
            norm = s.LocalNormalAt( p );
            ie = norm.IsEqual( new Vector( -1, 0, 0 ) );
            Assert.AreEqual( ie, true );
        }
    }
}
