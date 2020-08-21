using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class World
    {
        public List<Shape> Shapes { get; set; }
        public Light Light { get; set; }

        public World()
        {
            Light = new Light(new Point(-10, 10, -10), new Color(1, 1, 1));
            Shape s1 = new Sphere();
            s1.Material.MatColor = new Color(0.8, 1.0, 0.6);
            s1.Material.Diffuse = 0.7;
            s1.Material.Specular = 0.2;
            Shape s2 = new Sphere();
            s2.Transform.Scaling(0.5,0.5,0.5);
            Shapes = new List<Shape>{s1,s2};
        }

        public World(List<Shape> shapes, Light light)
        {
            Shapes = shapes;
            Light = light;
        }

        public Intersections IntersectWorld(Intersections intersections, Ray ray)
        {
            //Intersections intersections = new Intersections();
            foreach (Shape shape in Shapes)
            {
                intersections = shape.Intersect(intersections, ray);
            }

            return intersections;
        }

        public Intersections IntersectWorld(Ray ray)
        {
            Intersections intersections = new Intersections();
            foreach (Shape shape in Shapes)
            {
                intersections = shape.Intersect(intersections, ray);
            }

            return intersections;
        }

        public Lighting ShadeHit(Computations computations, int remaining = 5)
        {
            bool shadowed = this.IsShadowed(computations.OverPoint());

            Lighting surface = new Lighting(computations.Shape.Material,
                                computations.Shape,
                                this.Light, 
                                computations.OverPoint(), 
                                computations.EyeVector,
                                computations.NormalVector, shadowed);

            Color reflectedColor = ReflectedColor( computations, remaining );
            Color refractedColor = RefractedColor( computations, remaining );

            Material m = computations.Shape.Material;
            if (m.Reflective > 0 && m.Transparency > 0 )
            {
                double reflectance = Intersections.Schlick( computations );
                surface.Result = surface.Result + reflectedColor * reflectance + refractedColor * ( 1 - reflectance );
                return surface;
            }

            surface.Result = surface.Result + reflectedColor + refractedColor;
            return surface;
        }

        public Color ColorAt(Ray ray, int remaining)
        {
            Intersections intersections = new Intersections();
            intersections = this.IntersectWorld(intersections, ray);
            Intersection intersection = intersections.Hit();
            Computations c;
            if (intersection == null)
                return new Color(0,0,0);
            else
                c = intersections.PrepareComputations(intersection, ray, intersections);
            return ShadeHit(c, remaining).Result;
        }

        public bool IsShadowed(Point point)
        {
            Vector v = this.Light.Position - point;
            double distance = v.Magnitude;
            Vector direction = Vector.Normalize(v);

            Ray r = new Ray(point, direction);
            Intersections xs = IntersectWorld(r);

            Intersection h = xs.Hit();
            if (h != null && h.Tick < distance && h.Shape.CastShadow)
                return true;
            return false;
        }

        public Color ReflectedColor(Computations c, int remaining)
        {
            if ( c.Shape.Material.Reflective == 0 || remaining <= 0)
                return new Color( 0, 0, 0 );
            Ray reflectRay = new Ray( c.OverPoint(), c.ReflectV );
            Color color = this.ColorAt( reflectRay, remaining - 1 );
            return color * c.Shape.Material.Reflective;
        }

        public Color RefractedColor(Computations c, int remaining)
        {
            if ( c.Shape.Material.Transparency == 0 || remaining <= 0 )
                return new Color( 0, 0, 0 );

            double nRatio = c.N1 / c.N2;
            double cosi = c.EyeVector.Dot( c.NormalVector );
            double sin2t = Math.Pow( nRatio, 2 ) * ( 1 - Math.Pow( cosi, 2 ) );

            if ( sin2t > 1 )
                return new Color( 0, 0, 0 );

            double cost = Math.Sqrt( 1 - sin2t );
            Vector direction = c.NormalVector * ( nRatio * cosi - cost ) - c.EyeVector * nRatio;
            Ray refractRay = new Ray( c.UnderPoint(), direction );

            Color color = this.ColorAt( refractRay, remaining - 1 ) * c.Shape.Material.Transparency;

            return color;
        }

    }
}
