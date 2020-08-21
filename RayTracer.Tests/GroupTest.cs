using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayTracer.Tests
{
    [TestClass]
    public class GroupTest
    {
        [TestMethod]
        public void CreateGroup()
        {
            Shape g = new Group();
            Matrix m = g.Transform.TMatrix;

            bool ie = m.IsEqual( Matrix.Identity( 4 ) );

            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void AddingChild()
        {
            Group g = new Group();
            Shape s = new TestShape();
            g.AddChild( s );

            bool ie = g.Children[0] == s;
            Assert.AreEqual( ie, true );

            ie = s.Parent == g;
            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void IntersectEmptyGroup()
        {
            Group g = new Group();
            Ray r = new Ray( new Point( 0, 0, 0 ), new Vector( 0, 0, 1 ) );
            Intersections xs = new Intersections();
            xs = g.LocalIntersect( xs, r );

            Assert.AreEqual( xs.Count, 0 );
        }

        [TestMethod]
        public void IntersectNonEmptyGroup()
        {
            Group g = new Group();
            Shape s1 = new Sphere();
            Shape s2 = new Sphere();
            s2.Transform.Translation( 0, 0, -3 );
            Shape s3 = new Sphere();
            s3.Transform.Translation( 5, 0, 0 );
            g.AddChild( s1 );
            g.AddChild( s2 );
            g.AddChild( s3 );

            Ray r = new Ray( new Point( 0, 0, -5 ), new Vector( 0, 0, 1 ) );
            Intersections xs = new Intersections();
            xs = g.LocalIntersect( xs, r );

            Assert.AreEqual( xs.Count, 4 );
            bool ie = xs[0].Shape == s2;
            Assert.AreEqual( ie, true );
            ie = xs[1].Shape == s2;
            Assert.AreEqual( ie, true );
            ie = xs[2].Shape == s1;
            Assert.AreEqual( ie, true );
            ie = xs[3].Shape == s1;
            Assert.AreEqual( ie, true );

        }

        [TestMethod]
        public void IntersectTransformedGroup()
        {
            Group g = new Group();
            g.Transform.Scaling( 2, 2, 2 );
            Shape s = new Sphere();
            s.Transform.Translation( 5, 0, 0 );
            g.AddChild( s );

            Ray r = new Ray( new Point( 10, 0, -10 ), new Vector( 0, 0, 1 ) );
            Intersections xs = new Intersections();
            xs = g.Intersect( xs, r );

            Assert.AreEqual( xs.Count, 2 );
        }

        [TestMethod]
        public void GroupBoundingBox()
        {
            Shape s = new Sphere();
            Transformation t1 = new Transformation( TransformationType.Translation, "2,5,-3" );
            Transformation t2 = new Transformation( TransformationType.Scaling, "2,2,2" );
            s.SetTransform( t1 * t2 );

            Shape c = new Cylinder( -2, 2 );
            t1 = new Transformation( TransformationType.Translation, "-4,-1,4" );
            t2 = new Transformation( TransformationType.Scaling, "0.5,1,0.5" );
            c.SetTransform( t1 * t2 );

            Group g = new Group();
            g.AddChild( s );
            g.AddChild( c );

            BoundingBox b = g.BoundsOf();

            bool ie = b.Min.IsEqual( new Point( -4.5, -3, -5 ) );
            Assert.AreEqual( ie, true );
            ie = b.Max.IsEqual( new Point( 4, 7, 4.5 ) );
            Assert.AreEqual( ie, true );

        }

        [TestMethod]
        public void TestRayMiss()
        {
            Shape t = new TestShape();
            Group g = new Group();
            g.AddChild( t );
            Ray r = new Ray( new Point( 0, 0, -5 ), new Vector( 0, 1, 0 ) );
            Intersections xs = new Intersections();
            xs = g.Intersect( xs, r );
            bool isNull = false;
            if ( t.SavedRay == null )
                isNull = true;
            Assert.AreEqual( isNull, true );
        }

        [TestMethod]
        public void TestRayHit()
        {
            Shape t = new TestShape();
            Group g = new Group();
            g.AddChild( t );
            Ray r = new Ray( new Point( 0, 0, -5 ), new Vector( 0, 0, 1 ) );
            Intersections xs = new Intersections();
            xs = g.Intersect( xs, r );
            bool isNull = false;
            if ( t.SavedRay == null )
                isNull = true;
            Assert.AreEqual( isNull, false );
        }

        [TestMethod]
        public void PartitionChildren()
        {
            Shape s1 = new Sphere();
            s1.Transform.Translation( -2, 0, 0 );
            Shape s2 = new Sphere();
            s2.Transform.Translation( 2, 0, 0 );
            Shape s3 = new Sphere();

            Group g = new Group();
            g.AddChild( s1 );
            g.AddChild( s2 );
            g.AddChild( s3 );

            g.PartitionChildren();

            Assert.AreEqual( g.Left[0], s1 );
            Assert.AreEqual( g.Right[0], s2 );
            Assert.AreEqual( g.Children[0], s3 );
        }

        [TestMethod]
        public void SubGroup()
        {
            Shape s1 = new Sphere();
            Shape s2 = new Sphere();
            List<Shape> shapes = new List<Shape>
            {
                s1,
                s2
            };
            Group g = new Group();

            g.SubGroup( shapes );

            Assert.AreEqual(g.Children.Count, 1);
            //Assert.AreEqual( g.Children, shapes );
        }
    }
}
