using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static RayTracer.Functions;

namespace RayTracer.Tests
{
    [TestClass]
    public class SphereTest
    {
        [TestMethod]
        public void TestSphereRayCentre()
        {
            Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            Sphere s = new Sphere();

            Intersections xs = new Intersections();
            s.Intersect(xs, r);

            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].Tick, 4);
            Assert.AreEqual(xs[1].Tick, 6);
        }

        [TestMethod]
        public void TestSphereRayTop()
        {
            Ray r = new Ray(new Point(0, 1, -5), new Vector(0, 0, 1));
            Sphere s = new Sphere();

            Intersections xs = new Intersections();
            s.Intersect(xs, r);

            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].Tick, 5);
            Assert.AreEqual(xs[1].Tick, 5);
        }

        [TestMethod]
        public void TestSphereRayMiss()
        {
            Ray r = new Ray(new Point(0, 2, -5), new Vector(0, 0, 1));
            Sphere s = new Sphere();

            Intersections xs = new Intersections();
            s.Intersect(xs, r);

            Assert.AreEqual(xs.Count, 0);
        }

        [TestMethod]
        public void TestSphereRayInside()
        {
            Ray r = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
            Sphere s = new Sphere();

            Intersections xs = new Intersections();
            s.Intersect(xs, r);

            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].Tick, -1);
            Assert.AreEqual(xs[1].Tick, 1);
        }

        [TestMethod]
        public void TestSphereBehindRay()
        {
            Ray r = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));
            Sphere s = new Sphere();

            Intersections xs = new Intersections();
            s.Intersect(xs, r);

            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].Tick, -6);
            Assert.AreEqual(xs[1].Tick, -4);
        }

        [TestMethod]
        public void TestIntersections()
        {
            Ray r = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));
            Sphere s = new Sphere();

            Intersections xs = new Intersections();
            s.Intersect(xs, r);

            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].Shape, s);
        }

        [TestMethod]
        public void TestDefaultTransform()
        {
            Shape s = new Sphere();

            Transformation t = new Transformation();

            bool isEqual = t.IsEqual(s.Transform);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestTransformTranslation()
        {
            Shape s = new Sphere();

            Transformation t = new Transformation();
            t.Translation(2,3,4);

            s.SetTransform(t);

            Assert.AreEqual(s.Transform, t);
        }

        [TestMethod]
        public void TestScalingIntersect()
        {
            Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            
            Shape s = new Sphere();
            Transformation t = new Transformation();
            t.Scaling(2, 2, 2);
            s.SetTransform(t);

            Intersections i = new Intersections();
            s.Intersect(i, r);

            Assert.AreEqual(i.Count, 2);
            Assert.AreEqual(i[0].Tick, 3);
            Assert.AreEqual(i[1].Tick, 7);
        }

        [TestMethod]
        public void TestTranslationIntersect()
        {
            Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            
            Shape s = new Sphere();
            Transformation t = new Transformation();
            t.Translation(5, 0, 0);
            s.SetTransform(t);

            Intersections i = new Intersections();
            s.Intersect(i, r);

            Assert.AreEqual(i.Count, 0);
        }

        [TestMethod]
        public void TestShereNormal1()
        {
            Shape s = new Sphere();

            Point p = new Point(1, 0, 0);

            Vector v = s.NormalAt(p);
            Vector va = new Vector(1, 0, 0);
            bool isEqual = IsEqual(v, va);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestShereNormal2()
        {
            Shape s = new Sphere();

            Point p = new Point(0, 1, 0);

            Vector v = s.NormalAt(p);
            Vector va = new Vector(0, 1, 0);
            bool isEqual = IsEqual(v, va);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestShereNormal3()
        {
            Shape s = new Sphere();

            Point p = new Point(0, 0, 1);

            Vector v = s.NormalAt(p);
            Vector va = new Vector(0, 0, 1);
            bool isEqual = IsEqual(v, va);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestShereNormal4()
        {
            Shape s = new Sphere();

            Point p = new Point(Math.Sqrt(3)/3, Math.Sqrt(3)/3, Math.Sqrt(3)/3);

            Vector v = s.NormalAt(p);
            Vector va = new Vector(Math.Sqrt(3)/3, Math.Sqrt(3)/3, Math.Sqrt(3)/3);
            bool isEqual = IsEqual(v, va);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestShereNormal5()
        {
            Shape s = new Sphere();
            Transformation t = new Transformation();
            t.Translation(0, 1, 0);
            s.Transform = t;

            Point p = new Point(0, 1.70711, -0.70711);

            Vector v = s.NormalAt(p);
            Vector va = new Vector(0, 0.70711, -0.70711);
            bool isEqual = IsEqual(v, va);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestShereNormal6()
        {
            Shape s = new Sphere();
            Transformation t1 = new Transformation();
            t1.Scaling(1, 0.5, 1);
            Transformation t2 = new Transformation();
            double d = Math.PI / 5;
            t2.RotationZ(d);
            Transformation t = t1 * t2;
            s.Transform = t;
            double d2 = Math.Sqrt(2) / 2;
            Point p = new Point(0, d2, -d2);

            Vector v = s.NormalAt(p);
            Vector va = new Vector(0, 0.97014, -0.24254);
            bool isEqual = IsEqual(v, va);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestMaterials()
        {
            Shape s = new Sphere();
            Material m = s.Material;

            Assert.AreEqual(m, s.Material);
        }

        [TestMethod]
        public void TestMaterials2()
        {
            Shape s = new Sphere();
            Material m = new Material(new Color(1,1,1),1);
            s.Material = m;

            Assert.AreEqual(m, s.Material);
            Assert.AreEqual(m.Ambient, 1);
            Assert.AreEqual(m, s.Material);
        }

        [TestMethod]
        public void GlassSphere()
        {
            Shape s = Sphere.GlassSphere();

            bool ie = s.Transform.TMatrix.IsEqual( Matrix.Identity( 4 ) );

            Assert.AreEqual( ie, true );
            Assert.AreEqual( s.Material.Transparency, 1 );
            Assert.AreEqual( s.Material.RefractiveIndex, 1.5 );
        }
    }
}
