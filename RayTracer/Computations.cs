using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F = RayTracer.Functions;

namespace RayTracer
{
    public class Computations
    {
        public double Tick { get; set; }
        public Shape Shape { get; set; }
        public Point Position { get; set; }
        public Vector EyeVector { get; set; }
        public Vector NormalVector { get; set; }
        public bool Inside { get; set; }
        public Vector ReflectV { get; set; }
        public double N1 { get; set; }
        public double N2 { get; set; }

        public Computations() { }

        public Computations(double tick, Shape shape, Point position, Vector eyeVector,  Vector normalVector)
        {
            Tick = tick;
            Shape = shape;
            Position = position;
            EyeVector = eyeVector;
            NormalVector = normalVector;
            HitInside();
        }

        public Computations(Intersection intersection, Ray ray)
        {
            //Computations c = new Computations();
            //Intersections xs = new Intersections();
            //c = xs.PrepareComputations(intersection, ray);
            this.Tick = intersection.Tick;
            this.Shape = intersection.Shape;
            this.Position = Ray.Position( ray, this.Tick );
            this.EyeVector = ray.Direction * -1;
            this.NormalVector = this.Shape.NormalAt( this.Position, intersection );
            this.HitInside();
            this.ReflectV = ReflectVector( ray.Direction, this.NormalVector );
        }

        public void HitInside()
        {
            Inside = false;
            if (NormalVector.Dot(EyeVector) < 0)
            {
                Inside = true;
                NormalVector = NormalVector * -1;
            }
        }

        public Point OverPoint()
        {
            RTuple r = Position + NormalVector * F.Epsilon;
            return new Point(r);
        }

        public Point UnderPoint()
        {
            RTuple r = Position + Vector.Negate( NormalVector ) * F.Epsilon;
            return new Point( r );
        }

        public Vector ReflectVector(Vector rayDirection, Vector normal)
        {
            return rayDirection.Reflect( normal );
        }
    }
}
