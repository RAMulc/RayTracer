using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using F = RayTracer.Functions;

namespace RayTracer.Tests
{
    [TestClass]
    public class ColorTest
    {
        [TestMethod]
        public void InitialiseColorTest()
        {
            //Arrange
            Color color = new Color(-0.5, 0.4, 1.7);

            //Act

            //Assert
            Assert.AreEqual(color.Red, -0.5);
            Assert.AreEqual(color.Green, 0.4);
            Assert.AreEqual(color.Blue, 1.7);
        }

        [TestMethod]
        public void ColorAddTest()
        {
            //Arrange
            Color c1 = new Color(0.9, 0.6, 0.75);
            Color c2 = new Color(0.7, 0.1, 0.25);
            Color shouldBe = new Color(1.6, 0.7, 1.0);

            //Act
            Color cFinal = F.Add(c1, c2);

            //Assert
            Assert.AreEqual(cFinal.IsEqual(shouldBe), true);
        }

        [TestMethod]
        public void ColorSubtractTest()
        {
            //Arrange
            Color c1 = new Color(0.9, 0.6, 0.75);
            Color c2 = new Color(0.7, 0.1, 0.25);
            Color shouldBe = new Color(0.2, 0.5, 0.5);

            //Act
            Color cFinal = F.Subtract(c1, c2);

            //Assert
            Assert.AreEqual(F.IsEqual(cFinal, shouldBe), true);
        }

        [TestMethod]
        public void ColorMultiplyTest()
        {
            //Arrange
            Color c1 = new Color(0.2, 0.3, 0.4);
            double scalar = 2;
            Color shouldBe = new Color(0.4, 0.6, 0.8);

            //Act
            Color cFinal = F.Multiply(c1, scalar);

            //Assert
            Assert.AreEqual(F.IsEqual(cFinal, shouldBe), true);
        }

        [TestMethod]
        public void HadamardMultiplyTest()
        {
            //Arrange
            Color c1 = new Color(1, 0.2, 0.4);
            Color c2 = new Color(0.9, 1, 0.1);
            Color shouldBe = new Color(0.9, 0.2, 0.04);

            //Act
            Color cFinal = c1.HadamardProduct(c2);

            //Assert
            Assert.AreEqual(F.IsEqual(cFinal, shouldBe), true);
        }
    }
}
