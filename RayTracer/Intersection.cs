using System;
using System.Collections.Generic;

namespace RayTracer
{
    public class Intersection : IComparable<Intersection>
    {
        public double Tick { get; set; }
        public Shape Shape { get; set; }
        public double U { get; set; }
        public double V { get; set; }

        public Intersection(){}
        
        public Intersection(double t, Shape s)
        {
            Tick = t;
            Shape = s;
        }

        public int CompareTo(Intersection intersection)
        {
            if (intersection == null)
                return 1;
            
            else
                return this.Tick.CompareTo(intersection.Tick);
        }

        public bool IsEqual(Intersection i)
        {
            if ( this.Tick == i.Tick && this.Shape == i.Shape )
                return true;
            return false;
        }

        public static Intersection IntersectionWithUV(double t, Shape s, double u, double v)
        {
            Intersection i = new Intersection( t, s ) { U = u, V = v };
            return i;
        }
    }

    public class Intersections
    {
        public List<Intersection> IntersectionList;

        public Intersection this[int index]
        {
            get { return IntersectionList[index]; }
            set { IntersectionList[index] = value; }
        }

        public int Count
        {
            get { return IntersectionList.Count; }
        }

        public Intersections()
        {
            IntersectionList = new List<Intersection>();
        }

        public void Add(Intersection i)
        {
            IntersectionList.Add(i);
            IntersectionList.Sort();
        }

        public Intersection Hit()
        {
            for (int i = 0; i < IntersectionList.Count; i++)
            {
                if (IntersectionList[i].Tick >= 0)
                    return IntersectionList[i];
            }
            return null;
        }

        public Intersection Hit(Intersection intersection)
        {
            for ( int i = 0; i < IntersectionList.Count; i++ )
            {
                if ( IntersectionList[i].Tick >= 0 && intersection.Tick == IntersectionList[i].Tick )
                    return IntersectionList[i];
            }
            return null;
        }

        public static double Schlick(Computations comps)
        {
            double cos = comps.EyeVector.Dot( comps.NormalVector );
            if (comps.N1 > comps.N2 )
            {
                double n = comps.N1 / comps.N2;
                double sin2T = Math.Pow( n, 2 ) * ( 1 - Math.Pow( cos, 2 ) );
                if ( sin2T > 1 )
                    return 1;
                double cosT = Math.Sqrt( 1 - sin2T );
                cos = cosT;
            }

            double r0 = Math.Pow( ( comps.N1 - comps.N2 ) / ( comps.N1 + comps.N2 ), 2 );

            return r0 + ( 1 - r0 ) * Math.Pow( 1 - cos, 5 );
        }

        public Computations PrepareComputations(Intersection intersection, Ray ray, Intersections intersections)
        {
            Computations computations = new Computations( intersection, ray );

            List<Shape> containers = new List<Shape>();
            foreach (Intersection i in intersections.IntersectionList)
            {
                if ( i.IsEqual( intersection ) )
                {
                    if (containers.Count == 0 )
                        computations.N1 = 1;
                    else
                        computations.N1 = containers[containers.Count - 1].Material.RefractiveIndex;
                }

                if ( containers.Contains( i.Shape ) )
                    containers.Remove( i.Shape );
                else
                    containers.Add( i.Shape );

                if ( i.IsEqual( intersection ) )
                {
                    if ( containers.Count == 0 )
                        computations.N2 = 1;
                    else
                        computations.N2 = containers[containers.Count - 1].Material.RefractiveIndex;
                    return computations;
                }

            }

            return computations;
        }

    }

    
}
