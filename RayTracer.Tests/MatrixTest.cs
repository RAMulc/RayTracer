using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using F = RayTracer.Functions;

namespace RayTracer.Tests
{
    [TestClass]
    public class MatrixTest
    {
        [TestMethod]
        public void TestMatrix4x4()
        {
            Matrix m = new Matrix("1.1,2.2,3.3,0|4.4,5.5,6.6,0|7.7,8.8,9.9,0|1,2,3,4", 4, 4);

            for (int i =0; i < m.Rows; i++)
            {
                for (int c =0; c < m.Cols; c++)
                {
                    Console.Write("{0} ", m.MArray[i,c]);
                }
                
                Console.WriteLine();
            }
        }

        [TestMethod]
        public void TestMatrix2x2()
        {
            Matrix m = new Matrix("1.1,2.2|4.4,5.5", 2,2);

            for (int i =0; i < m.Rows; i++)
            {
                for (int c =0; c < m.Cols; c++)
                {
                    Console.Write("{0} ", m.MArray[i,c]);
                }
                
                Console.WriteLine();
            }
        }

        [TestMethod]
        public void TestMatrix3x3()
        {
            Matrix m = new Matrix("1.1,2.2,3|4.4,5.5,6.6|7.7,8.8,9.9", 3,3);

            for (int i =0; i < m.Rows; i++)
            {
                for (int c =0; c < m.Cols; c++)
                {
                    Console.Write("{0} ", m.MArray[i,c]);
                }
                
                Console.WriteLine();
            }
        }

        [TestMethod]
        public void TestMatrixAEqualB()
        {
            Matrix A = new Matrix("1,2,3,4|5,6,7,8|1,2,3,4|5,6,7,8", 4, 4);
            Matrix B = new Matrix("1,2,3,4|5,6,7,8|1,2,3,4|5,6,7,8", 4, 4);

            bool isEqual = A.IsEqual(B);

            Assert.AreEqual(isEqual, true);

        }

        [TestMethod]
        public void TestMatrixANotEqualB()
        {
            Matrix A = new Matrix("1,2,3,4|5,6,6,8|1,2,3,4|5,6,7,8", 4, 4);
            Matrix B = new Matrix("1,2,3,4|5,6,7,8|1,2,3,4|5,6,7,8", 4, 4);

            bool isEqual = A.IsEqual(B);

            Assert.AreEqual(isEqual, false);

        }

        [TestMethod]
        public void TestMatrixANotEqualB2()
        {
            Matrix A = new Matrix("1,2,3,4|5,6,6,8|1,2,3,4", 3, 4);
            Matrix B = new Matrix("1,2,3,4|5,6,7,8|1,2,3,4|5,6,7,8", 4, 4);

            bool isEqual = A.IsEqual(B);

            Assert.AreEqual(isEqual, false);

        }

        [TestMethod]
        public void TestMatrixMultiply()
        {
            Matrix A = new Matrix("1,2,3,4|5,6,7,8|9,8,7,6|5,4,3,2", 4, 4);
            Matrix B = new Matrix("-2,1,2,3|3,2,1,-1|4,3,6,5|1,2,7,8", 4, 4);

            Matrix C = new Matrix("20,22,50,48|44,54,114,108|40,58,110,102|16,26,46,42", 4, 4);

            bool isEqual = (A * B).IsEqual(C);

            Assert.AreEqual(isEqual, true);

        }

        [TestMethod]
        public void TestMatrixTupleMultiply()
        {
            Matrix A = new Matrix("1,2,3,4|2,4,4,2|8,6,4,1|0,0,0,1", 4, 4);
            RTuple T = new RTuple(1, 2, 3, 1);

            RTuple result = new RTuple(18, 24, 33, 1);

            bool isEqual = F.IsEqual(A * T, result);

            Assert.AreEqual(isEqual, true);

        }

        [TestMethod]
        public void TestIdentity4x4()
        {
            Matrix m = Matrix.Identity(4);

            for (int i =0; i < m.Rows; i++)
            {
                for (int c =0; c < m.Cols; c++)
                {
                    Console.Write("{0} ", m.MArray[i,c]);
                }
                
                Console.WriteLine();
            }
        }

        [TestMethod]
        public void TestMatrixMultiplyIdentity()
        {
            Matrix A = new Matrix("1,2,3,4|5,6,7,8|9,8,7,6|5,4,3,2", 4, 4);
            Matrix B = Matrix.Identity(4);


            bool isEqual = (A * B).IsEqual(A);

            Assert.AreEqual(isEqual, true);

        }

        [TestMethod]
        public void TestMatrixTranspose()
        {
            Matrix A = new Matrix("0,9,3,0|9,8,0,8|1,8,5,3|0,0,5,8", 4, 4);
            Matrix B = new Matrix("0,9,1,0|9,8,8,0|3,0,5,5|0,8,3,8", 4, 4);


            bool isEqual = A.Transpose().IsEqual(B);

            Assert.AreEqual(isEqual, true);

        }

        [TestMethod]
        public void TestMatrixIdentityTranspose()
        {
            Matrix A = Matrix.Identity(4);

            bool isEqual = Matrix.Transpose(A).IsEqual(A);

            Assert.AreEqual(isEqual, true);

        }

        [TestMethod]
        public void TestMatrixDeterminantX2()
        {
            Matrix A = new Matrix("1,5|-3,2", 2, 2);

            double d = Matrix.DeterminantX2(A);
            bool isEqual = F.IsEqual(d, 17);

            Assert.AreEqual(isEqual, true);

        }

        [TestMethod]
        public void TestSubMatrix3()
        {
            Matrix A = new Matrix("1,5,0|-3,2,7|0,6,-3", 3, 3);
            Matrix B = new Matrix("-3,2|0,6", 2, 2);

            Matrix C = Matrix.SubMatrix(A, 0, 2);
            bool isEqual = C.IsEqual(B);

            Assert.AreEqual(isEqual, true);

        }

        [TestMethod]
        public void TestMinor()
        {
            Matrix A = new Matrix("3,5,0|2,-1,-7|6,-1,5", 3, 3);

            double minor = Matrix.Minor(A, 1, 0);
            bool isEqual = F.IsEqual(minor, 25);

            Assert.AreEqual(isEqual, true);

        }

        [TestMethod]
        public void TestCofactor0()
        {
            Matrix A = new Matrix("3,5,0|2,-1,-7|6,-1,5", 3, 3);

            double cofactor = Matrix.CoFactor(A, 0, 0);
            bool isEqual = F.IsEqual(cofactor, -12);

            Assert.AreEqual(isEqual, true);

        }

        [TestMethod]
        public void TestCofactor1()
        {
            Matrix A = new Matrix("3,5,0|2,-1,-7|6,-1,5", 3, 3);

            double cofactor = Matrix.CoFactor(A, 1, 0);
            bool isEqual = F.IsEqual(cofactor, -25);

            Assert.AreEqual(isEqual, true);

        }

        [TestMethod]
        public void TestDeterminant3x3()
        {
            Matrix A = new Matrix("1,2,6|-5,8,-4|2,6,4", 3, 3);

            double determ = Matrix.Determinant(A);
            bool isEqual = F.IsEqual(determ, -196);

            Assert.AreEqual(isEqual, true);

        }

        [TestMethod]
        public void TestDeterminant4x4()
        {
            Matrix A = new Matrix("-2,-8,3,5|-3,1,7,3|1,2,-9,6|-6,7,7,-9", 4, 4);

            double determ = Matrix.Determinant(A);
            bool isEqual = F.IsEqual(determ, -4071);

            Assert.AreEqual(isEqual, true);

        }

        [TestMethod]
        public void TestIsIvertible()
        {
            Matrix A = new Matrix("6,4,4,4|5,5,7,6|4,-9,3,-7|9,1,7,-6", 4, 4);

            bool isInvertible = A.Invertible;

            Assert.AreEqual(isInvertible, true);
        }

        [TestMethod]
        public void TestIsNotIvertible()
        {
            Matrix A = new Matrix("-4,2,-2,-3|9,6,2,6|0,-5,1,-5|0,0,0,0", 4, 4);

            bool isInvertible = A.Invertible;

            Assert.AreEqual(isInvertible, false);
        }

        [TestMethod]
        public void TestMatrixInverse1()
        {
            Matrix A = new Matrix("-5,2,6,-8|1,-5,1,8|7,7,-6,-7|1,-3,7,4", 4, 4);
            Matrix B = new Matrix("0.21805,0.45113,0.24060,-0.04511|" +
                                  "-0.80827,-1.45677,-0.44361,0.52068|" +
                                  "-0.07895,-0.22368,-0.05263,0.19737|" +
                                  "-0.52256,-0.81391,-0.30075,0.30639", 4, 4);

            bool isEqual = A.Inverse().IsEqual(B);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestMatrixInverse2()
        {
            Matrix A = new Matrix("8,-5,9,2|7,5,6,1|-6,0,9,6|-3,0,-9,-4", 4, 4);
            Matrix B = new Matrix("-0.15385,-0.15385,-0.28205,-0.53846|" +
                                  "-0.07692,0.12308,0.02564,0.03077|" +
                                  "0.35897,0.35897,0.43590,0.92308|" +
                                  "-0.69231,-0.69231,-0.76923,-1.92308", 4, 4);

            bool isEqual = A.Inverse().IsEqual(B);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestMatrixInverse3()
        {
            Matrix A = new Matrix("9,3,0,9|-5,-2,-6,-3|-4,9,6,4|-7,6,6,2", 4, 4);
            Matrix B = new Matrix("-0.04074,-0.07778,0.14444,-0.22222|" +
                                  "-0.07778,0.03333,0.36667,-0.33333|" +
                                  "-0.02901,-0.14630,-0.10926,0.12963|" +
                                  "0.17778,0.06667,-0.26667,0.33333", 4, 4);

            bool isEqual = A.Inverse().IsEqual(B);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestMatrixInverse4()
        {
            Matrix A = new Matrix("3,-9,7,3|3,-8,2,-9|-4,4,4,1|-6,5,-1,1", 4, 4);
            Matrix B = new Matrix("8,2,2,2|3,-1,7,0|7,0,5,4|6,-2,0,5", 4, 4);

            Matrix C = F.Multiply(A, B);

            bool isEqual = (C * B.Inverse()).IsEqual(A);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestIdentityInverse()
        {
            Matrix A = Matrix.Identity(4);
            Matrix B = Matrix.Inverse(A);

            bool isEqual = A.IsEqual(B);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestMultiplyByInverse()
        {
            Matrix A = Matrix.Identity(4);
            Matrix B = new Matrix("3,-9,7,3|3,-8,2,-9|-4,4,4,1|-6,5,-1,1", 4, 4);

            Matrix C = F.Multiply(B, Matrix.Inverse(B));

            bool isEqual = A.IsEqual(C);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestInverseTranspose()
        {
            Matrix A = new Matrix("3,-9,7,3|3,-8,2,-9|-4,4,4,1|-6,5,-1,1", 4, 4);

            Matrix C = Matrix.Transpose(Matrix.Inverse(A));
            Matrix B = Matrix.Inverse(Matrix.Transpose(A));

            bool isEqual = B.IsEqual(C);

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestTupleMultiplyModifiedIdentity()
        {
            RTuple T = new RTuple(1,1,1,1);
            
            Matrix B = Matrix.Identity(4);

            B.MArray[0, 1] = 10;

            RTuple C = B * T;

            Console.WriteLine("X: {0}, Y: {1}, Z: {2}, W:{3}", T.X, T.Y, T.Z, T.W);
            Console.WriteLine();
            Console.WriteLine("X: {0}, Y: {1}, Z: {2}, W:{3}", C.X, C.Y, C.Z, C.W);
        }
    }
}
