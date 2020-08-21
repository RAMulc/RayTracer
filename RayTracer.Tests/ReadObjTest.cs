using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayTracer;

namespace RayTracer.Tests
{
    [TestClass]
    public class ReadObjTest
    {
        [TestMethod]
        public void ParseObj()
        {
            ReadObj rObj = new ReadObj( @"E:\CSharp\RayTracer\obj\Sample\Sample.obj" );
            //ReadObj rObj = new ReadObj( @"E:\CSharp\RayTracer\obj\Sample\Gibberish.obj" );

            bool b1 = Functions.IsEqual( rObj.Vertices[1], new Point( -1, 1, 0 ) );
            bool b2 = Functions.IsEqual( rObj.Vertices[2], new Point( -1, 0, 0 ) );
            bool b3 = Functions.IsEqual( rObj.Vertices[3], new Point( 1, 0, 0 ) );
            bool b4 = Functions.IsEqual( rObj.Vertices[4], new Point( 1, 1, 0 ) );

            Assert.AreEqual( b1, true );
            Assert.AreEqual( b2, true );
            Assert.AreEqual( b3, true );
            Assert.AreEqual( b4, true );
        }

        [TestMethod]
        public void ParseObjWithFaces()
        {
            ReadObj rObj = new ReadObj( @"E:\CSharp\RayTracer\obj\Sample\Sample.obj" );

            bool b1 = Functions.IsEqual( (rObj.ObjGroup.Children[0] as Triangle).P1 , new Point( -1, 1, 0 ) );
            bool b2 = Functions.IsEqual( ( rObj.ObjGroup.Children[0] as Triangle ).P2, new Point( -1, 0, 0 ) );
            bool b3 = Functions.IsEqual( ( rObj.ObjGroup.Children[0] as Triangle ).P3, new Point( 1, 0, 0 ) );
            bool b4 = Functions.IsEqual( ( rObj.ObjGroup.Children[1] as Triangle ).P1, new Point( -1, 1, 0 ) );
            bool b5 = Functions.IsEqual( ( rObj.ObjGroup.Children[1] as Triangle ).P2, new Point( 1, 0, 0 ) );
            bool b6 = Functions.IsEqual( ( rObj.ObjGroup.Children[1] as Triangle ).P3, new Point( 1, 1, 0 ) );

            Assert.AreEqual( b1, true );
            Assert.AreEqual( b2, true );
            Assert.AreEqual( b3, true );
            Assert.AreEqual( b4, true );
            Assert.AreEqual( b5, true );
            Assert.AreEqual( b6, true );
        }

        [TestMethod]
        public void ParseObjWithFacesAndTriangulation()
        {
            ReadObj rObj = new ReadObj( @"E:\CSharp\RayTracer\obj\Sample\Sample.obj" );

            bool b1 = Functions.IsEqual( ( rObj.ObjGroup.Children[0] as Triangle ).P1, new Point( -1, 1, 0 ) );
            bool b2 = Functions.IsEqual( ( rObj.ObjGroup.Children[0] as Triangle ).P2, new Point( -1, 0, 0 ) );
            bool b3 = Functions.IsEqual( ( rObj.ObjGroup.Children[0] as Triangle ).P3, new Point( 1, 0, 0 ) );
            bool b4 = Functions.IsEqual( ( rObj.ObjGroup.Children[1] as Triangle ).P1, new Point( -1, 1, 0 ) );
            bool b5 = Functions.IsEqual( ( rObj.ObjGroup.Children[1] as Triangle ).P2, new Point( 1, 0, 0 ) );
            bool b6 = Functions.IsEqual( ( rObj.ObjGroup.Children[1] as Triangle ).P3, new Point( 1, 1, 0 ) );
            bool b7 = Functions.IsEqual( ( rObj.ObjGroup.Children[2] as Triangle ).P1, new Point( -1, 1, 0 ) );
            bool b8 = Functions.IsEqual( ( rObj.ObjGroup.Children[2] as Triangle ).P2, new Point( 1, 1, 0 ) );
            bool b9 = Functions.IsEqual( ( rObj.ObjGroup.Children[2] as Triangle ).P3, new Point( 0, 2, 0 ) );

            Assert.AreEqual( b1, true );
            Assert.AreEqual( b2, true );
            Assert.AreEqual( b3, true );
            Assert.AreEqual( b4, true );
            Assert.AreEqual( b5, true );
            Assert.AreEqual( b6, true );
            Assert.AreEqual( b7, true );
            Assert.AreEqual( b8, true );
            Assert.AreEqual( b9, true );
        }

        [TestMethod]
        public void ParseObjWithFacesIntoGroups()
        {
            ReadObj rObj = new ReadObj( @"E:\CSharp\RayTracer\obj\Sample\Sample.obj" );

            bool b1 = Functions.IsEqual( (( rObj.ObjGroup.Children[0] as Group ).Children[0] as Triangle ).P1, new Point( -1, 1, 0 ) );
            bool b2 = Functions.IsEqual( (( rObj.ObjGroup.Children[0] as Group ).Children[0] as Triangle ).P2, new Point( -1, 0, 0 ) );
            bool b3 = Functions.IsEqual( (( rObj.ObjGroup.Children[0] as Group ).Children[0] as Triangle ).P3, new Point( 1, 0, 0 ) );
            bool b4 = Functions.IsEqual( (( rObj.ObjGroup.Children[1] as Group ).Children[0] as Triangle ).P1, new Point( -1, 1, 0 ) );
            bool b5 = Functions.IsEqual( (( rObj.ObjGroup.Children[1] as Group ).Children[0] as Triangle ).P2, new Point( 1, 0, 0 ) );
            bool b6 = Functions.IsEqual( (( rObj.ObjGroup.Children[1] as Group ).Children[0] as Triangle ).P3, new Point( 1, 1, 0 ) );

            Assert.AreEqual( b1, true );
            Assert.AreEqual( b2, true );
            Assert.AreEqual( b3, true );
            Assert.AreEqual( b4, true );
            Assert.AreEqual( b5, true );
            Assert.AreEqual( b6, true );
        }

        [TestMethod]
        public void ReadObj()
        {
            ReadObj rObj = new ReadObj( @"E:\CSharp\RayTracer\obj\Teapot.obj" );
            //ReadObj rObj = new ReadObj( @"E:\CSharp\RayTracer\obj\Sample\Gibberish.obj" );

        }

        [TestMethod]
        public void ReadObjNormals()
        {
            ReadObj rObj = new ReadObj( @"E:\CSharp\RayTracer\obj\Sample\Sample.obj" );

            bool b1 = Functions.IsEqual( rObj.Normals[1], new Vector( 0, 0, 1 ) );
            bool b2 = Functions.IsEqual( rObj.Normals[2], new Vector( 0.707, 0, -0.707 ) );
            bool b3 = Functions.IsEqual( rObj.Normals[3], new Vector( 1, 2, 3 ) );

            Assert.AreEqual( b1, true );
            Assert.AreEqual( b2, true );
            Assert.AreEqual( b3, true );
        }

        [TestMethod]
        public void ReadObjFaceNormals()
        {
            ReadObj rObj = new ReadObj( @"E:\CSharp\RayTracer\obj\Sample\Sample.obj" );

            bool b1 = Functions.IsEqual( ( rObj.ObjGroup.Children[0] as SmoothTriangle ).P1, new Point( 0, 1, 0 ) );
            bool b2 = Functions.IsEqual( ( rObj.ObjGroup.Children[0] as SmoothTriangle ).P2, new Point( -1, 0, 0 ) );
            bool b3 = Functions.IsEqual( ( rObj.ObjGroup.Children[0] as SmoothTriangle ).P3, new Point( 1, 0, 0 ) );
            bool b4 = Functions.IsEqual( ( rObj.ObjGroup.Children[0] as SmoothTriangle ).N1, new Vector( 0, 1, 0 ) );
            bool b5 = Functions.IsEqual( ( rObj.ObjGroup.Children[0] as SmoothTriangle ).N2, new Vector( -1, 0, 0 ) );
            bool b6 = Functions.IsEqual( ( rObj.ObjGroup.Children[0] as SmoothTriangle ).N3, new Vector( 1, 0, 0 ) );

            Assert.AreEqual( b1, true );
            Assert.AreEqual( b2, true );
            Assert.AreEqual( b3, true );
            Assert.AreEqual( b4, true );
            Assert.AreEqual( b5, true );
            Assert.AreEqual( b6, true );
        }
    }
}
