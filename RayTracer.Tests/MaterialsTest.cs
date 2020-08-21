using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RayTracer.Tests
{
    [TestClass]
    public class MaterialsTest
    {
        [TestMethod]
        public void TestMaterials()
        {
            Material material = new Material(new Color(1,1,1));

            Color newColor = new Color(1,1,1);
            bool isEqual = material.MatColor.IsEqual(newColor);

            Assert.AreEqual(isEqual, true);
            Assert.AreEqual(material.Ambient, 0.1);
            Assert.AreEqual(material.Diffuse, 0.9);
            Assert.AreEqual(material.Shininess, 200.0);
            Assert.AreEqual(material.Specular, 0.9);
        }

        [TestMethod]
        public void TestMaterialLight1()
        {
            Material m = new Material(new Color(1, 1, 1));
            Point position = new Point(0, 0, 0);
            
            Vector eyeV = new Vector(0, 0, -1);
            Vector normalV = new Vector(0, 0, -1);

            Light light = new Light(new Point(0, 0, -10), new Color(1, 1, 1));
            Lighting result = new Lighting(m, new Sphere(), light, position, eyeV, normalV);

            bool isEqual = result.Result.IsEqual(new Color(1.9, 1.9, 1.9));

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestMaterialLight1Stripe()
        {
            Material m = new Material
            {
                Pattern = new StripePattern( new Color( 1, 1, 1 ), new Color( 0, 0, 0 ), false ),
                Ambient = 1,
                Diffuse = 0,
                Specular = 0
            };

            Vector eyeV = new Vector( 0, 0, -1 );
            Vector normalV = new Vector( 0, 0, -1 );
            Light light = new Light( new Point( 0, 0, -10 ), new Color( 1, 1, 1 ) );

            Point position1 = new Point( 0.9, 0, 0 );
            Lighting result1 = new Lighting( m, new Sphere(), light, position1, eyeV, normalV );

            Point position2 = new Point( 1.1, 0, 0 );
            Lighting result2 = new Lighting( m, new Sphere(), light, position2, eyeV, normalV );

            bool isEqual1 = result1.Result.IsEqual( new Color( 1, 1, 1 ) );
            bool isEqual2 = result2.Result.IsEqual( new Color( 0, 0, 0 ) );

            Assert.AreEqual( isEqual1, true );
            Assert.AreEqual( isEqual2, true );
        }

        [TestMethod]
        public void TestMaterialLight2()
        {
            Material m = new Material(new Color(1, 1, 1));
            Point position = new Point(0, 0, 0);
            
            Vector eyeV = new Vector(0, Math.Sqrt(2)/2, -Math.Sqrt(2)/2);
            Vector normalV = new Vector(0, 0, -1);

            Light light = new Light(new Point(0, 0, -10), new Color(1, 1, 1));
            Lighting result = new Lighting(m, new Sphere(), light, position, eyeV, normalV);

            bool isEqual = result.Result.IsEqual(new Color(1, 1, 1));

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestMaterialLight3()
        {
            Material m = new Material(new Color(1, 1, 1));
            Point position = new Point(0, 0, 0);
            
            Vector eyeV = new Vector(0, 0, -1);
            Vector normalV = new Vector(0, 0, -1);

            Light light = new Light(new Point(0, 10, -10), new Color(1, 1, 1));
            Lighting result = new Lighting(m, new Sphere(), light, position, eyeV, normalV);

            bool isEqual = result.Result.IsEqual(new Color(0.7364, 0.7364, 0.7364));

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestMaterialLight4()
        {
            Material m = new Material(new Color(1, 1, 1));
            Point position = new Point(0, 0, 0);
            
            Vector eyeV = new Vector(0, -Math.Sqrt(2)/2, -Math.Sqrt(2)/2);
            Vector normalV = new Vector(0, 0, -1);

            Light light = new Light(new Point(0, 10, -10), new Color(1, 1, 1));
            Lighting result = new Lighting(m, new Sphere(), light, position, eyeV, normalV);

            bool isEqual = result.Result.IsEqual(new Color(1.6364, 1.6364, 1.6364));

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestMaterialLight5()
        {
            Material m = new Material(new Color(1, 1, 1));
            Point position = new Point(0, 0, 0);
            
            Vector eyeV = new Vector(0, 0, -1);
            Vector normalV = new Vector(0, 0, -1);

            Light light = new Light(new Point(0, 0, 10), new Color(1, 1, 1));
            Lighting result = new Lighting(m, new Sphere(), light, position, eyeV, normalV);

            bool isEqual = result.Result.IsEqual(new Color(0.1, 0.1, 0.1));

            Assert.AreEqual(isEqual, true);
        }

        [TestMethod]
        public void TestInShadow()
        {
            Vector eyeVec = new Vector(0, 0, -1);
            Vector normalVec = new Vector(0, 0, -1);

            Light light = new Light(new Point(0, 0, -10), new Color(1, 1, 1));
            bool inShadow = true;
            Lighting res = new Lighting(new Material(), new Sphere(), light, new Point(0,0,0), eyeVec, normalVec, inShadow);
            Color col = res.Result;

            Color c = new Color(0.1, 0.1, 0.1);

            bool isequal = col.IsEqual(c);

            Assert.AreEqual(isequal, true);
        }

        [TestMethod]
        public void TestReflective()
        {
            Material m = new Material();

            Assert.AreEqual( m.Reflective, 0 );
        }

        [TestMethod]
        public void TransparencyRefractiveIndex()
        {
            Material m = new Material();

            Assert.AreEqual( m.RefractiveIndex, 1 );
            Assert.AreEqual( m.Transparency, 0 );
        }
    }
}
