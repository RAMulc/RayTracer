using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using F = RayTracer.Functions;

namespace RayTracer.Tests
{
    [TestClass]
    public class CanvasTest
    {
        [TestMethod]
        public void TestCanvas()
        {
            //Arrange
            Canvas canvas = new Canvas(10, 20);

            //Act

            //Assert
            Assert.AreEqual(Canvas.Width, 10);
            Assert.AreEqual(Canvas.Height, 20);
        }

        [TestMethod]
        public void TestPixel()
        {
            //Arrange
            Canvas canvas = new Canvas(10, 20);
            Color color = new Color(1, 0, 0);
            Pixel pixel = new Pixel(2, 3, color);

            canvas.WritePixel(pixel);

            //Act

            //Assert
            Assert.AreEqual(F.IsEqual(canvas.Pixels[2,3].PixelColor, color), true);
        }

        [TestMethod]
        public void TestWriteToPPM()
        {
            //Arrange
            Canvas canvas = new Canvas(5, 3);
            Color color1 = new Color(1.5, 0, 0);
            Pixel pixel1 = new Pixel(0, 0, color1);

            Color color2 = new Color(0, 0.5, 0);
            Pixel pixel2 = new Pixel(2, 1, color2);

            Color color3 = new Color(-0.5, 0, 1);
            Pixel pixel3 = new Pixel(4, 2, color3);

            canvas.WritePixel(pixel1);
            canvas.WritePixel(pixel2);
            canvas.WritePixel(pixel3);

            //Act
            List<string> ppmString = canvas.CanvasToPpmString();
            foreach (string ppmLine in ppmString)
            {
                Console.WriteLine(ppmLine);
            }

            //Assert
        }

        [TestMethod]
        public void TestWriteToPPM70Char()
        {
            //Arrange
            Canvas canvas = new Canvas(10, 2);
            Color color = new Color(1, 0.8, 0.6);
            
            for (int i = 0; i < Canvas.Height; i++)
            {
                for (int j = 0; j < Canvas.Width; j++)
                    canvas.Pixels[j, i] = new Pixel(j, i, color);
            }

            //Act
            List<string> ppmString = canvas.CanvasToPpmString();
            foreach (string ppmLine in ppmString)
            {
                Console.WriteLine(ppmLine);
            }

            //Assert
        }

        [TestMethod]
        public void TestWriteToPpmFile()
        {
            //Arrange
            Canvas canvas = new Canvas(10, 2);
            Color color = new Color(1, 0, 0.6);
            
            for (int i = 0; i < Canvas.Height; i++)
            {
                for (int j = 0; j < Canvas.Width; j++)
                    canvas.Pixels[j, i] = new Pixel(j, i, color);
            }

            //Act
            List<string> ppmString = canvas.CanvasToPpmString();
            canvas.WritePpm();

            //Assert
        }
    }
}
