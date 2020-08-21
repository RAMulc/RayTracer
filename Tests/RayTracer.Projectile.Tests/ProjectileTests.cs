using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayTracer.Projectile;

namespace RayTracer.Projectile.Tests
{
    [TestClass]
    public class ProjectileTests
    {
        [TestMethod]
        public void ProjectileTest()
        {
            //Arrange
            Projectile proj = new Projectile(new Point(0,0,0), Functions.Normalize(new Vector(1,2,0)));
            Environment env = new Environment(new Vector(0, -0.1, 0), new Vector(-0.01, 0, 0));

            //Act
            while (proj.Position.Y >= 0)
            {
                proj = Tracker.Tick(env, proj);
                Console.WriteLine(proj.Position.Y);
            }

            //Assert


        }

        [TestMethod]
        public void ProjectileToPpmTest()
        {
            //Arrange
            Point start = new Point(0, 1, 0);
            Vector velocity = Functions.Multiply(Functions.Normalize(new Vector(1,1.8,0)),11.25);
            Projectile proj = new Projectile(start, velocity);

            Vector gravity = new Vector(0, -0.1, 0);
            Vector wind = new Vector(-0.01, 0, 0);
            Environment env = new Environment(gravity, wind);

            Canvas canvas = new Canvas(900, 550);
            Color color = new Color(0, 0, 0);

            //Act
            while (proj.Position.Y >= 0)
            {
                int x = (int) proj.Position.X;
                int y = Canvas.Height - (int) proj.Position.Y;

                if (x >= 0 && x <= Canvas.Width)
                    if (y >= 0 && y <= Canvas.Height)
                        canvas.Pixels[x, y] = new Pixel(x, y, color);
                
                proj = Tracker.Tick(env, proj);
            }
            canvas.WritePpm();

            //Assert


        }
    }
}
