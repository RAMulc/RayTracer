using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayTracer.Tests
{
    [TestClass]
    public class BoundsTest
    {
        [TestMethod]
        public void EmptyBox()
        {
            BoundingBox b = new BoundingBox();

            bool ie = b.Min.IsEqual( new Point( double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity ) );
            Assert.AreEqual( ie, true );

            ie = b.Max.IsEqual( new Point( double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity ) );
            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void BoxWithVolume()
        {
            BoundingBox b = new BoundingBox( new Point( -1, -2, -3 ), new Point( 3, 2, 1 ) );

            bool ie = b.Min.IsEqual( new Point( -1, -2, -3 ) );
            Assert.AreEqual( ie, true );

            ie = b.Max.IsEqual( new Point( 3, 2, 1 ) );
            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void AddPointsToBox()
        {
            BoundingBox b = new BoundingBox();
            b.AddPoint( new Point( -5, 2, 0 ) );
            b.AddPoint( new Point( 7, 0, -3 ) );

            bool ie = b.Min.IsEqual( new Point( -5, 0, -3 ) );
            Assert.AreEqual( ie, true );

            ie = b.Max.IsEqual( new Point( 7, 2, 0 ) );
            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void SphereBoundingBox()
        {
            Shape s = new Sphere();
            BoundingBox b = new BoundingBox( s );

            bool ie = b.Min.IsEqual( new Point( -1, -1, -1 ) );
            Assert.AreEqual( ie, true );
            ie = b.Max.IsEqual( new Point( 1, 1, 1 ) );
            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void PlaneBoundingBox()
        {
            Shape s = new Plane();
            BoundingBox b = new BoundingBox( s );

            bool ie = b.Min.IsEqual( new Point( double.NegativeInfinity, 0, double.NegativeInfinity ) );
            Assert.AreEqual( ie, true );
            ie = b.Max.IsEqual( new Point( double.PositiveInfinity, 0, double.PositiveInfinity ) );
            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void CubeBoundingBox()
        {
            Shape s = new Cube();
            BoundingBox b = new BoundingBox( s );

            bool ie = b.Min.IsEqual( new Point( -1, -1, -1 ) );
            Assert.AreEqual( ie, true );
            ie = b.Max.IsEqual( new Point( 1, 1, 1 ) );
            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void CylinderBoundingBox()
        {
            Shape s = new Cylinder();
            BoundingBox b = new BoundingBox( s );

            bool ie = b.Min.IsEqual( new Point( -1, double.NegativeInfinity, -1 ) );
            Assert.AreEqual( ie, true );
            ie = b.Max.IsEqual( new Point( 1, double.PositiveInfinity, 1 ) );
            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void BoundedCylinderBoundingBox()
        {
            Shape s = new Cylinder
            {
                Minimum = -5,
                Maximum = 3
            };
            BoundingBox b = new BoundingBox( s );

            bool ie = b.Min.IsEqual( new Point( -1, -5, -1 ) );
            Assert.AreEqual( ie, true );
            ie = b.Max.IsEqual( new Point( 1, 3, 1 ) );
            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void ConeBoundingBox()
        {
            Shape s = new Cone();
            BoundingBox b = new BoundingBox( s );

            bool ie = b.Min.IsEqual( new Point( double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity ) );
            Assert.AreEqual( ie, true );
            ie = b.Max.IsEqual( new Point( double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity ) );
            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void BoundedConeBoundingBox()
        {
            Shape s = new Cone
            {
                Minimum = -5,
                Maximum = 3
            };
            BoundingBox b = new BoundingBox( s );

            bool ie = b.Min.IsEqual( new Point( -5, -5, -5 ) );
            Assert.AreEqual( ie, true );
            ie = b.Max.IsEqual( new Point( 5, 3, 5 ) );
            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void TestShapeBoundingBox()
        {
            Shape s = new TestShape();
            BoundingBox b = new BoundingBox( s );

            bool ie = b.Min.IsEqual( new Point( -1, -1, -1 ) );
            Assert.AreEqual( ie, true );
            ie = b.Max.IsEqual( new Point( 1, 1, 1 ) );
            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void TriangleBoundingBox()
        {
            Point p1 = new Point( -3, 7, 2 );
            Point p2 = new Point( 6, 2, -4 );
            Point p3 = new Point( 2, -1, -1 );

            Triangle s = new Triangle( p1, p2, p3 );
            BoundingBox b = new BoundingBox( s );

            bool ie = b.Min.IsEqual( new Point( -3, -1, -4 ) );
            Assert.AreEqual( ie, true );
            ie = b.Max.IsEqual( new Point( 6, 7, 2 ) );
            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void AddBoundingBoxToAnother()
        {
            BoundingBox b1 = new BoundingBox( new Point( -5, -2, 0 ), new Point( 7, 4, 4 ) );
            BoundingBox b2 = new BoundingBox( new Point( 8, -7, -2 ), new Point( 14, 2, 8 ) );

            b1.AddBoundingBox( b2 );
            bool ie = b1.Min.IsEqual( new Point( -5, -7, -2 ) );
            Assert.AreEqual( ie, true );
            ie = b1.Max.IsEqual( new Point( 14, 4, 8 ) );
            Assert.AreEqual( ie, true );

        }

        [TestMethod]
        public void CheckPointInBoundingBox()
        {
            BoundingBox b = new BoundingBox( new Point( 5, -2, 0 ), new Point( 11, 4, 7 ) );
            Point p = new Point( 5, -2, 0 );
            Assert.AreEqual( b.ContainsPoint( p ), true );

            p = new Point( 11, 4, 7 );
            Assert.AreEqual( b.ContainsPoint( p ), true );

            p = new Point( 8, 1, 3 );
            Assert.AreEqual( b.ContainsPoint( p ), true );

            p = new Point( 3, 0, 3 );
            Assert.AreEqual( b.ContainsPoint( p ), false );

            p = new Point( 8, -4, 3 );
            Assert.AreEqual( b.ContainsPoint( p ), false );

            p = new Point( 8, 1, -1 );
            Assert.AreEqual( b.ContainsPoint( p ), false );

            p = new Point( 13, 1, 3 );
            Assert.AreEqual( b.ContainsPoint( p ), false );

            p = new Point( 8, 5, 3 );
            Assert.AreEqual( b.ContainsPoint( p ), false );

            p = new Point( 8, 1, 8 );
            Assert.AreEqual( b.ContainsPoint( p ), false );
        }

        [TestMethod]
        public void CheckBoundingBoxInBoundingBox()
        {
            BoundingBox b1 = new BoundingBox( new Point( 5, -2, 0 ), new Point( 11, 4, 7 ) );
            Point pMin = new Point( 5, -2, 0 );
            Point pMax = new Point( 11, 4, 7 );
            BoundingBox b2 = new BoundingBox( pMin, pMax );
            Assert.AreEqual( b1.ContainsBoundingBox( b2 ), true );

            pMin = new Point( 6, -1, 1 );
            pMax = new Point( 10, 3, 6 );
            b2 = new BoundingBox( pMin, pMax );
            Assert.AreEqual( b1.ContainsBoundingBox( b2 ), true );

            pMin = new Point( 4, -3, -1 );
            pMax = new Point( 10, 3, 6 );
            b2 = new BoundingBox( pMin, pMax );
            Assert.AreEqual( b1.ContainsBoundingBox( b2 ), false );

            pMin = new Point( 6, -1, 1 );
            pMax = new Point( 15, 5, 8 );
            b2 = new BoundingBox( pMin, pMax );
            Assert.AreEqual( b1.ContainsBoundingBox( b2 ), false );
        }

        [TestMethod]
        public void TransformBoundingBox()
        {
            BoundingBox b1 = new BoundingBox( new Point( -1, -1, -1 ), new Point( 1, 1, 1 ) );
            Transformation t1 = new Transformation();
            t1.RotationX( Math.PI / 4 );
            Transformation t2 = new Transformation();
            t2.RotationY( Math.PI / 4 );
            Transformation t3 = new Transformation
            {
                TMatrix = t1.TMatrix * t2.TMatrix
            };

            BoundingBox b2 = BoundingBox.Transform( b1, t3 );

            bool ie = b2.Min.IsEqual( new Point( -1.4142, -1.7071, -1.7071 ), 4 );
            Assert.AreEqual( ie, true );

            ie = b2.Max.IsEqual( new Point( 1.4142, 1.7071, 1.7071 ), 4 );
            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void RayIntersectBoundingBox()
        {
            BoundingBox b = new BoundingBox( new Point( -1, -1, -1 ), new Point( 1, 1, 1 ) );

            //1
            Point origin = new Point( 5, 0.5, 0 );
            Vector direction = Vector.Normalize( new Vector( -1, 0, 0 ) );
            Ray ray = new Ray( origin, direction );

            List<double> t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), true );

            //2
            origin = new Point( -5, 0.5, 0 );
            direction = Vector.Normalize( new Vector( 1, 0, 0 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), true );

            //3
            origin = new Point( 0.5, 5, 0 );
            direction = Vector.Normalize( new Vector( 0, -1, 0 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), true );

            //4
            origin = new Point( 0.5, -5, 0 );
            direction = Vector.Normalize( new Vector( 0, 1, 0 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), true );

            //5
            origin = new Point( 0.5, 0, 5 );
            direction = Vector.Normalize( new Vector( 0, 0, -1 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), true );

            //6
            origin = new Point( 0.5, 0, -5 );
            direction = Vector.Normalize( new Vector( 0, 0, 1 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), true );

            //7
            origin = new Point( 0, 0.5, 0 );
            direction = Vector.Normalize( new Vector( 0, 0, 1 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), true );

            //8
            origin = new Point( -2, 0, 0 );
            direction = Vector.Normalize( new Vector( 2, 4, 6 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), false );

            //9
            origin = new Point( 0, -2, 0 );
            direction = Vector.Normalize( new Vector( 6, 2, 4 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), false );

            //10
            origin = new Point( 0, 0, -2 );
            direction = Vector.Normalize( new Vector( 4, 6, 2 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), false );

            //11
            origin = new Point( 2, 0, 2 );
            direction = Vector.Normalize( new Vector( 0, 0, -1 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), false );

            //12
            origin = new Point( 0, 2, 2 );
            direction = Vector.Normalize( new Vector( 0, -1, 0 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), false );

            //13
            origin = new Point( 2, 2, 0 );
            direction = Vector.Normalize( new Vector( -1, 0, 0 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), false );
        }

        [TestMethod]
        public void RayIntersectNonCubicBoundingBox()
        {
            BoundingBox b = new BoundingBox( new Point( 5, -2, 0 ), new Point( 11, 4, 7 ) );

            //1
            Point origin = new Point( 15, 1, 2 );
            Vector direction = Vector.Normalize( new Vector( -1, 0, 0 ) );
            Ray ray = new Ray( origin, direction );

            List<double> t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), true );

            //2
            origin = new Point( -5, -1, 4 );
            direction = Vector.Normalize( new Vector( 1, 0, 0 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), true );

            //3
            origin = new Point( 7, 6, 5 );
            direction = Vector.Normalize( new Vector( 0, -1, 0 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), true );

            //4
            origin = new Point( 9, -5, 6 );
            direction = Vector.Normalize( new Vector( 0, 1, 0 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), true );

            //5
            origin = new Point( 8, 2, 12 );
            direction = Vector.Normalize( new Vector( 0, 0, -1 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), true );

            //6
            origin = new Point( 6, 0, -5 );
            direction = Vector.Normalize( new Vector( 0, 0, 1 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), true );

            //7
            origin = new Point( 8, 1, 3.5 );
            direction = Vector.Normalize( new Vector( 0, 0, 1 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), true );

            //8
            origin = new Point( 9, -1, -8 );
            direction = Vector.Normalize( new Vector( 2, 4, 6 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), false );

            //9
            origin = new Point( 8, 3, -4 );
            direction = Vector.Normalize( new Vector( 6, 2, 4 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), false );

            //10
            origin = new Point( 9, -1, -2 );
            direction = Vector.Normalize( new Vector( 4, 6, 2 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), false );

            //11
            origin = new Point( 4, 0, 9 );
            direction = Vector.Normalize( new Vector( 0, 0, -1 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), false );

            //12
            origin = new Point( 8, 6, -1 );
            direction = Vector.Normalize( new Vector( 0, -1, 0 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), false );

            //13
            origin = new Point( 12, 5, 4 );
            direction = Vector.Normalize( new Vector( -1, 0, 0 ) );
            ray = new Ray( origin, direction );

            t = new List<double>();

            Assert.AreEqual( BoundingBox.Intersects( b, ray, ref t ), false );
        }

        [TestMethod]
        public void SplitACube()
        {
            BoundingBox b = new BoundingBox( new Point( -1, -4, -5 ), new Point( 9, 6, 5 ) );

            bool ie = b.Left.Min.IsEqual( new Point( -1, -4, -5 ) );
            Assert.AreEqual( ie, true );
            ie = b.Left.Max.IsEqual( new Point( 4, 6, 5 ) );
            Assert.AreEqual( ie, true );
            ie = b.Right.Min.IsEqual( new Point( 4, -4, -5 ) );
            Assert.AreEqual( ie, true );
            ie = b.Right.Max.IsEqual( new Point( 9, 6, 5 ) );
            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void SplitXWideCube()
        {
            BoundingBox b = new BoundingBox( new Point( -1, -2, -3 ), new Point( 9, 5.5, 3 ) );

            bool ie = b.Left.Min.IsEqual( new Point( -1, -2, -3 ) );
            Assert.AreEqual( ie, true );
            ie = b.Left.Max.IsEqual( new Point( 4, 5.5, 3 ) );
            Assert.AreEqual( ie, true );
            ie = b.Right.Min.IsEqual( new Point( 4, -2, -3 ) );
            Assert.AreEqual( ie, true );
            ie = b.Right.Max.IsEqual( new Point( 9, 5.5, 3 ) );
            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void SplitYWideCube()
        {
            BoundingBox b = new BoundingBox( new Point( -1, -2, -3 ), new Point( 5, 8, 3 ) );

            bool ie = b.Left.Min.IsEqual( new Point( -1, -2, -3 ) );
            Assert.AreEqual( ie, true );
            ie = b.Left.Max.IsEqual( new Point( 5, 3, 3 ) );
            Assert.AreEqual( ie, true );
            ie = b.Right.Min.IsEqual( new Point( -1, 3, -3 ) );
            Assert.AreEqual( ie, true );
            ie = b.Right.Max.IsEqual( new Point( 5, 8, 3 ) );
            Assert.AreEqual( ie, true );
        }

        [TestMethod]
        public void SplitZWideCube()
        {
            BoundingBox b = new BoundingBox( new Point( -1, -2, -3 ), new Point( 5, 3, 7 ) );

            bool ie = b.Left.Min.IsEqual( new Point( -1, -2, -3 ) );
            Assert.AreEqual( ie, true );
            ie = b.Left.Max.IsEqual( new Point( 5, 3, 2 ) );
            Assert.AreEqual( ie, true );
            ie = b.Right.Min.IsEqual( new Point( -1, -2, 2 ) );
            Assert.AreEqual( ie, true );
            ie = b.Right.Max.IsEqual( new Point( 5, 3, 7 ) );
            Assert.AreEqual( ie, true );
        }
    }
}
