using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayTracer.Tests
{
    [TestClass]
    public class RefractionTest
    {
        [TestMethod]
        public void TestRefractionWorld()
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
            floor.Material.Pattern = new CheckerPattern( new Color( 1, 1, 1 ), new Color( 0.1, 0.1, 0.1 ), true );

            Transformation t3 = new Transformation();
            t3.Translation( -1, 0, 1 );
            Transformation t4 = new Transformation();
            t4.Scaling( 0.25, 0.25, 0.25 );

            Shape ball = Sphere.GlassSphere();
            //Shape ball = new Cylinder(1, 2);
            ball.SetTransform( t3 * t4 );
            ball.Material.MatColor = new Color( 0, 0, 0 );
            ball.Material.Ambient = 0.0;
            ball.Material.Diffuse = 0.0;
            ball.Material.Reflective = 1;
            ball.Material.Transparency = 1;
            ball.Material.RefractiveIndex = 3.5;
            ball.CastShadow = true;

            Transformation t5 = new Transformation();
            t5.RotationX( Math.PI / 3 );
            Transformation t6 = new Transformation();
            t6.RotationZ( Math.PI / 4 );
            Transformation t7 = new Transformation();
            t7.Translation( 0, 1, -5 );
            Transformation t8 = new Transformation();
            t8.Scaling( 0.25, 0.25, 0.5 );

            //Shape redball = new Cube();
            Shape redball = new Cone();
            redball.Maximum = 0;
            redball.Minimum = -2;
            redball.SetTransform( t7 * t5 * t6 * t8);
            //redball.Material.MatColor = new Color( 1, 0, 0 );
            redball.Material.Pattern = new GradientPattern( new Color( 0, 0, 0 ), new Color( 1, 0, 0 ), false );
            redball.Material.Ambient = 0.1;
            redball.Material.Diffuse = 0.1;
            redball.Material.Reflective = 1;
            redball.Material.Transparency = 0;
            redball.Material.RefractiveIndex = 3.5;
            redball.CastShadow = true;

            Shape greenball = Sphere.GlassSphere();
            //Shape greenball = new Cylinder(1, 3);
            //greenball.IsClosed = true;
            greenball.Transform.Translation( -5, 1, -8 );
            greenball.Material.MatColor = new Color( 0, 1, 0 );
            greenball.Material.Ambient = 0.2;
            greenball.Material.Diffuse = 0.1;
            greenball.Material.Reflective = 1;
            greenball.Material.Transparency = 1;
            greenball.Material.RefractiveIndex = 3.5;
            greenball.CastShadow = true;

            //Shape blueball = Sphere.GlassSphere();
            Shape blueball = new Cone();
            blueball.Minimum = 1;
            blueball.Maximum = 4;
            blueball.Transform.Translation( 5, 1, -10 );
            blueball.Material.MatColor = new Color( 0, 0, 1 );
            blueball.Material.Ambient = 0.2;
            blueball.Material.Diffuse = 0.1;
            blueball.Material.Reflective = 1;
            blueball.Material.Transparency = 1;
            blueball.Material.RefractiveIndex = 3.5;
            blueball.CastShadow = true;

            Group g = new Group();
            g.AddChild( ball );
            g.AddChild( redball );

            List<Shape> shapes = new List<Shape>
            {
                floor,
                ball,
                redball,
                greenball,
                blueball
            };

            Light light = new Light( new Point( 0, 10, -10 ), new Color( 1, 1, 1 ) );

            World world = new World( shapes, light );

            Point from = new Point( 0, 1, 4 );
            Point to = new Point( 0, 1, 0 );
            Vector up = new Vector( 0, 1, 0 );

            Camera camera = new Camera( 150, 100, Math.PI / 3 )
            {
                Transform = Transformation.ViewTransform( from, to, up )
            };

            Canvas canvas = camera.Render( world );
            canvas.WritePpm();
        }

        [TestMethod]
        public void TestRefractionWorld2()
        {
            Shape floor = new Plane();
            floor.Material.Pattern = new CheckerPattern( new Color( 0, 0, 0 ), new Color( 1, 1, 1 ), false );
            floor.Material.Pattern.Transform.Scaling( 0.25, 0.25, 0.25 );

            Transformation t1 = new Transformation();
            t1.RotationX( Math.PI / 2 );
            Transformation t2 = new Transformation();
            t2.RotationY( -Math.PI / 4 );
            Transformation t3 = new Transformation();
            t3.Translation( 0, 0, 5 );

            Shape leftWall = new Plane();
            leftWall.SetTransform( t3 * t2 * t1 );
            leftWall.Material = new Material( new Color( 0.8, 0.1, 0.1 ) );
            leftWall.Material.Reflective = 1;
            leftWall.Material.Specular = 1;
            leftWall.Material.Shininess = 300;

            t1.RotationX( Math.PI / 2 );
            t2.RotationY( Math.PI / 4 );
            t3.Translation( 0, 0, 5 );

            Shape rightWall = new Plane();
            rightWall.SetTransform( t3 * t2 * t1 );
            rightWall.Material = new Material( new Color( 0.1, 0.2, 0.1 ) );
            rightWall.Material.Reflective = 1;
            rightWall.Material.Specular = 1;
            rightWall.Material.Shininess = 100;
            //rightWall.Material.Transparency = 0.02;

            Shape glassBall = Sphere.GlassSphere();
            glassBall.Material.MatColor = new Color( 0.2, 0.0, 0.0 );
            glassBall.Material.Ambient = 0.0;
            glassBall.Material.Diffuse = 0.0;
            glassBall.Material.Reflective = 0.75;
            glassBall.Material.Transparency = 0.75;
            glassBall.Transform.Translation( -1, 1, -1 );


            Shape ball0 = new Sphere();
            ball0.Material.MatColor = new Color( 1, 1, 1 );
            ball0.Transform.Translation( 0, 1, 0 );
            ball0.Transform.Scaling( 0.25, 0.25, 0.25 );
            ball0.Material.Diffuse = 0.7;
            ball0.Material.Specular = 0.3;

            Shape ball1 = new Sphere();
            ball1.Material.MatColor = new Color( 1, 0.0, 0.0 );
            ball1.Transform.Translation( 2, 1, 0 );
            ball1.Transform.Scaling( 0.5, 0.5, 0.5 );
            ball1.Material.Diffuse = 0.7;
            ball1.Material.Specular = 0.3;

            Shape ball2 = new Sphere();
            ball2.Material.MatColor = new Color( 0, 1, 0.0 );
            ball2.Transform.Translation( 0, 2, 0 );
            ball2.Transform.Scaling( 0.25, 0.25, 0.25 );
            ball2.Material.Diffuse = 0.7;
            ball2.Material.Specular = 0.3;

            Shape ball3 = new Sphere();
            ball3.Material.MatColor = new Color( 0, 0.0, 1 );
            ball3.Transform.Translation( 0, 1, 2 );
            ball3.Transform.Scaling( 0.5, 0.5, 0.5 );
            ball3.Material.Diffuse = 0.7;
            ball3.Material.Specular = 0.3;

            Group g1 = new Group();
            g1.AddChild( glassBall );
            g1.AddChild( ball0 );
            g1.AddChild( ball1 );
            
            Group g2 = new Group();
            g2.AddChild( ball2 );
            g2.AddChild( ball3 );

            List<Shape> shapes = new List<Shape>
            {
                floor,
                leftWall,
                rightWall,
                //glassBall,
                g1,
                g2
                //ball0,
                //ball1,
                //ball2,
                //ball3
            };

            Light light = new Light( new Point( -10, 20, -10 ), new Color( 1, 1, 1 ) );

            World world = new World( shapes, light );

            Point from = new Point( -5, 5, -7 );
            Point to = new Point( 0, 1, 0 );
            Vector up = new Vector( 0, 1, 0 );

            Camera camera = new Camera( 250, 150, Math.PI / 3 )
            {
                Transform = Transformation.ViewTransform( from, to, up )
            };

            Canvas canvas = camera.Render( world );
            canvas.WritePpm();
        }
    }
}
