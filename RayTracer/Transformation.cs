using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F = RayTracer.Functions;

namespace RayTracer
{
    public class Transformation
    {
        public Matrix TMatrix { get; set; }

        public Transformation()
        {
            TMatrix = Matrix.Identity(4);
        }

        public Transformation(Matrix m)
        {
            TMatrix = m;
        }

        public Transformation(TransformationType transformationType, string parameters)
        {
            TMatrix = Matrix.Identity( 4 );

            try
            { 
                double[] args = parameters.Split( ',' ).Select(x => Convert.ToDouble(x)).ToArray();

                switch ( transformationType )
                {
                    case TransformationType.RotationX:
                        if ( args.Length >= 1 )
                            RotationX( args[0] );
                        break;
                    case TransformationType.RotationY:
                        if ( args.Length >= 1 )
                            RotationY( args[0] );
                        break;
                    case TransformationType.RotationZ:
                        if ( args.Length >= 1 )
                            RotationZ( args[0] );
                        break;
                    case TransformationType.Scaling:
                        if ( args.Length >= 3 )
                            Scaling( args[0] , args[1] , args[2] );
                        break;
                    case TransformationType.Shear:
                        if ( args.Length >= 6 )
                            Shear( args[0] , args[1] , args[2] , args[3] , args[4] , args[5] );
                        break;
                    case TransformationType.Translation:
                        if ( args.Length >= 3 )
                            Translation( args[0] , args[1] , args[2] );
                        break;
                }
            }
            catch { }
            
        }

        public void Translation(double x, double y, double z)
        {
            TMatrix.MArray[0, 3] = x;
            TMatrix.MArray[1, 3] = y;
            TMatrix.MArray[2, 3] = z;
        }

        public void Scaling(double x, double y, double z)
        {
            TMatrix.MArray[0, 0] = x;
            TMatrix.MArray[1, 1] = y;
            TMatrix.MArray[2, 2] = z;
        }

        public void RotationX(double radians)
        {
            TMatrix.MArray[1, 1] = Math.Cos(radians);
            TMatrix.MArray[1, 2] = -Math.Sin(radians);
            TMatrix.MArray[2, 1] = Math.Sin(radians);
            TMatrix.MArray[2, 2] = Math.Cos(radians);
        }

        public void RotationY(double radians)
        {
            TMatrix.MArray[0, 0] = Math.Cos(radians);
            TMatrix.MArray[0, 2] = Math.Sin(radians);
            TMatrix.MArray[2, 0] = -Math.Sin(radians);
            TMatrix.MArray[2, 2] = Math.Cos(radians);
        }

        public void RotationZ(double radians)
        {
            TMatrix.MArray[0, 0] = Math.Cos(radians);
            TMatrix.MArray[0, 1] = -Math.Sin(radians);
            TMatrix.MArray[1, 0] = Math.Sin(radians);
            TMatrix.MArray[1, 1] = Math.Cos(radians);
        }

        public void Shear(double xy, double xz, double yx, double yz, double zx, double zy)
        {
            TMatrix.MArray[0, 1] = xy;
            TMatrix.MArray[0, 2] = xz;
            TMatrix.MArray[1, 0] = yx;
            TMatrix.MArray[1, 2] = yz;
            TMatrix.MArray[2, 0] = zx;
            TMatrix.MArray[2, 1] = zy;
        }

        public Transformation Inverse()
        {
            Transformation t = new Transformation
            {
                TMatrix = this.TMatrix.Inverse()
            };
            return t;
        }

        public Transformation Transpose()
        {
            Transformation t = new Transformation
            {
                TMatrix = this.TMatrix.Transpose()
            };
            return t;
        }

        public static RTuple operator *(Transformation tr, RTuple t)
        {
            return F.Multiply(tr.TMatrix, t);
        }

        public static Transformation operator *(Transformation tr1, Transformation tr2)
        {
            return new Transformation(F.Multiply(tr1.TMatrix, tr2.TMatrix));
        }

        public bool IsEqual(Transformation t)
        {
            return F.IsEqual(TMatrix, t.TMatrix);
        }

        public Transformation Multiply(Transformation t)
        {
            return new Transformation(F.Multiply(this.TMatrix, t.TMatrix));
        }

        public static Transformation ViewTransform(Point from, Point to, Vector up)
        {
            Vector forward = Vector.Normalize(to - from);
            Vector upN = Vector.Normalize(up);
            Vector left = forward.Cross(upN);
            Vector trueUp = left.Cross(forward);

            Matrix m = new Matrix(4,4);
            m.MArray[0,0] = left.X;
            m.MArray[0,1] = left.Y;
            m.MArray[0,2] = left.Z;
            m.MArray[0,3] = 0;

            m.MArray[1,0] = trueUp.X;
            m.MArray[1,1] = trueUp.Y;
            m.MArray[1,2] = trueUp.Z;
            m.MArray[1,3] = 0;

            m.MArray[2,0] = -forward.X;
            m.MArray[2,1] = -forward.Y;
            m.MArray[2,2] = -forward.Z;
            m.MArray[2,3] = 0;

            m.MArray[3,0] = 0;
            m.MArray[3,1] = 0;
            m.MArray[3,2] = 0;
            m.MArray[3,3] = 1;

            Transformation orientation = new Transformation(m);
            Transformation translation = new Transformation();
            translation.Translation(-from.X, -from.Y, -from.Z);
            
            return orientation * translation;
        }
    }
}
