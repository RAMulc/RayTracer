using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Lighting
    {
        public Material Material { get; set; }
        public Light Light { get; set; }
        public Point Point { get; set; }
        public Vector EyeVector { get; set; }
        public Vector NormalVector { get; set; }
        public Color Result { get; set; }
        public Shape Shape { get; set; }
        
        public Lighting(Material material, Shape shape, Light light, Point point, Vector eyeVector, Vector normalVector, bool inShadow = false)
        {
            Material = material;
            Light = light;
            Point = point;
            EyeVector = eyeVector;
            NormalVector = normalVector;
            Shape = shape;
            Result = Process(inShadow);
        }

        private Color Process(bool inShadow)
        {
            Color color = Material.MatColor;
            if ( Material.Pattern != null )
                color = Pattern.PatternAtShape( Material.Pattern, Shape, Point );

            Color effectiveColor = color.HadamardProduct(Light.Intensity);
            Vector lightVector = Light.Position - Point;
            lightVector.Normalize();

            Color ambient = effectiveColor * Material.Ambient;

            Color diffuse;
            Color specular;

            double lightDotNorm = lightVector.Dot(NormalVector);
            if (lightDotNorm < 0 || inShadow)
            {
                diffuse = new Color(0,0,0);
                specular = new Color(0,0,0);
            }
            else
            {
                diffuse = effectiveColor * (Material.Diffuse * lightDotNorm);

                Vector reflectV = (lightVector * -1).Reflect(NormalVector);
                double reflectVDotEye = reflectV.Dot(EyeVector);

                if (reflectVDotEye <= 0)
                    specular = new Color(0,0,0);
                else
                {
                    double factor = Math.Pow(reflectVDotEye, Material.Shininess);
                    specular = Light.Intensity * (Material.Specular * factor);
                }
            }

            return ambient + diffuse + specular;
        }
    }
}
