using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Group : Shape
    {
        public override Point Origin { get; set; }
        public override int ID { get; }
        public override Ray SavedRay { get; set; }

        public List<Shape> Children { get; set; }
        public List<Shape> Left { get; set; }
        public List<Shape> Right { get; set; }

        public BoundingBox BoundingBoxLeft { get; set; }
        public BoundingBox BoundingBoxRight { get; set; }

        public Group()
        {
            Children = new List<Shape>();
            Left = new List<Shape>();
            Right = new List<Shape>();
        }

        public override Intersections LocalIntersect(Intersections xs, Ray ray)
        {
            List<double> t = new List<double>();
            if (BoundingBox.Intersects(this.BoundsOf(), ray, ref t ))
            {
                foreach ( Shape s in Children )
                    xs = s.Intersect( xs, ray );
            }
            return xs;
        }

        public override Vector LocalNormalAt(Point point, Intersection hit)
        {
            throw new NotImplementedException();
        }

        public void AddChild(Shape s)
        {
            Children.Add( s );
            s.Parent = this;
        }

        public override BoundingBox BoundsOf()
        {
            BoundingBox b = new BoundingBox();
            foreach ( Shape s in Children )
            {
                BoundingBox c = s.ParentSpaceBoundsOf();
                b.AddBoundingBox( c );
            }
            return b;
        }

        private Point MinPoint(Point p1, Point p2)
        {
            return new Point( Math.Min( p1.X, p2.X ), Math.Min( p1.Y, p2.Y ), Math.Min( p1.Z, p2.Z ) );
        }

        private Point MaxPoint(Point p1, Point p2)
        {
            return new Point( Math.Max( p1.X, p2.X ), Math.Max( p1.Y, p2.Y ), Math.Max( p1.Z, p2.Z ) );
        }

        public void PartitionChildren()
        {
            Group g = new Group();

            BoundingBox bBL = this.BoundsOf().Left;
            BoundingBox bBR = this.BoundsOf().Right;

            foreach (Shape child in Children )
            {
                if ( bBL.ContainsBoundingBox( child.ParentSpaceBoundsOf() ) )
                {
                    Left.Add( child );
                    bBL.AddBoundingBox( child.ParentSpaceBoundsOf() );
                }
                else if ( bBR.ContainsBoundingBox( child.ParentSpaceBoundsOf() ) )
                {
                    Right.Add( child );
                    bBR.AddBoundingBox( child.ParentSpaceBoundsOf() );
                }
                //if ( this.BoundsOf().Left.ContainsBoundingBox( child.ParentSpaceBoundsOf() ) )
                //Left.Add( child );
                //else if ( this.BoundsOf().Right.ContainsBoundingBox( child.ParentSpaceBoundsOf() ) )
                //Right.Add( child );
            }

            foreach ( Shape s in Left )
                Children.Remove( s );
            foreach ( Shape s in Right )
                Children.Remove( s );
        }

        public void SubGroup(List<Shape> shapes)
        {
            Group g = new Group();
            foreach (Shape s in shapes )
            {
                g.AddChild( s );
            }
            this.AddChild( g );
        }

        public override void Divide(double threshhold)
        {
            //return;
            if (threshhold < this.Children.Count )
            {
                this.PartitionChildren();
            }
            if ( Left.Count > 0 )
                SubGroup( Left );
            if ( Right.Count > 0 )
                SubGroup( Right );

            foreach (Shape child in Children )
            {
                child.Divide( threshhold );
            }
        }
    }
}
