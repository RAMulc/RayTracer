using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayTracer.Tests
{
    [TestClass]
    public class ShapeTest
    {
        [TestMethod]
        public void DefaulTransform()
        {
            Shape s = new TestShape();
            Transformation t = new Transformation(Matrix.Identity(4));

            bool ie = t.IsEqual(s.Transform);

            Assert.AreEqual(ie, true);
        }

        [TestMethod]
        public void ShapeTransformation()
        {
            Shape s = new TestShape();
            s.Transform.Translation(2, 3, 4);

            Transformation t = new Transformation();
            t.Translation(2, 3, 4);

            bool ie = t.IsEqual(s.Transform);

            Assert.AreEqual(ie, true);
        }

        [TestMethod]
        public void ShapeMaterial()
        {
            Shape s = new TestShape();
            Material m = new Material();
            s.Material = m;

            Assert.AreEqual(m, s.Material);
        }

        [TestMethod]
        public void ShapeMaterial2()
        {
            Shape s = new TestShape();
            Material m = new Material
            {
                Ambient = 1
            };

            s.Material = m;

            Assert.AreEqual(1, s.Material.Ambient);
        }

        [TestMethod]
        public void TestScalingIntersect()
        {
            Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            
            Shape s = new TestShape();
            Transformation t = new Transformation();
            t.Scaling(2, 2, 2);
            s.SetTransform(t);

            Intersections xs = new Intersections();
            s.Intersect(xs, r);

            s.SavedRay.Origin = new Point(0, 0, -2.5);
            s.SavedRay.Direction = new Vector(0, 0, 0.5);

            bool ie1 = s.SavedRay.Origin.IsEqual(new Point(0, 0, -2.5));
            bool ie2 = s.SavedRay.Direction.IsEqual(new Vector(0, 0, 0.5));

            Assert.AreEqual(ie1, true);
            Assert.AreEqual(ie2, true);
        }

        [TestMethod]
        public void TestTranslationIntersect()
        {
            Ray r = new Ray(new Point(0, 0, -5), new Vector(0, 0, 1));
            
            Shape s = new TestShape();
            Transformation t = new Transformation();
            t.Translation(5, 0, 0);
            s.SetTransform(t);

            Intersections i = new Intersections();
            s.Intersect(i, r);

            Assert.AreEqual(i.Count, 0);
        }

        [TestMethod]
        public void NormalAtTranslatedShape()
        {
            Shape s = new TestShape();
            s.Transform.Translation( 0, 1, 0 );
            Vector n = s.NormalAt( new Point( 0, 1.70711, -0.70711 ) );
            bool ie = n.IsEqual( new Vector( 0, 0.70711, -0.70711 ) );

            Assert.AreEqual( true, ie );
        }

        [TestMethod]
        public void NormalAtTransformedShape()
        {
            Transformation scaling = new Transformation();
            scaling.Scaling( 1, 0.5, 1 );
            Transformation rotation = new Transformation();
            rotation.RotationZ( Math.PI / 5 );

            Shape s = new TestShape();
            s.Transform = scaling*rotation;

            Vector n = s.NormalAt( new Point( 0, Math.Sqrt(2)/2, -Math.Sqrt(2)/2 ) );
            bool ie = n.IsEqual( new Vector( 0, 0.97014, -0.24254 ) );

            Assert.AreEqual( true, ie );
        }

        [TestMethod]
        public void ParentAttrib()
        {
            Shape s = new TestShape();
            Group t = s.Parent;

            bool ie = t == null;

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void ConvertPointFromWorldToObjectSpace()
        {
            Group g1 = new Group();
            g1.Transform.RotationY( Math.PI / 2 );
            Group g2 = new Group();
            g2.Transform.Scaling( 2, 2, 2 );
            g1.AddChild( g2 );
            Sphere s = new Sphere();
            s.Transform.Translation( 5, 0, 0 );
            g2.AddChild( s );

            Point p = Shape.WorldToObject( s, new Point( -2, 0, -10 ) );

            bool ie = p.IsEqual( new Point( 0, 0, -1 ) );

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void ConvertNormalFromObjectToWorldSpace()
        {
            Group g1 = new Group();
            g1.Transform.RotationY( Math.PI / 2 );

            Group g2 = new Group();
            g2.Transform.Scaling( 1, 2, 3 );
            g1.AddChild( g2 );

            Sphere s = new Sphere();
            s.Transform.Translation( 5, 0, 0 );
            g2.AddChild( s );

            Vector n = Shape.NormalToWorld( s, new Vector( Math.Sqrt( 3 ) / 3, Math.Sqrt( 3 ) / 3, Math.Sqrt( 3 ) / 3 ) );

            bool ie = n.IsEqual( new Vector( 0.2857142857142857, 0.4285714285714286, -0.85714285714285721 ) );

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void FindNormalOnChildObject()
        {
            Group g1 = new Group();
            g1.Transform.RotationY( Math.PI / 2 );

            Group g2 = new Group();
            g2.Transform.Scaling( 1, 2, 3 );
            g1.AddChild( g2 );

            Sphere s = new Sphere();
            s.Transform.Translation( 5, 0, 0 );
            g2.AddChild( s );

            Vector n = s.NormalAt( new Point( 1.7321, 1.1547, -5.5774 ));

            bool ie = n.IsEqual( new Vector( 0.28570368184140726, 0.42854315178114105, -0.85716052944810173 ) );

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void BoundingBoxInParentSpace()
        {
            Shape s = new Sphere();
            Transformation t1 = new Transformation(TransformationType.Translation, "1,-3,5" );
            Transformation t2 = new Transformation( TransformationType.Scaling, "0.5,2,4" );
            s.SetTransform( t1 * t2 );

            BoundingBox b = s.ParentSpaceBoundsOf();

            bool ie = b.Min.IsEqual( new Point( 0.5, -5, 1 ) );
            Assert.AreEqual( ie, true );
            ie = b.Max.IsEqual( new Point( 1.5, -1, 9 ) );
            Assert.AreEqual( ie, true );
        }
    }
}
