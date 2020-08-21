using System;
using System.Collections.Generic;
using static RayTracer.Functions;

namespace RayTracer
{
    public class Sphere : Shape
    {
        public override Point Origin { get; set; }
        public override int ID { get; }
        public override Ray SavedRay { get; set; }

        public Sphere()
        {
            Origin = new Point(0, 0, 0);
            ID = Count;
        }

        public override Intersections LocalIntersect(Intersections xs, Ray ray)
        {
            Vector sphereToRay = ray.Origin - Origin;
            double a = ray.Direction.Dot(ray.Direction);
            double b = 2 * ray.Direction.Dot(sphereToRay);
            double c = sphereToRay.Dot(sphereToRay) - 1;

            double discriminant = Math.Pow(b, 2) - 4 * a * c;

            if (discriminant < 0)
                return xs;

            xs.Add(new Intersection((-b - Math.Sqrt(discriminant)) / (2 * a), this));
            xs.Add(new Intersection((-b + Math.Sqrt(discriminant)) / (2 * a), this));

            return xs;
        }

        public override Vector LocalNormalAt(Point point, Intersection hit)
        {
            Vector v = point - new Point(0, 0, 0);
            return v;
        }

        public static Sphere GlassSphere()
        {
            Sphere s = new Sphere();
            s.Material.Transparency = 1;
            s.Material.RefractiveIndex = 2.5;
            s.Material.Reflective = 1;

            return s;
        }

        public override BoundingBox BoundsOf()
        {
            BoundingBox b = new BoundingBox();
            b.AddPoint( new Point( -1, -1, -1 ) );
            b.AddPoint( new Point( 1, 1, 1 ) );
            return b;
        }

        public override void Divide(double threshhold)
        {
        }
    }
}
