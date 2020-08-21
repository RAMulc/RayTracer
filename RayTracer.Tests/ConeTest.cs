using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayTracer.Tests
{
    [TestClass]
    public class ConeTest
    {
        [TestMethod]
        public void ConeRayIntersection()
        {
            Shape cone = new Cone();
            Vector direction = Vector.Normalize( new Vector( 0, 0, 1 ));
            Ray ray = new Ray( new Point( 0, 0, -5 ), direction );
            Intersections xs = new Intersections();
            xs = cone.LocalIntersect( xs, ray );

            Assert.AreEqual( xs.Count, 2 );
            Assert.AreEqual( xs[0].Tick, 5 );
            Assert.AreEqual( xs[1].Tick, 5 );

            direction = Vector.Normalize( new Vector( 1, 1, 1 ));
            ray = new Ray( new Point( 0, 0, -5 ), direction );
            xs = new Intersections();
            xs = cone.LocalIntersect( xs, ray );

            Assert.AreEqual( xs.Count, 2 );
            bool ie1 = (xs[0].Tick - 8.66025) < Functions.Epsilon;
            Assert.AreEqual( ie1, true );
            ie1 = ( xs[1].Tick - 8.66025 ) < Functions.Epsilon;
            Assert.AreEqual( ie1, true );

            direction = Vector.Normalize( new Vector( -0.5, -1, 1 ));
            ray = new Ray( new Point( 1, 1, -5 ), direction );
            xs = new Intersections();
            xs = cone.LocalIntersect( xs, ray );

            Assert.AreEqual( xs.Count, 2 );
            ie1 = ( xs[0].Tick - 4.55006 ) < Functions.Epsilon;
            Assert.AreEqual( ie1, true );
            ie1 = ( xs[1].Tick - 49.44994 ) < Functions.Epsilon;
            Assert.AreEqual( ie1, true );
        }

        [TestMethod]
        public void ConeRaySingleIntersection()
        {
            Shape cone = new Cone();
            Vector direction = Vector.Normalize( new Vector( 0, 1, 1 ));
            Ray ray = new Ray( new Point( 0, 0, -1 ), direction );
            Intersections xs = new Intersections();
            xs = cone.LocalIntersect( xs, ray );

            Assert.AreEqual( xs.Count, 1 );
            bool ie1 = ( xs[0].Tick - 0.35355 ) < Functions.Epsilon;
            Assert.AreEqual( ie1, true );
        }

        [TestMethod]
        public void ConeEndCapIntersection()
        {
            Shape cone = new Cone();
            cone.Minimum = -0.5;
            cone.Maximum = 0.5;
            cone.IsClosed = true;

            Vector direction = Vector.Normalize( new Vector( 0, 1, 0 ) );
            Ray ray = new Ray( new Point( 0, 0, -5 ), direction );
            Intersections xs = new Intersections();
            xs = cone.LocalIntersect( xs, ray );
            Assert.AreEqual( xs.Count, 0 );

            direction = Vector.Normalize( new Vector( 0, 1, 1 ) );
            ray = new Ray( new Point( 0, 0, -0.25 ), direction );
            xs = new Intersections();
            xs = cone.LocalIntersect( xs, ray );
            Assert.AreEqual( xs.Count, 2 );

            direction = Vector.Normalize( new Vector( 0, 1, 0 ) );
            ray = new Ray( new Point( 0, 0, -0.25 ), direction );
            xs = new Intersections();
            xs = cone.LocalIntersect( xs, ray );
            Assert.AreEqual( xs.Count, 4 );
        }

        [TestMethod]
        public void NormalToCone()
        {
            Shape c = new Cone();

            Vector n = c.LocalNormalAt( new Point( 0, 0, 0 ) );
            bool ie = n.IsEqual( new Vector( 0, 0, 0 ) );
            Assert.AreEqual( ie, true );

            n = c.LocalNormalAt( new Point( 1, 1, 1 ) );
            ie = n.IsEqual( new Vector( 1, -Math.Sqrt(2), 1 ) );
            Assert.AreEqual( ie, true );

            n = c.LocalNormalAt( new Point( -1, -1, 0 ) );
            ie = n.IsEqual( new Vector( -1, 1, 0 ) );
            Assert.AreEqual( ie, true );
        }
    }
}
