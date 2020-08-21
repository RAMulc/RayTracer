using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Camera
    {
        public int HSize { get; set; }
        public int VSize { get; set; }
        public double FieldOfView { get; set; }
        public Transformation Transform { get; set; }

        public World CameraWorld { get; private set; }
        public int RayRecursion { get; private set; }
        public List<List<Color>> ColorAt { get; private set; }

        public double PixelSize
        {
            get
            {
                return HalfWidth * 2 / HSize;
            } 
        }
        public double HalfWidth 
        {
            get
            {
                double halfView = Math.Tan(FieldOfView/2);
                double aspectRatio = (double)HSize / (double)VSize;
                if (aspectRatio >=1)
                    return halfView;
                else
                    return halfView*aspectRatio;
            } 
        }
        public double HalfHeight
        {
            get
            {
                double halfView = Math.Tan(FieldOfView/2);
                double aspectRatio = (double)HSize / (double)VSize;
                if (aspectRatio >=1)
                    return halfView / aspectRatio;
                else
                    return halfView;
            } 
        }


        public Camera(int hSize, int vSize, double fieldOfView)
        {
            HSize = hSize;
            VSize = vSize;
            FieldOfView = fieldOfView;
            Transform = new Transformation(Matrix.Identity(4));
        }

        public Ray RayForPixel(int px, int py)
        {
            double xOffset = (px + 0.5) * PixelSize;
            double yOffset = (py + 0.5) * PixelSize;

            double worldX = HalfWidth - xOffset;
            double worldY = HalfHeight - yOffset;

            Point worldXYZ = new Point(worldX, worldY, -1);

            RTuple _pixel = this.Transform.Inverse() * worldXYZ;
            Point pixel = new Point(_pixel);
            RTuple _origin = this.Transform.Inverse() * new Point(0, 0, 0);
            Point origin = new Point(_origin);
            Vector direction = Vector.Normalize(pixel - origin);

            return new Ray(origin, direction);
        }

        public Canvas Render(World w, int rayRecursion = 5)
        {
            CameraWorld = w;
            RayRecursion = rayRecursion;
            ProcessRays();

            Canvas image = new Canvas(HSize, VSize);
            for (int y = 0; y < VSize; y++)
            {
                List<Color> xColor = ColorAt[y];
                for (int x = 0; x < HSize; x++)
                {
                    //Ray r = RayForPixel(x, y);
                    //Color c = w.ColorAt(r, rayRecursion);
                    //image.WritePixel(new Pixel(x, y, c));
                    image.WritePixel( new Pixel( x, y, xColor[x] ) );
                }
            }
            return image;
        }

        private void ProcessRays()
        {
            ColorAt = new List<List<Color>>();
            List<List<Ray>> rays = new List<List<Ray>>();
            
            for ( int y = 0; y < VSize; y++ )
            {
                List<Ray> xRays = new List<Ray>();
                List<Color> xColor = new List<Color>();
                for ( int x = 0; x < HSize; x++ )
                {
                    xRays.Add( RayForPixel( x, y ) );
                    xColor.Add( null );
                }
                rays.Add( xRays );
                ColorAt.Add( xColor );
            }

            Parallel.For( 0, VSize, y =>
            {
                List<Ray> xRays = rays[y];
                for ( int x = 0; x < HSize; x++ )
                {
                    ColorAt[y][x] = CameraWorld.ColorAt( xRays[x], RayRecursion );
                }
            } );
            return;
        }


    }

}
