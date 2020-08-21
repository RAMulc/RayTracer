using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static RayTracer.Functions;

namespace RayTracer.Tests
{
    [TestClass]
    public class TupleTest
    {
        [TestMethod]
        public void TestIsAPoint()
        {
            //Arrange
            RTuple rTuple = new RTuple(4.3, -4.2, 3.1, 1);

            //Act


            //Assert
            Assert.AreEqual(rTuple.X, 4.3);
            Assert.AreEqual(rTuple.Y, -4.2);
            Assert.AreEqual(rTuple.Z, 3.1);
            Assert.AreEqual((int)rTuple.W, 1);
        }

        [TestMethod]
        public void TestIsAVector()
        {
            //Arrange
            RTuple rTuple = new RTuple(4.3, -4.2, 3.1, 0);

            //Act


            //Assert
            Assert.AreEqual(rTuple.X, 4.3);
            Assert.AreEqual(rTuple.Y, -4.2);
            Assert.AreEqual(rTuple.Z, 3.1);
            Assert.AreEqual(rTuple.W, 0);
        }

        [TestMethod]
        public void VectorIsTuple()
        {
            //Arrange
            Vector vector = new Vector(4, -4, 3);

            //Act
            RTuple rTuple = vector;

            //Assert
            Assert.AreEqual(rTuple.X, 4);
            Assert.AreEqual(rTuple.Y, -4);
            Assert.AreEqual(rTuple.Z, 3);
            Assert.AreEqual(rTuple.W, 0);
        }

        [TestMethod]
        public void PointIsTuple()
        {
            //Arrange
            Point point = new Point(4, -4, 3);

            //Act
            RTuple rTuple = point;

            //Assert
            Assert.AreEqual(rTuple.X, 4);
            Assert.AreEqual(rTuple.Y, -4);
            Assert.AreEqual(rTuple.Z, 3);
            Assert.AreEqual(rTuple.W, 1);
        }

        [TestMethod]
        public void AddTuples()
        {
            //Arrange
            RTuple a = new RTuple(3, -2, 5, 1);
            RTuple b = new RTuple(-2, 3, 1, 0);
            RTuple shouldBe = new RTuple(1, 1, 6, 1);

            //Act
            RTuple actual = Add(a, b);

            //Assert
            Assert.AreEqual(IsEqual(shouldBe,actual), true);
        }

        [TestMethod]
        public void SubtractPoints()
        {
            //Arrange
            Point a = new Point(3, 2, 1);
            Point b = new Point(5, 6, 7);
            Vector shouldBe = new Vector(-2, -4, -6);

            //Act
            RTuple actual = Subtract(a, b);

            //Assert
            Assert.AreEqual(IsEqual(shouldBe,actual), true);
        }

        [TestMethod]
        public void SubtractVectorFromPoint()
        {
            //Arrange
            Point a = new Point(3, 2, 1);
            Vector b = new Vector(5, 6, 7);
            Point shouldBe = new Point(-2, -4, -6);

            //Act
            RTuple actual = Subtract(a, b);

            //Assert
            Assert.AreEqual(IsEqual(shouldBe,actual), true);
        }

        [TestMethod]
        public void SubtractVectorFromVector()
        {
            //Arrange
            Vector a = new Vector(3, 2, 1);
            Vector b = new Vector(5, 6, 7);
            Vector shouldBe = new Vector(-2, -4, -6);

            //Act
            RTuple actual = Subtract(a, b);

            //Assert
            Assert.AreEqual(IsEqual(shouldBe,actual), true);
        }

        [TestMethod]
        public void SubtractVectorFromZeroVector()
        {
            //Arrange
            Vector a = new Vector(0, 0, 0);
            Vector b = new Vector(1, -2, 3);
            Vector shouldBe = new Vector(-1, 2, -3);

            //Act
            RTuple actual = Subtract(a, b);

            //Assert
            Assert.AreEqual(IsEqual(shouldBe,actual), true);
        }

        [TestMethod]
        public void NegateTuple()
        {
            //Arrange
            RTuple rTuple = new RTuple(1, -2, 3 , 1);
            RTuple shouldBe = new RTuple(-1, 2, -3, 1);

            //Act
            RTuple actual = Negate(rTuple);
            RTuple actual2 = -rTuple;

            //Assert
            Assert.AreEqual(IsEqual(shouldBe,actual), true);
            Assert.AreEqual(IsEqual(shouldBe,actual2), true);
        }

        [TestMethod]
        public void ScalarTupleMultiply()
        {
            //Arrange
            RTuple rTuple = new RTuple(1, -2, 3 , -4);
            double scalar = 3.5;
            RTuple shouldBe = new RTuple(3.5, -7, 10.5, -14);

            //Act
            RTuple actual = Multiply(rTuple, scalar);

            //Assert
            Assert.AreEqual(IsEqual(shouldBe,actual), true);
        }

        [TestMethod]
        public void ScalarTupleMultiplyByFraction()
        {
            //Arrange
            RTuple rTuple = new RTuple(1, -2, 3 , -4);
            double scalar = 0.5;
            RTuple shouldBe = new RTuple(0.5, -1, 1.5, -2);

            //Act
            RTuple actual = Multiply(rTuple, scalar);

            //Assert
            Assert.AreEqual(IsEqual(shouldBe,actual), true);
        }

        [TestMethod]
        public void TupleDivision()
        {
            //Arrange
            RTuple rTuple = new RTuple(1, -2, 3 , -4);
            double divisor = 2;
            RTuple shouldBe = new RTuple(0.5, -1, 1.5, -2);

            //Act
            RTuple actual = Division(rTuple, divisor);

            //Assert
            Assert.AreEqual(IsEqual(shouldBe,actual), true);
        }

        [TestMethod]
        public void TupleReflect()
        {
            //Arrange
            Vector v = new Vector(1, -1, 0);
            Vector n = new Vector(0, 1, 0);

            Vector vs = new Vector(1, 1, 0);

            //Act
            Vector vr = v.Reflect(n);
            bool isEqual = IsEqual(vs, vr);

            //Assert
            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TupleReflectSlope()
        {
            //Arrange
            Vector v = new Vector(0, -1, 0);
            Vector n = new Vector(Math.Sqrt(2)/2, Math.Sqrt(2)/2, 0);

            Vector vs = new Vector(1, 0, 0);

            //Act
            Vector vr = v.Reflect(n);
            bool isEqual = IsEqual(vs, vr);

            //Assert
            Assert.AreEqual(isEqual, true);
        }
    }
}
