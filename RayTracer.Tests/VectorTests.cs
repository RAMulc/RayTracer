using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using F = RayTracer.Functions;

namespace RayTracer.Tests
{
    [TestClass]
    public class VectorTests
    {
        [TestMethod]
        public void CheckMagnitude()
        {
            //Arrange
            Vector a = new Vector(1, 0, 0);
            Vector b = new Vector(0, 1, 0);
            Vector c = new Vector(0, 0, 1);
            Vector d = new Vector(1, 2, 3);
            Vector e = new Vector(-1, -2, -3);

            //Act

            //Assert
            Assert.AreEqual(a.Magnitude, 1);
            Assert.AreEqual(b.Magnitude, 1);
            Assert.AreEqual(c.Magnitude, 1);
            Assert.AreEqual(d.Magnitude, Math.Sqrt(14));
            Assert.AreEqual(e.Magnitude, Math.Sqrt(14));
        }

        [TestMethod]
        public void NormaliseVector()
        {
            //Arrange
            Vector a = new Vector(4, 0, 0);
            Vector b = new Vector(0, 4, 0);
            Vector c = new Vector(0, 0, 4);
            Vector d = new Vector(1, 2, 3);
            Vector e = new Vector(-1, -2, -3);

            //Act
            a.Normalize();
            b.Normalize();
            c.Normalize();
            d.Normalize();
            e.Normalize();

            //Assert
            Assert.AreEqual(a.Magnitude, 1);
            Assert.AreEqual(b.Magnitude, 1);
            Assert.AreEqual(c.Magnitude, 1);
            Assert.AreEqual(F.IsEqual(d.Magnitude,1), true);
            Assert.AreEqual(F.IsEqual(e.Magnitude,1), true);
        }
    }
}
