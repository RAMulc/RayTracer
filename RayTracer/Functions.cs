using System;

namespace RayTracer
{
    public static class Functions
    {
        public static readonly double Epsilon = 0.00001;

        public static bool IsEqual(double a, double b)
        {
            if (Math.Abs(a - b) < Epsilon || (double.IsInfinity(a) && double.IsInfinity(b)))
                return true;
            else
                return false;
        }

        public static bool IsEqual(RTuple a, RTuple b)
        {
            if (IsEqual(a.X, b.X) && 
                IsEqual(a.Y, b.Y) && 
                IsEqual(a.Z, b.Z))
                return true;
            else
                return false;
        }

        public static bool IsEqual(Matrix a, Matrix b)
        {
            if (a.Rows == b.Rows && a.Cols == b.Cols)
            {
                for (int r = 0; r < a.Rows; r++)
                    for (int c = 0; c < a.Cols; c++)
                    {
                        if (!IsEqual(a.MArray[r, c], b.MArray[r, c]))
                            return false;
                    }

                return true;
            }

            return false;
        }

        //public static bool IsEqual(Point a, Point b)
        //{
        //    if (IsEqual(a.X, b.X) && 
        //        IsEqual(a.Y, b.Y) && 
        //        IsEqual(a.Z, b.Z))
        //        return true;
        //    else
        //        return false;
        //}

        //public static bool IsEqual(Vector a, Vector b)
        //{
        //    if (IsEqual(a.X, b.X) && 
        //        IsEqual(a.Y, b.Y) && 
        //        IsEqual(a.Z, b.Z))
        //        return true;
        //    else
        //        return false;
        //}

        public static RTuple Add(RTuple a, RTuple b)
        {
            RTuple rTuple = AddTuple(a, b);
            return rTuple;
        }

        public static Point Add(Point a, Point b)
        {
            RTuple rTuple = AddTuple(a, b);
            return new Point(rTuple.X, rTuple.Y, rTuple.Z);
        }

        public static Vector Add(Vector a, Vector b)
        {
            RTuple rTuple = AddTuple(a, b);
            return new Vector(rTuple.X, rTuple.Y, rTuple.Z);
        }

        public static Color Add(Color a, Color b)
        {
            RTuple rTuple = AddTuple(a, b);
            return new Color(rTuple.X, rTuple.Y, rTuple.Z);
        }

        private static RTuple AddTuple(RTuple a, RTuple b)
        {
            double x = a.X + b.X;
            double y = a.Y + b.Y;
            double z = a.Z + b.Z;
            double w = a.W + b.W;
            return new RTuple(x, y, z, w);
        }

        public static RTuple Subtract(RTuple a, RTuple b)
        {
            RTuple rTuple = SubtractTuple(a, b);
            return rTuple;
        }

        public static Vector Subtract(Point a, Point b)
        {
            RTuple rTuple = SubtractTuple(a, b);
            return new Vector(rTuple.X, rTuple.Y, rTuple.Z);
        }

        public static Vector Subtract(Vector a, Vector b)
        {
            RTuple rTuple = SubtractTuple(a, b);
            return new Vector(rTuple.X, rTuple.Y, rTuple.Z);
        }

        public static Color Subtract(Color a, Color b)
        {
            RTuple rTuple = SubtractTuple(a, b);
            return new Color(rTuple.X, rTuple.Y, rTuple.Z);
        }

        private static RTuple SubtractTuple(RTuple a, RTuple b)
        {
            double x = a.X - b.X;
            double y = a.Y - b.Y;
            double z = a.Z - b.Z;
            double w = a.W - b.W;
            return new RTuple(x, y, z, w);
        }

        public static RTuple Negate(RTuple t)
        {
            double x = -1 * t.X;
            double y = -1 * t.Y;
            double z = -1 * t.Z;
            double w = -1 * t.W;

            return new RTuple(x, y, z, w);
        }

        public static Matrix Multiply(Matrix a, Matrix b)
        {
            if (a.Rows != 4 && b.Rows != 4 && a.Cols != 4 && b.Cols != 4)
                return new Matrix();

            Matrix result = new Matrix(4, 4);
            for (int r = 0; r < a.Rows; r++)
                for (int c = 0; c < a.Cols; c++)
                    result.MArray[r, c] = a.MArray[r, 0] * b.MArray[0, c] +
                                          a.MArray[r, 1] * b.MArray[1, c] +
                                          a.MArray[r, 2] * b.MArray[2, c] +
                                          a.MArray[r, 3] * b.MArray[3, c];

            return result;
        }

        public static RTuple Multiply(Matrix m, RTuple t)
        {
            if (m.Rows != 4 && m.Cols != 4)
                return new RTuple();

            return new RTuple
            {
                X = m.MArray[0, 0] * t.X + m.MArray[0, 1] * t.Y + m.MArray[0, 2] * t.Z + m.MArray[0, 3] * t.W,
                Y = m.MArray[1, 0] * t.X + m.MArray[1, 1] * t.Y + m.MArray[1, 2] * t.Z + m.MArray[1, 3] * t.W,
                Z = m.MArray[2, 0] * t.X + m.MArray[2, 1] * t.Y + m.MArray[2, 2] * t.Z + m.MArray[2, 3] * t.W,
                W = m.MArray[3, 0] * t.X + m.MArray[3, 1] * t.Y + m.MArray[3, 2] * t.Z + m.MArray[3, 3] * t.W
            };
        }

        public static RTuple Multiply(RTuple t, double s)
        {
            RTuple rTuple = MultiplyTuple(t, s);
            return rTuple;
        }

        public static Vector Multiply(Vector v, double s)
        {
            RTuple rTuple = MultiplyTuple(v, s);
            return new Vector(rTuple.X, rTuple.Y, rTuple.Z);
        }

        public static Color Multiply(Color c, double s)
        {
            RTuple rTuple = MultiplyTuple(c, s);
            return new Color(rTuple.X, rTuple.Y, rTuple.Z);
        }

        private static RTuple MultiplyTuple(RTuple t, double s)
        {
            double x = s * t.X;
            double y = s * t.Y;
            double z = s * t.Z;
            double w = s * t.W;
            return new RTuple(x, y, z, w);
        }

        public static RTuple Division(RTuple t, double d)
        {
            return Multiply(t, 1 / d);
        }

        public static Vector Normalize(Vector v)
        {
            return new Vector(v.X / v.Magnitude, v.Y / v.Magnitude, v.Z / v.Magnitude);
        }

        public static RTuple Normalize(RTuple a)
        {
            return Division(a, a.Magnitude);
        }

        public static double DotProduct(RTuple a, RTuple b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
        }

        public static Vector CrossProduct(Vector a, Vector b)
        {
            return new Vector(a.Y * b.Z - a.Z * b.Y, 
                                a.Z * b.X - a.X * b.Z, 
                                a.X * b.Y - a.Y * b.X);
        }

        public static Point Transform(Point p, Matrix m)
        {
            return new Point( m * p );
        }
    }
}
