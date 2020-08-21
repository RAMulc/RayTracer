using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayTracer.Tests
{
    [TestClass]
    public class GroupTesting
    {
        [TestMethod]
        public void TestGroup()
        {
            Transformation t1 = new Transformation();
            t1.Translation( 0, -1, 0 );
            Transformation t2 = new Transformation();
            t1.RotationY( Math.PI / 7 );

            Shape floor = new Plane();
            floor.SetTransform( t1 * t2 );
            floor.Material.Transparency = 1;
            floor.Material.Reflective = 1;
            floor.Material.RefractiveIndex = 1.5;
            floor.CastShadow = false;
            //floor.Material.MatColor = new Color( 0, 0, 0 );
            floor.Material.Pattern = new CheckerPattern( new Color( 1, 1, 1 ), new Color( 0.1, 0.1, 0.1 ), false );

            //Shape t = new Triangle( new Point( 0, 0, 3 ), new Point( 2, 0, 8 ), new Point( 0, 1, 2 ) );
            //t.Material.Transparency = 0.5;
            //t.Material.Reflective = 1;
            //t.CastShadow = true;
            //t.Material.MatColor = new Color( 1, 0, 0 );

            Transformation t7 = new Transformation();
            Transformation t8 = new Transformation();
            t8.Scaling( 0.33, 0.33, 0.33 );

            Group g = new Group();

            for ( int x = 0; x < 2; x++ )
            {
                for (int y = 0; y < 2; y++ )
                {
                    for (int z = 0; z < 2; z++ )
                    {
                        t7.Translation( x, y, z );
                        Shape ball = new Sphere();
                        ball.SetTransform( t7 * t8 );
                        ball.Material.MatColor = new Color( 1, 0, 0 );
                        //ball.Material.Pattern = new GradientPattern( new Color( 0, 0, 0 ), new Color( 1, 0, 0 ), false );
                        ball.Material.Ambient = 0.1;
                        ball.Material.Diffuse = 0.1;
                        ball.Material.Reflective = 1;
                        ball.Material.Transparency = 1;
                        ball.Material.RefractiveIndex = 3.5;
                        ball.CastShadow = true;
                        g.AddChild( ball );
                    }
                }
            }
            g.Divide( 4 );

            List<Shape> shapes = new List<Shape>
            {
                floor,
                g
            };

            Light light = new Light( new Point( 0, 10, 10 ), new Color( 1, 1, 1 ) );

            World world = new World( shapes, light );

            Point from = new Point( 0, 4, 10 );
            Point to = new Point( 2, 1, 0 );
            Vector up = new Vector( 0, 1, 0 );

            Camera camera = new Camera( 200, 150, Math.PI / 3 )

            {
                Transform = Transformation.ViewTransform( from, to, up )
            };

            Canvas canvas = camera.Render( world, 3 );
            canvas.WritePpm();
        }

        [TestMethod]
        public void TestObj()
        {
            Transformation t1 = new Transformation();
            t1.Translation( 0, -1, 0 );
            Transformation t2 = new Transformation();
            t1.RotationY( Math.PI / 7 );

            Shape floor = new Plane();
            floor.SetTransform( t1 * t2 );
            floor.Material.Transparency = 1;
            floor.Material.Reflective = 1;
            floor.Material.RefractiveIndex = 1.5;
            floor.CastShadow = false;
            //floor.Material.MatColor = new Color( 0, 0, 0 );
            floor.Material.Pattern = new CheckerPattern( new Color( 1, 1, 1 ), new Color( 0.1, 0.1, 0.1 ), false );

            Transformation t7 = new Transformation();
            Transformation t8 = new Transformation();
            t8.Scaling( 0.33, 0.33, 0.33 );

            ReadObj rObj = new ReadObj( @"E:\CSharp\RayTracer\obj\LowResTeapot.obj" );

            Group g = rObj.ObjGroup;
            //g.Divide( 1000 );

            List<Shape> shapes = new List<Shape>
            {
                floor,
                g
            };

            Light light = new Light( new Point( 0, 10, 10 ), new Color( 1, 1, 1 ) );

            World world = new World( shapes, light );

            Point from = new Point( 0, 4, 10 );
            Point to = new Point( 2, 1, 0 );
            Vector up = new Vector( 0, 1, 0 );

            Camera camera = new Camera( 250, 150, Math.PI / 3 )

            {
                Transform = Transformation.ViewTransform( from, to, up )
            };

            Canvas canvas = camera.Render( world, 3 );
            canvas.WritePpm();
        }
    }
}
