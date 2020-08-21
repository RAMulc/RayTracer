using System.Collections.Generic;

namespace RayTracer
{
    public abstract class Shape
    {
        public abstract Point Origin { get; set; }
        public abstract int ID { get; }
        public abstract Ray SavedRay { get; set; }

        public BoundingBox ShapeBounds
        {
            get
            {
                return BoundsOf();
            }
        }

        public abstract Intersections LocalIntersect(Intersections xs, Ray ray);
        public abstract Vector LocalNormalAt(Point point, Intersection hit = null);
        public abstract BoundingBox BoundsOf();
        public abstract void Divide(double threshhold);

        public Transformation Transform { get; set; }
        public static int Count { get; private set; }
        public Material Material { get; set; }
        public bool CastShadow { get; set; }
        public Group Parent { get; set; }

        public virtual double Minimum { get; set; }
        public virtual double Maximum { get; set; }
        public virtual bool IsClosed { get; set; }

        public Shape()
        {
            Count++;
            Transform = new Transformation();
            Material = new Material();
            CastShadow = true;
        }

        public void SetTransform(Transformation t)
        {
            Transform = t;
        }

        public Intersections Intersect(Intersections xs, Ray ray)
        {
            Ray localRay = ray.Transform(this.Transform.Inverse());
            return LocalIntersect(xs, localRay);
        }

        public Vector NormalAt(Point point, Intersection hit = null)
        {
            //Point localPoint = new Point(this.Transform.Inverse() * point);
            Point localPoint = WorldToObject( this, point );
            Vector localNormal = LocalNormalAt( localPoint, hit );

            //Vector worldNormal = new Vector( this.Transform.Inverse().Transpose() * localNormal )
            //{
            //W = 0
            //};
            //worldNormal.Normalize();

            //return worldNormal;
            return NormalToWorld( this, localNormal );
        }

        public static Point WorldToObject(Shape s, Point p)
        {
            if ( s.Parent != null )
                p = WorldToObject( s.Parent, p );

            return new Point(Matrix.Inverse( s.Transform.TMatrix ).Multiply( p ));
        }

        public static Vector NormalToWorld(Shape s, Vector normal)
        {
            Vector n = new Vector( Matrix.Transpose( Matrix.Inverse( s.Transform.TMatrix ) ).Multiply( normal ) )
            {
                W = 0
            };
            n = Vector.Normalize( n );

            if ( s.Parent != null )
                n = NormalToWorld( s.Parent, n );

            return n;
        }

        public BoundingBox ParentSpaceBoundsOf()
        {
            BoundingBox b = new BoundingBox();
            b.AddPoint( Functions.Transform( this.ShapeBounds.Min, this.Transform.TMatrix ) );
            b.AddPoint( Functions.Transform( this.ShapeBounds.Max, this.Transform.TMatrix ) );
            return b;
        }

    }
}
