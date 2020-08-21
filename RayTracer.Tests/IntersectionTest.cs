using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using F = RayTracer.Functions;

namespace RayTracer.Tests
{
    [TestClass]
    public class IntersectionTest
    {
        [TestMethod]
        public void TestIntersection()
        {
            Shape s = new Sphere();
            Intersection i = new Intersection(3.5, s);

            Assert.AreEqual(i.Tick, 3.5);
            Assert.AreEqual(i.Shape, s);
        }

        [TestMethod]
        public void TestAggregation()
        {
            Shape s = new Sphere();
            Intersections xs = new Intersections();

            xs.Add(new Intersection(1, s));
            xs.Add(new Intersection(2, s));

            Assert.AreEqual(xs.Count, 2);

            Assert.AreEqual(xs[0].Tick, 1);
            Assert.AreEqual(xs[1].Tick, 2);
        }

        [TestMethod]
        public void TestHit1()
        {
            Shape s = new Sphere();
            Intersections xs = new Intersections();

            Intersection i1 = new Intersection(1, s);
            Intersection i2 = new Intersection(2, s);

            xs.Add(i1);
            xs.Add(i2);

            Assert.AreEqual(xs.Hit(), i1);
        }

        [TestMethod]
        public void TestHit2()
        {
            Shape s = new Sphere();
            Intersections xs = new Intersections();

            Intersection i1 = new Intersection(-1, s);
            Intersection i2 = new Intersection(1, s);

            xs.Add(i1);
            xs.Add(i2);

            Assert.AreEqual(xs.Hit(), i2);
        }

        [TestMethod]
        public void TestHit3()
        {
            Shape s = new Sphere();
            Intersections xs = new Intersections();

            Intersection i1 = new Intersection(-2, s);
            Intersection i2 = new Intersection(-1, s);

            xs.Add(i1);
            xs.Add(i2);

            Assert.AreEqual(xs.Hit(), null);
        }

        [TestMethod]
        public void TestHit4()
        {
            Shape s = new Sphere();
            Intersections xs = new Intersections();

            Intersection i1 = new Intersection(5, s);
            Intersection i2 = new Intersection(7, s);
            Intersection i3 = new Intersection(-3, s);
            Intersection i4 = new Intersection(2, s);

            xs.Add(i1);
            xs.Add(i2);
            xs.Add(i3);
            xs.Add(i4);

            Assert.AreEqual(xs.Hit(), i4);
        }

        [TestMethod]
        public void TestPrecomputations()
        {
            Ray r = new Ray(new Point(0,0,-5), new Vector(0,0,1));
            Shape s = new Sphere();
            Intersection i = new Intersection(4, s);

            Computations c = new Computations(i, r);

            Assert.AreEqual(c.Tick, i.Tick);
            Assert.AreSame(c.Shape, i.Shape);
            bool ie1 = c.Position.IsEqual(new Point(0, 0, -1));
            Assert.AreEqual(ie1, true);
            bool ie2 = c.EyeVector.IsEqual(new Vector(0, 0, -1));
            Assert.AreEqual(ie2, true);
            bool ie3 = c.NormalVector.IsEqual(new Vector(0, 0, -1));
            Assert.AreEqual(ie3, true);
        }

        [TestMethod]
        public void TestPrecomputationsNotInside()
        {
            Ray r = new Ray(new Point(0,0,-5), new Vector(0,0,1));
            Shape s = new Sphere();
            Intersection i = new Intersection(4, s);

            Computations c = new Computations(i, r);

            Assert.AreEqual(c.Inside, false);
        }

        [TestMethod]
        public void TestPrecomputationsInside()
        {
            Ray r = new Ray(new Point(0,0,0), new Vector(0,0,1));
            Shape s = new Sphere();
            Intersection i = new Intersection(1, s);

            Computations c = new Computations(i, r);

            bool ie1 = c.Position.IsEqual(new Point(0, 0, 1));
            Assert.AreEqual(ie1, true);

            bool ie2 = c.EyeVector.IsEqual(new Vector(0, 0, -1));
            Assert.AreEqual(ie2, true);

            Assert.AreEqual(c.Inside, true);

            bool ie3 = c.NormalVector.IsEqual(new Vector(0, 0, -1));
            Assert.AreEqual(ie3, true);
        }

        [TestMethod]
        public void TestOffsetPoint()
        {
            Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            Shape s1 = new Sphere();
            s1.Transform.Translation(0, 0, 1);
            Intersection i = new Intersection(5, s1);
            Computations comps = new Computations(i, r);
            bool compare = comps.OverPoint().Z < -F.Epsilon/2;
            bool compare2 = comps.Position.Z > comps.OverPoint().Z;

            Assert.AreEqual(compare, true);
            Assert.AreEqual(compare2, true);
        }

        [TestMethod]
        public void TestReflection()
        {
            Shape s = new Plane();
            Ray r = new Ray( new Point( 0, 1, -1 ), new Vector( 0, -Math.Sqrt( 2 ) / 2, Math.Sqrt( 2 ) / 2 ) );
            Intersection i = new Intersection( Math.Sqrt( 2 ), s );
            Computations c = new Computations( i, r );

            Vector v = c.ReflectV;

            bool ie = v.IsEqual(new Vector( 0, Math.Sqrt( 2 ) / 2, Math.Sqrt( 2 ) / 2 ) );

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void Findn1n2()
        {
            Shape A = Sphere.GlassSphere();
            A.Transform.Scaling( 2, 2, 2 );
            A.Material.RefractiveIndex = 1.5;

            Shape B = Sphere.GlassSphere();
            B.Transform.Translation( 0, 0, -0.25 );
            B.Material.RefractiveIndex = 2.0;

            Shape C = Sphere.GlassSphere();
            C.Transform.Translation( 0, 0, 0.25 );
            C.Material.RefractiveIndex = 2.5;

            Ray r = new Ray( new Point( 0, 0, 0 ), new Vector( 0, 0, 1 ) );
            Intersections xs = new Intersections();
            Intersection a = new Intersection( 2, A );
            xs.Add(a);
            Intersection b = new Intersection( 2.75, B );
            xs.Add(b);
            Intersection c = new Intersection( 3.25, C );
            xs.Add(c);
            Intersection d = new Intersection( 4.75, B );
            xs.Add(d);
            Intersection e = new Intersection( 5.25, C );
            xs.Add(e);
            Intersection f = new Intersection( 6, A );
            xs.Add( f );

            Computations comps0 = xs.PrepareComputations( xs[0], r, xs );
            Assert.AreEqual( comps0.N1, 1 );
            Assert.AreEqual( comps0.N2, 1.5 );

            Computations comps1 = xs.PrepareComputations( xs[1], r, xs );
            Assert.AreEqual( comps1.N1, 1.5 );
            Assert.AreEqual( comps1.N2, 2 );

            Computations comps2 = xs.PrepareComputations( xs[2], r, xs );
            Assert.AreEqual( comps2.N1, 2 );
            Assert.AreEqual( comps2.N2, 2.5 );

            Computations comps3 = xs.PrepareComputations( xs[3], r, xs );
            Assert.AreEqual( comps3.N1, 2.5 );
            Assert.AreEqual( comps3.N2, 2.5 );

            Computations comps4 = xs.PrepareComputations( xs[4], r, xs );
            Assert.AreEqual( comps4.N1, 2.5 );
            Assert.AreEqual( comps4.N2, 1.5 );

            Computations comps5 = xs.PrepareComputations( xs[5], r, xs );
            Assert.AreEqual( comps5.N1, 1.5 );
            Assert.AreEqual( comps5.N2, 1 );

        }

        [TestMethod]
        public void UnderPoint()
        {
            Ray r = new Ray( new Point( 0, 0, -5 ), new Vector( 0, 0, 1 ) );
            Shape s = Sphere.GlassSphere();
            s.Transform.Translation( 0, 0, 1 );
            Intersection i = new Intersection( 5, s );
            Intersections xs = new Intersections();
            xs.Add( i );
            Computations comps = xs.PrepareComputations( i, r, xs );

            bool testA = comps.UnderPoint().Z > Functions.Epsilon / 2;
            bool testB = comps.Position.Z < comps.UnderPoint().Z;

            Assert.AreEqual( testA, true );
            Assert.AreEqual( testB, true );
        }

        [TestMethod]
        public void SchlickApprox()
        {
            Shape s = Sphere.GlassSphere();
            Ray r = new Ray( new Point( 0, 0, Math.Sqrt( 2 ) / 2 ), new Vector( 0, 1, 0 ) );
            Intersection i1 = new Intersection( -Math.Sqrt( 2 ) / 2, s );
            Intersection i2 = new Intersection( Math.Sqrt( 2 ) / 2, s );
            Intersections xs = new Intersections();
            xs.Add( i1 );
            xs.Add( i2 );
            Computations comp = xs.PrepareComputations( xs[1], r, xs );
            double reflectance = Intersections.Schlick( comp );

            Assert.AreEqual( reflectance, 1 );
        }

        [TestMethod]
        public void SchlickApproxPerp()
        {
            Shape s = Sphere.GlassSphere();
            Ray r = new Ray( new Point( 0, 0, 0 ), new Vector( 0, 1, 0 ) );
            Intersection i1 = new Intersection( -1, s );
            Intersection i2 = new Intersection( 1, s );
            Intersections xs = new Intersections();
            xs.Add( i1 );
            xs.Add( i2 );
            Computations comp = xs.PrepareComputations( xs[1], r, xs );
            double reflectance = Intersections.Schlick( comp );

            bool ie = false;
            if ( Math.Abs( reflectance - 0.040000000000000008 ) < F.Epsilon )
                ie = true;

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void SchlickApproxSmallAngle()
        {
            Shape s = Sphere.GlassSphere();
            Ray r = new Ray( new Point( 0, 0.99, -2 ), new Vector( 0, 0, 1 ) );
            Intersection i0 = new Intersection( 1.8589, s );
            Intersections xs = new Intersections();
            xs.Add( i0 );
            Computations comp = xs.PrepareComputations( xs[0], r, xs );
            double reflectance = Intersections.Schlick( comp );

            bool ie = false;
            if ( Math.Abs( reflectance - 0.48873081012212183) < F.Epsilon )
                ie = true;

            Assert.AreEqual( ie, true );
        }
    }

}
