using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static RayTracer.Functions;

namespace RayTracer.Tests
{
    [TestClass]
    public class SilhouetteTest
    {
        [TestMethod]
        public void TestSilhouette()
        {
            //Arrange
            Point rayOrigin = new Point(0, 0, -5);
            
            double wallZ = 10;
            double wallSize = 7;

            int canvasPixels = 200;
            double pixelSize = wallSize / canvasPixels;
            double half = wallSize / 2;


            Canvas canvas = new Canvas(canvasPixels, canvasPixels);
            Color color = new Color(1, 1, 1);
            Shape s = new Sphere();
            Material m = new Material(new Color(0.6, 0.2, 1));
            s.Material = m;

            Transformation t1 = new Transformation();
            t1.Scaling(0.3, 0.3, 0.3);
            Transformation t2 = new Transformation();
            t2.RotationZ(Math.PI/4);
            Transformation t3 = new Transformation();
            t3.Shear(0.5,0,0,0,0,0);
            Transformation t = t3 * t2;

            //s.Transform = t;

            Light l = new Light(new Point(-50, 10, -10), new Color(1, 1, 1));

            for (int y = 0; y < canvasPixels; y++)
            {
                double worldY = half - pixelSize * y;
                for (int x = 0; x < canvasPixels; x++)
                {
                    double worldX = -half + pixelSize * x;
                    Point positionOnWall = new Point(worldX, worldY, wallZ);
                    Vector v = Subtract(positionOnWall, rayOrigin);
                    Ray r = new Ray(rayOrigin, Normalize(v));
                    Intersections xs = new Intersections();
                    s.Intersect(xs, r);

                    if (xs.Hit() != null)
                    {
                        Point position = Ray.Position(r, xs.IntersectionList[0].Tick);
                        Vector normalV = s.NormalAt(position);
                        Vector eyeV = r.Direction * -1;
                        Lighting lighting =  new Lighting(xs.IntersectionList[0].Shape.Material, xs.IntersectionList[0].Shape, l, position, eyeV, normalV);
                        canvas.Pixels[x, y] = new Pixel(x, y, lighting.Result);
                    }

                }
            } 
           
            canvas.WritePpm();

            //Assert
        }

        [TestMethod]
        public void TestWorld()
        {
            Pattern p1 = new StripePattern( new Color( 0, 0, 0 ), new Color( 1, 1, 1 ), false );
            p1.Transform.RotationY( Math.PI / 2 );
            Pattern p2 = new StripePattern( new Color( 0, 0, 0 ), new Color( 1, 1, 1 ), false );

            Shape floor = new Plane
            {
                Material = new Material
                {
                    Pattern = new StripePattern( p1, p2, false ),
                    //MatColor = new Color( 1, 0.9, 0.9 ),
                    Specular = 0
                }
            };
            //floor.Transform.Scaling(10, 0.01, 10);

            Transformation t1 = new Transformation();
            t1.Translation(0, 0, 5);
            Transformation t2 = new Transformation();
            t2.RotationY(-Math.PI/4);
            Transformation t3 = new Transformation();
            t3.RotationX(Math.PI/2);
            Transformation t4 = new Transformation();
            t4.Scaling(10, 0.01, 10);
            Transformation t5 = new Transformation();
            t5.Shear(0.6,0.5,0.2,0.4,0.5,0.9);

            Shape leftWall = new Plane();
            leftWall.SetTransform( t1 * t2 * t3 * t4 );
            leftWall.Material = floor.Material;
            leftWall.Material.MatColor = new Color( 0.563, 0.789, 0.4 );

            t2.RotationY(Math.PI/4);

            Shape rightWall = new Plane();
            rightWall.SetTransform(t1 * t2 * t3 * t4);
            rightWall.Material = floor.Material;
            rightWall.Material.MatColor = new Color( 0.823, 0.789, 0.4 );

            Shape middle = Sphere.GlassSphere();//new Sphere();
            middle.Transform.Translation(-0.5, 1, 0.5);
            middle.Material.Transparency = 1;
            middle.Material.RefractiveIndex = 2;
            middle.Material.Reflective = 1;
            //middle.Material = new Material
            //{
            //    Pattern = new SolidPattern( new Color( 0.7529, 0.7529, 0.7529 ), true ),
            //    //MatColor = new Color(0.749, 0.749, 0.749),
            //    Diffuse = 0.9,
            //    Specular = 0.9,
            //    Reflective = 0.5,
            //    RefractiveIndex = 1.5
            //};
            //middle.Material.Pattern.Transform.Scaling( 0.25, 0.25, 0.25 );
            //middle.Material.Pattern.Transform.RotationY( Math.PI / 4 );

            //t1.Translation( -1, 0, 0 );
            //t4.Scaling( 0.5, 0.5, 0.5 );
            //middle.Material.Pattern.SetTransform( t1 * t4 );

            t1.Translation(1.5, 0.5, -0.5);
            t4.Scaling(0.5, 0.5, 0.5);

            Shape right = new Sphere();
            right.SetTransform(t1*t4);
            right.Material = new Material
            {
                //Pattern = new GradientPattern( new Color( 0.7529, 0.7529, 0.7529 ), new Color( 0.7529, 0.7529, 0.7529 ), false ),
                MatColor = new Color( 0.2, 0, 0 ),
                Diffuse = 0.1,
                Specular = 0.3,
                Reflective = 0.75,
                Transparency = 1
            };
            //right.Material.Pattern.Transform.RotationY( 2 * Math.PI / 3 );

            t1.Translation(-1.5, 0.33, -0.75);
            t4.Scaling(0.33, 0.33, 0.33);

            Shape left = new Sphere();
            left.SetTransform(t1*t4);
            left.Material = new Material
            {
                MatColor = new Color( 1, 1, 0 ),
                Diffuse = 0.7,
                Specular = 0.3
            };

            List<Shape> shapes = new List<Shape>
            {
                floor,
                leftWall,
                rightWall,
                left,
                middle,
                right
            };
            
            Light light = new Light(new Point(0, -10, 10), new Color(1, 1, 1));

            World world = new World(shapes, light);

            Point from = new Point(0, -5, 5);
            Point to = new Point(0, 0, 0);
            Vector up = new Vector(0, 1, 0);

            Camera camera = new Camera(100, 50, Math.PI / 3)
            {
                Transform = Transformation.ViewTransform(from, to, up)
            };

            Canvas canvas = camera.Render(world);
            canvas.WritePpm();
        }
    }
}
