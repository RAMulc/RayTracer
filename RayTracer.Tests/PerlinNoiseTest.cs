using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayTracer.Tests
{
    [TestClass]
    public class PerlinNoiseTest
    {
        [TestMethod]
        public void TestPerlinNoise()
        {
            Point c = PerlinNoise.Noise( new Point( 1, 1, 1 ), 100, 50);


        }
    }
}
