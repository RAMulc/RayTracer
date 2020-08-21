using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using F = RayTracer.Functions;

namespace RayTracer.Tests
{
    [TestClass]
    public class ClockTest
    {
        [TestMethod]
        public void TestClock()
        {
            //Arrange
            Point p1 = new Point(0, 0, 1);
            List<Point> points = new List<Point>();
            

            int size = 100;

            for (int i = 0; i < 12; i++)
            {
                Transformation A = new Transformation();
                A.RotationY(i * Math.PI / 6);

                Transformation B = new Transformation();
                B.Scaling(0.75 * size / 2, 0, 0.75 * size / 2);

                Transformation C = new Transformation();
                C.Translation(size / 2, 0, size / 2);

                Transformation T = C * (A * B);

                Point p2 = new Point(T * p1);
                points.Add(p2);
            }


            Canvas canvas = new Canvas(size, size);
            Color color = new Color(0, 0, 1);

            //Act
            foreach (Point point in points)
            {
                int x = (int) point.X;
                int y = (int) point.Z;

                if (x >= 0 && x <= Canvas.Width)
                    if (y >= 0 && y <= Canvas.Height)
                        canvas.Pixels[x, y] = new Pixel(x, y, color);
            }
            
            canvas.WritePpm();

            //Assert


        }
    }
}
