using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using F = RayTracer.Functions;

namespace RayTracer.Tests
{
    [TestClass]
    public class TransformTest
    {
        [TestMethod]
        public void TestTranslation()
        {
            Transformation t = new Transformation();
            t.Translation(5,-3,2);
            Point p = new Point(-3,4,5);

            Point p2 = new Point(t * p);
            Point p3 = new Point(2,1,7);

            bool isEqual = F.IsEqual(p2, p3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestReverseTranslation()
        {
            Transformation t = new Transformation();
            t.Translation(5,-3,2);
            Point p = new Point(-3,4,5);

            Point p2 = new Point(t.Inverse() * p);
            Point p3 = new Point(-8,7,3);

            bool isEqual = F.IsEqual(p2, p3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestTranslationOnVector()
        {
            Transformation t = new Transformation();
            t.Translation(5, -3, 2);
            Vector v = new Vector(-3, 4, 5);

            Vector v2 = new Vector(t * v);

            bool isEqual = F.IsEqual(v, v2);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestScaling()
        {
            Transformation t = new Transformation();
            t.Scaling(2,3,4);
            Point p1 = new Point(-4, 6, 8);

            Point p2 = new Point(t * p1);
            Point p3 = new Point(-8,18,32);

            bool isEqual = F.IsEqual(p2, p3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestScalingOnVector()
        {
            Transformation t = new Transformation();
            t.Scaling(2, 3, 4);
            Vector v1 = new Vector(-4, 6, 8);

            Vector v2 = new Vector(t * v1);
            Vector v3 = new Vector(-8, 18, 32);

            bool isEqual = F.IsEqual(v2, v3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestInverseScalingOnVector()
        {
            Transformation t = new Transformation();
            t.Scaling(2, 3, 4);
            Vector v1 = new Vector(-4, 6, 8);

            Vector v2 = new Vector(t.Inverse() * v1);
            Vector v3 = new Vector(-2, 2, 2);

            bool isEqual = F.IsEqual(v2, v3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestReflection()
        {
            Transformation t = new Transformation();
            t.Scaling(-1,1,1);
            Point p1 = new Point(2,3,4);

            Point p2 = new Point(t * p1);
            Point p3 = new Point(-2,3,4);

            bool isEqual = F.IsEqual(p2, p3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestRotationX1()
        {
            Transformation t = new Transformation();
            t.RotationX(Math.PI/4);
            Point p1 = new Point(0,1,0);

            Point p2 = new Point(t * p1);
            Point p3 = new Point(0, Math.Sqrt(2) / 2, Math.Sqrt(2) / 2);

            bool isEqual = F.IsEqual(p2, p3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestRotationX2()
        {
            Transformation t = new Transformation();
            t.RotationX(Math.PI/2);
            Point p1 = new Point(0,1,0);

            Point p2 = new Point(t * p1);
            Point p3 = new Point(0, 0, 1);

            bool isEqual = F.IsEqual(p2, p3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestRotationX3()
        {
            Transformation t = new Transformation();
            t.RotationX(Math.PI/4);
            Point p1 = new Point(0,1,0);

            Point p2 = new Point(t.Inverse() * p1);
            Point p3 = new Point(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);

            bool isEqual = F.IsEqual(p2, p3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestRotationY1()
        {
            Transformation t = new Transformation();
            t.RotationY(Math.PI/4);
            Point p1 = new Point(0,0,1);

            Point p2 = new Point(t * p1);
            Point p3 = new Point(Math.Sqrt(2) / 2, 0, Math.Sqrt(2) / 2);

            bool isEqual = F.IsEqual(p2, p3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestRotationY2()
        {
            Transformation t = new Transformation();
            t.RotationY(Math.PI / 2);
            Point p1 = new Point(0, 0, 1);

            Point p2 = new Point(t * p1);
            Point p3 = new Point(1, 0, 0);

            bool isEqual = F.IsEqual(p2, p3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestRotationZ1()
        {
            Transformation t = new Transformation();
            t.RotationZ(Math.PI/4);
            Point p1 = new Point(0,1,0);

            Point p2 = new Point(t * p1);
            Point p3 = new Point(-Math.Sqrt(2) / 2, Math.Sqrt(2) / 2, 0);

            bool isEqual = F.IsEqual(p2, p3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestRotationZ2()
        {
            Transformation t = new Transformation();
            t.RotationZ(Math.PI / 2);
            Point p1 = new Point(0, 1, 0);

            Point p2 = new Point(t * p1);
            Point p3 = new Point(-1, 0, 0);

            bool isEqual = F.IsEqual(p2, p3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestShearing1()
        {
            Transformation t = new Transformation();
            t.Shear(1, 0, 0, 0, 0, 0);
            Point p1 = new Point(2, 3, 4);

            Point p2 = new Point(t * p1);
            Point p3 = new Point(5, 3, 4);

            bool isEqual = F.IsEqual(p2, p3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestShearing2()
        {
            Transformation t = new Transformation();
            t.Shear(0, 1, 0, 0, 0, 0);
            Point p1 = new Point(2, 3, 4);

            Point p2 = new Point(t * p1);
            Point p3 = new Point(6, 3, 4);

            bool isEqual = F.IsEqual(p2, p3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestShearing3()
        {
            Transformation t = new Transformation();
            t.Shear(0, 0, 1, 0, 0, 0);
            Point p1 = new Point(2, 3, 4);

            Point p2 = new Point(t * p1);
            Point p3 = new Point(2, 5, 4);

            bool isEqual = F.IsEqual(p2, p3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestShearing4()
        {
            Transformation t = new Transformation();
            t.Shear(0, 0, 0, 1, 0, 0);
            Point p1 = new Point(2, 3, 4);

            Point p2 = new Point(t * p1);
            Point p3 = new Point(2, 7, 4);

            bool isEqual = F.IsEqual(p2, p3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestShearing5()
        {
            Transformation t = new Transformation();
            t.Shear(0, 0, 0, 0, 1, 0);
            Point p1 = new Point(2, 3, 4);

            Point p2 = new Point(t * p1);
            Point p3 = new Point(2, 3, 6);

            bool isEqual = F.IsEqual(p2, p3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestShearing6()
        {
            Transformation t = new Transformation();
            t.Shear(0, 0, 0, 0, 0, 1);
            Point p1 = new Point(2, 3, 4);

            Point p2 = new Point(t * p1);
            Point p3 = new Point(2, 3, 7);

            bool isEqual = F.IsEqual(p2, p3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void Chaining()
        {
            Transformation A = new Transformation();
            A.RotationX(Math.PI/2);

            Transformation B = new Transformation();
            B.Scaling(5, 5, 5);

            Transformation C = new Transformation();
            C.Translation(10, 5, 7);

            Transformation T = C * (B * A);
            
            Point p1 = new Point(1, 0, 1);

            Point p2 = new Point(T * p1);
            Point p3 = new Point(15, 0, 7);

            bool isEqual = F.IsEqual(p2, p3);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestWordDefaultTransform()
        {
            Point from = new Point(0,0,0);
            Point to = new Point(0,0,-1);
            Vector up = new Vector(0,1,0);
            Transformation t = Transformation.ViewTransform(from ,to, up);

            Transformation tr = new Transformation();

            bool ie = t.IsEqual(tr);

            Assert.AreEqual(ie, true);
        }

        [TestMethod]
        public void TestWorldPositiveZ()
        {
            Point from = new Point(0,0,0);
            Point to = new Point(0,0,1);
            Vector up = new Vector(0,1,0);
            Transformation t = Transformation.ViewTransform(from ,to, up);

            Transformation tr = new Transformation();
            tr.Scaling(-1,1,-1);

            bool ie = t.IsEqual(tr);

            Assert.AreEqual(ie, true);
        }

        [TestMethod]
        public void TestWorldMove()
        {
            Point from = new Point(0,0,8);
            Point to = new Point(0,0,0);
            Vector up = new Vector(0,1,0);
            Transformation t = Transformation.ViewTransform(from ,to, up);

            Transformation tr = new Transformation();
            tr.Translation(0,0,-8);

            bool ie = t.IsEqual(tr);

            Assert.AreEqual(ie, true);
        }

        [TestMethod]
        public void TestWorldMoveArbitrary()
        {
            Point from = new Point(1,3,2);
            Point to = new Point(4,-2,8);
            Vector up = new Vector(1,1,0);
            Transformation t = Transformation.ViewTransform(from ,to, up);

            Matrix m = new Matrix("-0.50709,0.50709,0.67612,-2.36643|" +
                                    "0.76772,0.60609,0.12122,-2.82843|" +
                                    "-0.35857,0.59761,-0.71714,0.00000|" +
                                    "0.00000,0.00000,0.00000,1.00000", 4, 4);

            Transformation tr = new Transformation(m);

            bool ie = t.IsEqual(tr);

            Assert.AreEqual(ie, true);
        }
    }
}
