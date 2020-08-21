using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayTracer.Tests
{
    [TestClass]
    public class LightTest
    {
        [TestMethod]
        public void TestLight()
        {
            Light light = new Light(new Point(0,0,0), new Color(1,1,1));

            Point position = new Point(0,0,0);

            bool isEqual2 = light.Position.IsEqual(position);
            bool ie = light.Intensity.IsEqual(new Color(1, 1, 1));

            Assert.AreEqual(ie, true);
            Assert.AreEqual(isEqual2, true);
        }
    }
}
