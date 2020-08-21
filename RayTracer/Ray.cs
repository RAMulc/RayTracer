using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RayTracer.Functions;

namespace RayTracer
{
    public class Ray
    {
        public Point Origin { get; set; }
        public Vector Direction { get; set; }

        public Ray(Point origin, Vector direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public static Point Position(Ray ray, double t)
        {
            return new Point(ray.Origin + (ray.Direction * t));
        }

        public Ray Transform(Transformation m)
        {
            Point p = new Point(m * Origin);
            Vector v = new Vector(m * Direction);

            return new Ray(p, v);
        }
    }
}
