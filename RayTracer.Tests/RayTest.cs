using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static RayTracer.Functions;

namespace RayTracer.Tests
{
    [TestClass]
    public class RayTest
    {
        [TestMethod]
        public void TestRay()
        {
            Ray ray = new Ray(new Point(1,2,3), new Vector(4,5,6));

            Point origin = new Point(1, 2, 3);
            Vector vector = new Vector(4, 5, 6);

            bool isEqual1 = ray.Origin.IsEqual(origin);
            bool isEqual2 = ray.Direction.IsEqual(vector);

            Assert.AreEqual(isEqual1, true);
            Assert.AreEqual(isEqual2, true);
        }

        [TestMethod]
        public void TestPointAtDistance()
        {
            Ray ray = new Ray(new Point(2, 3, 4), new Vector(1, 0, 0));

            Point p1 = Ray.Position(ray, 0);
            Point p1Test = new Point(2,3,4);
            Point p2 = Ray.Position(ray, 1);
            Point p2Test = new Point(3,3,4);
            Point p3 = Ray.Position(ray, -1);
            Point p3Test = new Point(1,3,4);
            Point p4 = Ray.Position(ray, 2.5);
            Point p4Test = new Point(4.5,3,4);

            bool isEqual1 = p1.IsEqual(p1Test);
            bool isEqual2 = p2.IsEqual(p2Test);
            bool isEqual3 = p3.IsEqual(p3Test);
            bool isEqual4 = p4.IsEqual(p4Test);

            Assert.AreEqual(isEqual1, true);
            Assert.AreEqual(isEqual2, true);
            Assert.AreEqual(isEqual3, true);
            Assert.AreEqual(isEqual4, true);
        }

        [TestMethod]
        public void TestRayTranslate()
        {
            Ray r = new Ray(new Point(1,2,3), new Vector(0,1,0));
            Transformation t = new Transformation();
            t.Translation(3, 4, 5);

            Ray r2 = r.Transform(t);
            
            bool isEqual1 = r2.Origin.IsEqual(new Point(4, 6, 8));
            bool isEqual2 = r2.Direction.IsEqual(new Vector(0, 1, 0));

            Assert.AreEqual(isEqual1, true);
            Assert.AreEqual(isEqual2, true);
        }

        [TestMethod]
        public void TestRayScale()
        {
            Ray r = new Ray(new Point(1,2,3), new Vector(0,1,0));
            Transformation t = new Transformation();
            t.Scaling(2, 3, 4);

            Ray r2 = r.Transform(t);

            bool isEqual1 = r2.Origin.IsEqual(new Point(2, 6, 12));
            bool isEqual2 = r2.Direction.IsEqual(new Vector(0, 3, 0));

            Assert.AreEqual(isEqual1, true);
            Assert.AreEqual(isEqual2, true);
        }
    }
}
