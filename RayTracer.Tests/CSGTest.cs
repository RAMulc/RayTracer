using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayTracer.Tests
{
    [TestClass]
    public class CSGTest
    {
        [TestMethod]
        public void CSG()
        {
            Shape s1 = new Sphere();
            Shape s2 = new Cube();

            CSG csg = new CSG( CSGOperation.Union, s1, s2 );

            Assert.AreEqual( csg.Operation, CSGOperation.Union );
            Assert.AreEqual( csg.Left, s1 );
            Assert.AreEqual( csg.Right, s2 );
            Assert.AreEqual( s1.Parent, csg );
            Assert.AreEqual( s2.Parent, csg );
        }

        [TestMethod]
        public void Union()
        {
            Shape s1 = new Sphere();
            Shape s2 = new Cube();

            CSG csg = new CSG( CSGOperation.Union, s1, s2 );

            Assert.AreEqual( csg.IntersectionAllowed( true, true, true ), false );
            Assert.AreEqual( csg.IntersectionAllowed( true, true, false ), true );
            Assert.AreEqual( csg.IntersectionAllowed( true, false, true ), false );
            Assert.AreEqual( csg.IntersectionAllowed( true, false, false ), true );
            Assert.AreEqual( csg.IntersectionAllowed( false, true, true ), false );
            Assert.AreEqual( csg.IntersectionAllowed( false, true, false ), false );
            Assert.AreEqual( csg.IntersectionAllowed( false, false, true ), true );
            Assert.AreEqual( csg.IntersectionAllowed( false, false, false ), true );
        }

        [TestMethod]
        public void Intersect()
        {
            Shape s1 = new Sphere();
            Shape s2 = new Cube();

            CSG csg = new CSG( CSGOperation.Intersection, s1, s2 );

            Assert.AreEqual( csg.IntersectionAllowed( true, true, true ), true );
            Assert.AreEqual( csg.IntersectionAllowed( true, true, false ), false );
            Assert.AreEqual( csg.IntersectionAllowed( true, false, true ), true );
            Assert.AreEqual( csg.IntersectionAllowed( true, false, false ), false );
            Assert.AreEqual( csg.IntersectionAllowed( false, true, true ), true );
            Assert.AreEqual( csg.IntersectionAllowed( false, true, false ), true );
            Assert.AreEqual( csg.IntersectionAllowed( false, false, true ), false );
            Assert.AreEqual( csg.IntersectionAllowed( false, false, false ), false );
        }

        [TestMethod]
        public void Difference()
        {
            Shape s1 = new Sphere();
            Shape s2 = new Cube();

            CSG csg = new CSG( CSGOperation.Difference, s1, s2 );

            Assert.AreEqual( csg.IntersectionAllowed( true, true, true ), false );
            Assert.AreEqual( csg.IntersectionAllowed( true, true, false ), true );
            Assert.AreEqual( csg.IntersectionAllowed( true, false, true ), false );
            Assert.AreEqual( csg.IntersectionAllowed( true, false, false ), true );
            Assert.AreEqual( csg.IntersectionAllowed( false, true, true ), true );
            Assert.AreEqual( csg.IntersectionAllowed( false, true, false ), true );
            Assert.AreEqual( csg.IntersectionAllowed( false, false, true ), false );
            Assert.AreEqual( csg.IntersectionAllowed( false, false, false ), false );
        }

        [TestMethod]
        public void FilterIntersections()
        {
            Shape s1 = new Sphere();
            Shape s2 = new Cube();

            //CSG csg = new CSG( CSGOperation.Union, s1, s2 );
            //CSG csg = new CSG( CSGOperation.Intersection, s1, s2 );
            CSG csg = new CSG( CSGOperation.Difference, s1, s2 );

            Intersection i1 = new Intersection( 1, s1 );
            Intersection i2 = new Intersection( 2, s2 );
            Intersection i3 = new Intersection( 3, s1 );
            Intersection i4 = new Intersection( 4, s2 );

            Intersections xs = new Intersections();
            xs.Add( i1 );
            xs.Add( i2 );
            xs.Add( i3 );
            xs.Add( i4 );

            Intersections results = csg.FilterIntersections( xs );

            Assert.AreEqual( results.IntersectionList.Count, 2 );
            Assert.AreEqual( results[0], xs[0] );
            Assert.AreEqual( results[1], xs[1] );
        }

        [TestMethod]
        public void TestCSGImage()
        {
            Transformation t1 = new Transformation();
            t1.Translation( 0, -1, 0 );
            Transformation t2 = new Transformation();
            t1.RotationY( Math.PI / 7 );

            Shape floor = new Plane();
            floor.SetTransform( t1 * t2 );
            floor.Material.Transparency = 1;
            floor.Material.Reflective = 1;
            floor.Material.RefractiveIndex = 1.5;
            floor.CastShadow = false;
            floor.Material.Pattern = new CheckerPattern( new Color( 1, 1, 1 ), new Color( 0.1, 0.1, 0.1 ), false );

            Transformation t7 = new Transformation();
            Transformation t8 = new Transformation();
            t8.Scaling( 0.33, 0.33, 0.33 );


            Shape s1 = new Cube();
            s1.SetTransform( t1 );
            s1.Material.MatColor = new Color( 0, 1, 0 );
            Shape s2 = new Cube();
            s1.Material.MatColor = new Color( 1, 0, 0 );

            CSG csg = new CSG( CSGOperation.Union, s1, s2 );


            List<Shape> shapes = new List<Shape>
            {
                floor,
                csg
            };

            Light light = new Light( new Point( 0, 10, 10 ), new Color( 1, 1, 1 ) );

            World world = new World( shapes, light );

            Point from = new Point( 0, 4, 10 );
            Point to = new Point( 2, 1, 0 );
            Vector up = new Vector( 0, 1, 0 );

            Camera camera = new Camera( 250, 150, Math.PI / 3 )

            {
                Transform = Transformation.ViewTransform( from, to, up )
            };

            Canvas canvas = camera.Render( world, 3 );
            canvas.WritePpm();
        }
    }
}
