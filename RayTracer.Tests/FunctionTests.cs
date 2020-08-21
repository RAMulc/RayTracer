using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using F = RayTracer.Functions;

namespace RayTracer.Tests
{
    [TestClass]
    public class FunctionTests
    {
        [TestMethod]
        public void IsEqualTestDouble()
        {
            //Arrange
            double a = 1.02;
            double b = 1.02;

            //Act
            bool isEqual = F.IsEqual(a, b);

            //Assert
            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void IsNotEqualTestDouble()
        {
            //Arrange
            double a = 1.02;
            double b = 1.0201;

            //Act
            bool isEqual = F.IsEqual(a, b);

            //Assert
            Assert.AreEqual(isEqual, false);
        }

        [TestMethod]
        public void IsEqualTestPoint()
        {
            //Arrange
            Point a = new Point(1, 2, 3);
            Point b = new Point(1, 2, 3);

            //Act
            bool isEqual = F.IsEqual(a, b);

            //Assert
            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void IsNotEqualTestPoint()
        {
            //Arrange
            Point a = new Point(1, 2, 3);
            Point b = new Point(1, 2.0001, 3);

            //Act
            bool isEqual = F.IsEqual(a, b);

            //Assert
            Assert.AreEqual(isEqual, false);
        }

        [TestMethod]
        public void IsEqualTestVector()
        {
            //Arrange
            Vector a = new Vector(1, 2, 3);
            Vector b = new Vector(1, 2, 3);

            //Act
            bool isEqual = F.IsEqual(a, b);

            //Assert
            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void IsNotEqualTestVector()
        {
            //Arrange
            Vector a = new Vector(1, 2, 3);
            Vector b = new Vector(1, 2.0001, 3);

            //Act
            bool isEqual = F.IsEqual(a, b);

            //Assert
            Assert.AreEqual(isEqual, false);
        }

        [TestMethod]
        public void DotProductTest()
        {
            //Arrange
            Vector a = new Vector(1, 2, 3);
            Vector b = new Vector(2, 3, 4);

            //Act
            double result = F.DotProduct(a, b);

            //Assert
            Assert.AreEqual(result, 20);
        }

        [TestMethod]
        public void CrossProductTest()
        {
            //Arrange
            Vector a = new Vector(1, 2, 3);
            Vector b = new Vector(2, 3, 4);
            Vector shouldBeA = new Vector(-1, 2, -1);
            Vector shouldBeB = new Vector(1, -2, 1);

            //Act
            Vector resultA = F.CrossProduct(a, b);
            Vector resultB = F.CrossProduct(b, a);

            //Assert
            Assert.AreEqual(F.IsEqual(resultA, shouldBeA), true);
            Assert.AreEqual(F.IsEqual(resultB, shouldBeB), true);
        }
    }
}
