using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayTracer.Tests
{
    [TestClass]
    public class WorldTest
    {
        [TestMethod]
        public void TestWorld()
        {
            World world = new World();

            Light light = new Light(new Point(-10, 10, -10), new Color(1, 1, 1));
            Shape s1 = new Sphere();
            s1.Material.MatColor = new Color(0.8,1.0,0.6);
            s1.Material.Diffuse = 0.7;
            s1.Material.Specular = 0.2;
            Shape s2 = new Sphere();
            s2.Transform.Scaling(0.5, 0.5, 0.5);

            bool e1 = world.Light.Equals(light);
            bool e2 = s1.Equals(world.Shapes[0]);
            bool e3 = s1.Equals(world.Shapes[1]);

            //Assert.AreEqual(e1,true);
            //Assert.AreEqual(e2,true);
            //Assert.AreEqual(e3,true);
        }

        [TestMethod]
        public void TestWorldIntersection()
        {
            World w = new World();
            Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            
            Intersections xs = w.IntersectWorld(new Intersections(), r);

            double c = xs.Count;
            double i1 = xs.IntersectionList[0].Tick;
            double i2 = xs.IntersectionList[1].Tick;
            double i3 = xs.IntersectionList[2].Tick;
            double i4 = xs.IntersectionList[3].Tick;

            Assert.AreEqual(c, 4);
            Assert.AreEqual(i1, 4);
            Assert.AreEqual(i2, 4.5);
            Assert.AreEqual(i3, 5.5);
            Assert.AreEqual(i4, 6);
        }

        [TestMethod]
        public void TestShadingIntersection()
        {
            World w = new World();
            Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            Shape s1 = w.Shapes[0];
            Intersection i = new Intersection(4, s1);

            Computations comps = new Computations(i, r);
            Lighting l = w.ShadeHit(comps);
            Color c = l.Result;

            bool ie = c.IsEqual(new Color(0.38066, 0.47583, 0.2855));
        }

        [TestMethod]
        public void TestShadingIntersectionInside()
        {
            World w = new World
            {
                Light = new Light(new Point(0, 0.25, 0), new Color(1, 1, 1))
            };
            Ray r = new Ray(new Point(0, 0, 0), new Vector(0, 0, 1));
            Shape s2 = w.Shapes[1];
            Intersection i = new Intersection(0.5, s2);

            Computations comps = new Computations(i, r);
            Color c = w.ShadeHit(comps).Result;

            bool ie = c.IsEqual(new Color(0.90498, 0.90498, 0.90498));
        }

        [TestMethod]
        public void TestRayMiss()
        {
            World w = new World();
            Ray r = new Ray(new Point(0,0,-5), new Vector(0,1,0));

            Color c = w.ColorAt(r, 1);

            bool ie = c.IsEqual(new Color(0,0,0));

            Assert.AreEqual(ie, true);
        }

        [TestMethod]
        public void TestRayHit()
        {
            World w = new World();

            Ray r = new Ray(new Point(0,0,-5), new Vector(0,0,1));

            Color c = w.ColorAt(r, 1);

            bool ie = c.IsEqual(new Color(0.38066, 0.47583, 0.2855));

            Assert.AreEqual(ie, true);
        }

        [TestMethod]
        public void TestRayHitInner()
        {
            World w = new World();

            w.Shapes[0].Material.Ambient = 1;
            w.Shapes[1].Material.Ambient = 1;

            Ray r = new Ray(new Point(0,0,0.75), new Vector(0,0,-1));

            Color c = w.ColorAt(r, 1);

            bool ie = c.IsEqual(w.Shapes[1].Material.MatColor);

            Assert.AreEqual(ie, true);
        }

        [TestMethod]
        public void TestInShadow1()
        {
            World w = new World();
            Point p = new Point(0, 10, 0);

            bool isShadowed = w.IsShadowed(p);

            Assert.AreEqual(isShadowed, false);
        }

        [TestMethod]
        public void TestInShadow2()
        {
            World w = new World();
            Point p = new Point(10, -10, 10);

            bool isShadowed = w.IsShadowed(p);

            Assert.AreEqual(isShadowed, true);
        }

        [TestMethod]
        public void TestInShadow3()
        {
            World w = new World();
            Point p = new Point(-20, 20, -20);

            bool isShadowed = w.IsShadowed(p);

            Assert.AreEqual(isShadowed, false);
        }

        [TestMethod]
        public void TestInShadow4()
        {
            World w = new World();
            Point p = new Point(-2, 2, -2);

            bool isShadowed = w.IsShadowed(p);

            Assert.AreEqual(isShadowed, false);
        }

        [TestMethod]
        public void TestIntersectionInShadow()
        {
            World w = new World
            {
                Light = new Light(new Point(0, 0, -10), new Color(1, 1, 1))
            };
            Shape s1 = new Sphere();
            w.Shapes.Add(s1);
            Shape s2 = new Sphere();
            s2.Transform.Translation(0, 0, 10);
            w.Shapes.Add(s2);
            Ray r = new Ray(new Point(0, 0, 5), new Vector(0, 0, 1));
            Intersection i = new Intersection(4, s2);
            Computations comps = new Computations(i, r);
            Lighting c = w.ShadeHit(comps);
            Color col = c.Result;

            bool ie = col.IsEqual(new Color(0.1, 0.1, 0.1));

            Assert.AreEqual(ie, true);
        }

        [TestMethod]
        public void TestReflectedColor()
        {
            World w = new World();
            Ray r = new Ray( new Point( 0, 0, 0 ), new Vector( 0, 0, 1 ) );
            Shape s = w.Shapes[1];
            s.Material.Ambient = 1;
            Intersection i = new Intersection( 1, s );
            Intersections xs = new Intersections();
            xs.Add( i );
            Computations c = xs.PrepareComputations( i, r, xs );
            Color color = w.ReflectedColor( c, 1 );

            bool ie = color.IsEqual( new Color( 0,0,0 ) );

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void TestReflectedColor2()
        {
            World w = new World();

            Shape s = new Plane();
            s.Material.Reflective = 0.5;
            s.Transform.Translation( 0, -1, 0 );
            w.Shapes.Add( s );

            Ray r = new Ray( new Point( 0, 0, -3 ), new Vector( 0, -Math.Sqrt(2)/2, Math.Sqrt( 2 ) / 2 ) );

            Intersection i = new Intersection( Math.Sqrt(2), s );
            Intersections xs = new Intersections();
            xs.Add( i );
            Computations c = xs.PrepareComputations( i, r, xs );
            Color color = w.ReflectedColor( c, 1 );

            bool ie = color.IsEqual( new Color( 0.19033220149513302, 0.23791525186891627, 0.14274915112134973 ) );

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void TestShadeHitReflectiveMaterial()
        {
            World w = new World();
            Shape s = new Plane();
            s.Material.Reflective = 0.5;
            s.Transform.Translation( 0, -1, 0 );
            w.Shapes.Add( s );

            Ray r = new Ray( new Point( 0, 0, -3 ), new Vector( 0, -Math.Sqrt( 2 ) / 2, Math.Sqrt( 2 ) / 2 ) );

            Intersection i = new Intersection( Math.Sqrt( 2 ), s );
            Intersections xs = new Intersections();
            xs.Add( i );
            Computations c = xs.PrepareComputations( i, r, xs );
            Color color = w.ShadeHit( c ).Result;
            bool ie = color.IsEqual( new Color( 0.87675728370209072, 0.924340334075874, 0.82917423332830742 ) );

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void ColorAtMutuallyReflectiveSurfaces()
        {

            List<Shape> shapes = new List<Shape>();
            Shape lower = new Plane();
            lower.Material.Reflective = 1;
            lower.Transform.Translation( 0, -1, 0 );
            shapes.Add( lower );
            Shape upper = new Plane();
            upper.Material.Reflective = 1;
            upper.Transform.Translation( 0, 1, 0 );
            shapes.Add( upper );

            World w = new World( shapes, new Light( new Point( 0, 0, 0 ), new Color( 1, 1, 1 ) ) );

            Ray r = new Ray( new Point( 0, 0, 0 ), new Vector( 0, 1, 0 ) );

            Color c = w.ColorAt( r, 1 );
        }

        [TestMethod]
        public void TestShadeHitReflectiveMaterial2()
        {
            World w = new World();
            Shape s = new Plane();
            s.Material.Reflective = 0.5;
            s.Transform.Translation( 0, -1, 0 );
            w.Shapes.Add( s );

            Ray r = new Ray( new Point( 0, 0, -3 ), new Vector( 0, -Math.Sqrt( 2 ) / 2, Math.Sqrt( 2 ) / 2 ) );

            Intersection i = new Intersection( Math.Sqrt( 2 ), s );
            Intersections xs = new Intersections();
            xs.Add( i );
            Computations c = xs.PrepareComputations( i, r, xs );
            Color color = w.ShadeHit( c ).Result;
            bool ie = color.IsEqual( new Color( 0.87675728370209072, 0.924340334075874, 0.82917423332830742 ) );

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void RefractedColor()
        {
            World w = new World();
            Shape s = w.Shapes[0];
            Ray r = new Ray( new Point( 0, 0, -5 ), new Vector( 0, 0, 1 ) );
            Intersection i1 = new Intersection( 4, s );
            Intersection i2 = new Intersection( 6, s );
            Intersections xs = new Intersections();
            xs.Add( i1 );
            xs.Add( i2 );
            Computations comps = xs.PrepareComputations( xs[0], r, xs );
            Color c = w.RefractedColor( comps, 5 );

            bool ie = c.IsEqual( new Color( 0, 0, 0 ) );

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void RefractedColorRemaining0()
        {
            World w = new World();
            Shape s = w.Shapes[0];
            s.Material.Transparency = 1;
            s.Material.RefractiveIndex = 1.5;

            Ray r = new Ray( new Point( 0, 0, -5 ), new Vector( 0, 0, 1 ) );
            Intersection i1 = new Intersection( 4, s );
            Intersection i2 = new Intersection( 6, s );
            Intersections xs = new Intersections();
            xs.Add( i1 );
            xs.Add( i2 );
            Computations comps = xs.PrepareComputations( xs[0], r, xs );
            Color c = w.RefractedColor( comps, 0 );

            bool ie = c.IsEqual( new Color( 0, 0, 0 ) );

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void TotalInternalReflection()
        {
            World w = new World();
            Shape s = w.Shapes[0];
            s.Material.Transparency = 1;
            s.Material.RefractiveIndex = 1.5;

            Ray r = new Ray( new Point( 0, 0, Math.Sqrt(2)/2 ), new Vector( 0, 1, 0 ) );
            Intersection i1 = new Intersection( -Math.Sqrt( 2 ) / 2, s );
            Intersection i2 = new Intersection( Math.Sqrt( 2 ) / 2, s );
            Intersections xs = new Intersections();
            xs.Add( i1 );
            xs.Add( i2 );
            Computations comps = xs.PrepareComputations( xs[1], r, xs );
            Color c = w.RefractedColor( comps, 5 );

            bool ie = c.IsEqual( new Color( 0, 0, 0 ) );

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void TestRefractedColor()
        {
            World w = new World();

            Shape A = w.Shapes[0];
            A.Material.Ambient = 1;
            A.Material.Pattern = new TestPattern( false );

            Shape B = w.Shapes[1];
            B.Material.Transparency = 1;
            B.Material.RefractiveIndex = 1.5;

            Ray r = new Ray( new Point( 0, 0, 0.1 ), new Vector( 0, 1, 0 ) );
            Intersection iA = new Intersection( -0.9899, A );
            Intersection iB = new Intersection( -0.4899, B );
            Intersection iC = new Intersection( 0.4899, B );
            Intersection iD = new Intersection( 0.9899, A );
            Intersections xs = new Intersections();
            xs.Add( iA );
            xs.Add( iB );
            xs.Add( iC );
            xs.Add( iD );

            Computations comps = xs.PrepareComputations( xs[2], r, xs );

            Color c = w.RefractedColor( comps, 5 );

            bool ie = c.IsEqual( new Color( 0, 0.998874550679558228, 0.04721898034382347 ) );

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void ShadeHitWithTransparentMaterial()
        {
            World w = new World();

            Shape floor = new Plane();
            floor.Transform.Translation( 0, -1, 0 );
            floor.Material.Transparency = 0.5;
            floor.Material.RefractiveIndex = 1.5;
            w.Shapes.Add( floor );

            Shape ball = new Sphere();
            ball.Material.MatColor = new Color( 1, 0, 0 );
            ball.Material.Ambient = 0.5;
            ball.Transform.Translation( 0, -3.5, -0.5 );
            w.Shapes.Add( ball );

            Ray r = new Ray( new Point( 0, 0, -3 ), new Vector( 0, -Math.Sqrt( 2 ) / 2, Math.Sqrt( 2 ) / 2 ) );
            Intersection i = new Intersection( Math.Sqrt( 2 ), floor );
            Intersections xs = new Intersections();
            xs.Add( i );

            Computations comps = xs.PrepareComputations( xs[0], r, xs );
            Color c = w.ShadeHit( comps, 5 ).Result;

            bool ie = c.IsEqual( new Color( 0.93642, 0.68642, 0.68642 ) );

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void ShadeHitRelectiveTransparentMaterial()
        {
            World w = new World();
            Shape floor = new Plane();
            floor.Transform.Translation( 0, -1, 0 );
            floor.Material.Transparency = 0.5;
            floor.Material.RefractiveIndex = 1.5;
            w.Shapes.Add( floor );

            Shape ball = new Sphere();
            ball.Material.MatColor = new Color( 1, 0, 0 );
            ball.Material.Ambient = 0.5;
            ball.Transform.Translation( 0, -3.5, -0.5 );
            w.Shapes.Add( ball );

            Ray r = new Ray( new Point( 0, 0, -3 ), new Vector( 0, -Math.Sqrt( 2 ) / 2, Math.Sqrt( 2 ) / 2 ) );

            Intersection i = new Intersection( Math.Sqrt( 2 ), floor );
            Intersections xs = new Intersections();
            xs.Add( i );

            Computations comps = xs.PrepareComputations( xs[0], r, xs );
            Color c = w.ShadeHit( comps, 5 ).Result;

            bool ie = c.IsEqual( new Color( 0.93391, 0.69643, 0.69243 ) );
            ie = c.IsEqual( new Color( 0.93642508220695775, 0.68642508220695775, 0.68642508220695775 ) );

            Assert.AreEqual( ie, true );
        }
    }
}
