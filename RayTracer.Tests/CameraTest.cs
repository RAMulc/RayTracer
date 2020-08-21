using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using F = RayTracer.Functions;

namespace RayTracer.Tests
{
    [TestClass]
    public class CameraTest
    {
        [TestMethod]
        public void TestCamera()
        {
            Camera camera = new Camera(160, 120, Math.PI/2);

            Assert.AreEqual(camera.HSize, 160);
            Assert.AreEqual(camera.VSize, 120);
            Assert.AreEqual(camera.FieldOfView, Math.PI/2);
        }

        [TestMethod]
        public void TestPixelSize1()
        {
            Camera camera = new Camera(200, 125, Math.PI/2);

            bool ie = F.IsEqual(camera.PixelSize, 0.01);
            
            Assert.AreEqual(ie, true);

        }

        [TestMethod]
        public void TestPixelSize2()
        {
            Camera camera = new Camera(125, 200, Math.PI/2);

            bool ie = F.IsEqual(camera.PixelSize, 0.01);
            
            Assert.AreEqual(ie, true);

        }

        [TestMethod]
        public void TestRayThruCanvas()
        {
            Camera camera = new Camera(201, 101, Math.PI/2);
            Ray r = camera.RayForPixel(100, 50);

            bool ie1 = F.IsEqual(r.Origin, new Point(0,0,0));
            bool ie2 = F.IsEqual(r.Direction, new Vector(0,0,-1));

            Assert.AreEqual(ie1, true);
        }

        [TestMethod]
        public void TestRayThruCanvasCorner()
        {
            Camera camera = new Camera(201, 101, Math.PI/2);
            Ray r = camera.RayForPixel(0, 0);

            bool ie1 = F.IsEqual(r.Origin, new Point(0,0,0));
            bool ie2 = F.IsEqual(r.Direction, new Vector(0.66519, 0.33259, -0.66851));

            Assert.AreEqual(ie1, true);
        }

        [TestMethod]
        public void TestRayThruCanvasWithTransform()
        {
            Camera camera = new Camera(201, 101, Math.PI/2);
            Transformation t1 = new Transformation();
            t1.RotationY(Math.PI/4);
            Transformation t2 = new Transformation();
            t2.Translation(0, -2, 5);
            Transformation t3 = t1 * t2;

            camera.Transform = t3;

            Ray r = camera.RayForPixel(100, 50);

            bool ie1 = F.IsEqual(r.Origin, new Point(0, 2, -5));
            bool ie2 = F.IsEqual(r.Direction, new Vector(Math.Sqrt(2)/2, 0, -Math.Sqrt(2)/2));

            Assert.AreEqual(ie1, true);
        }

        [TestMethod]
        public void TestRenderWorld()
        {
            World w = new World();
            Camera c = new Camera(11 ,11, Math.PI/2);

            Point from = new Point(0, 0, -5);
            Point to = new Point(0, 0, 0);
            Vector up = new Vector(0, 1, 0);

            c.Transform = Transformation.ViewTransform(from, to, up);

            Canvas image = c.Render(w);

            Color col55 = image.Pixels[5, 5].PixelColor;

            bool ie = col55.IsEqual(new Color(0.38066, 0.47583, 0.2855));

            Assert.AreEqual(ie, true);
        }
    }
}
