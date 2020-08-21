using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F = RayTracer.Functions;

namespace RayTracer
{
    public class Matrix
    {
        public int Rows { get; }
        public int Cols { get; }
        public double[,] MArray { get; set; }
        public bool Invertible
        {
            get 
            { 
                if (Determinant(this) == 0)
                    return false;
                return true;
            }
        }

        public Matrix(){}
            
        public Matrix(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            MArray = new double[Rows, Cols];
        }

        public Matrix(string input, int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            MArray = new double[Rows, Cols];
            string[] arrayRows = input.Split('|');
            int r = 0;
            foreach (string aRow in arrayRows)
            {
                string[] arrayRow = aRow.Split(',');
                for (int c = 0; c < arrayRow.Length; c++)
                {
                    if (c == cols)
                        break;
                    MArray[r,c] = double.Parse(arrayRow[c]);
                }
                r++;
                if (r == rows)
                    break;
            }

        }

        public static Matrix Identity (int rank)
        {
            Matrix matrix = new Matrix(rank, rank);
            for (int r = 0; r < matrix.Rows; r++)
                matrix.MArray[r, r] = 1;

            return matrix;
        }

        public static Matrix Transpose(Matrix m)
        {
            if (m.Rows != m.Cols)
                return new Matrix();
            
            Matrix transpose = new Matrix(m.Rows, m.Cols);

            for (int r = 0; r < m.Rows ; r++)
                for (int c = 0; c < m.Cols; c++)
                    transpose.MArray[c, r] = m.MArray[r, c];

            return transpose;
        }

        public Matrix Transpose()
        {
            return Matrix.Transpose(this);
        }

        public static double DeterminantX2(Matrix m)
        {
            return m.MArray[0, 0] * m.MArray[1, 1] - m.MArray[0, 1] * m.MArray[1, 0];
        }

        public static Matrix SubMatrix(Matrix m, int row, int col)
        {
            Matrix matrix = new Matrix(m.Rows-1, m.Cols-1);
            int mr = 0;
            for (int r = 0; r < m.Rows; r++)
                if (r != row)
                {
                    int mc = 0;
                    for (int c = 0; c < m.Cols; c++)
                        if (c != col)
                        {
                            matrix.MArray[mr, mc] = m.MArray[r, c];
                            mc++;
                        }

                    mr++;
                }
            return matrix;
        }

        public static double Minor(Matrix m, int row, int col)
        {
            Matrix matrix = SubMatrix(m, row, col);
            if (matrix.Rows != 2)
                return Determinant(matrix);
            else
                return DeterminantX2(matrix);
        }

        public static double CoFactor(Matrix m, int row, int col)
        {
            double minor = Minor(m, row, col);
            Math.DivRem(row + col, 2, out int rem);
            if (rem == 1)
                return -1 * minor;
            return minor;
        }

        public static double Determinant(Matrix m)
        {
            double deter = 0;
            List<double> cofacs = new List<double>();
            if (m.Rows == 2)
                return DeterminantX2(m);
            else
            {
                for (int c = 0; c < m.Cols; c++)
                {
                    double cof = CoFactor(m, 0, c);
                    cofacs.Add(cof);
                    deter = deter + m.MArray[0, c] * cof;
                }
            }

            return deter;
        }

        public static Matrix Inverse(Matrix m)
        {
            if (!m.Invertible)
                return null;
            
            Matrix m2 = new Matrix(m.Rows, m.Cols);
            double det = Determinant(m);
            for (int r = 0; r < m.Rows; r++)
            {
                for (int c = 0; c < m.Cols; c++)
                {
                    double cofactor = CoFactor(m, r, c);
                    m2.MArray[c, r] = cofactor / det;
                }
            }

            return m2;
        }

        public Matrix Inverse()
        {
            return Matrix.Inverse(this);
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            return F.Multiply(m1, m2);
        }

        public static RTuple operator *(Matrix m, RTuple t)
        {
            return F.Multiply(m, t);
        }


        
        public Matrix Multiply(Matrix m)
        {
            return F.Multiply(this, m);
        }

        public RTuple Multiply(RTuple t)
        {
            return F.Multiply(this, t);
        }

        public bool IsEqual(Matrix m)
        {
            return F.IsEqual(this, m);
        }

    }
}
