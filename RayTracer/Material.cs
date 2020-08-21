using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Material
    {
        public Color MatColor { get; set; }
        public double Ambient { get; set; }
        public double Diffuse { get; set; }
        public double Specular { get; set; }
        public double Shininess { get; set; }
        public Pattern Pattern { get; set; }
        public double Reflective { get; set; }
        public double RefractiveIndex { get; set; }
        public double Transparency { get; set; }

        public Material()
        {
            MatColor = new Color(1, 1, 1);
            Ambient = 0.1;
            Diffuse = 0.9;
            Specular = 0.9;
            Shininess = 200;
            Pattern = null;
            Reflective = 0;
            Transparency = 0;
            RefractiveIndex = 1;
        }

        public Material(Color color, double ambient = 0.1, double diffuse = 0.9, double specular = 0.9, double shininess = 200, double reflective = 0,
                        double transparency = 0, double refractiveIndex = 1, bool castShadow = true)
        {
            if (color == null)
                MatColor = new Color(1, 1, 1);
            else
                MatColor = color;

            if (ambient < 0)
                Ambient = 0;
            else if (ambient > 1)
                Ambient = 1;
            else
                Ambient = ambient;

            if (diffuse < 0)
                Diffuse = 0;
            else if (diffuse > 1)
                Diffuse = 1;
            else
                Diffuse = diffuse;

            if (specular < 0)
                Specular = 0;
            else if (specular > 1)
                Specular = 1;
            else
                Specular = specular;

            if (shininess < 10)
                Shininess = 10;
            else
                Shininess = shininess;

            Pattern = null;

            if ( reflective > 1 )
                Reflective = 1;
            else if ( reflective < 0 )
                Reflective = 0;
            else
                Reflective = reflective;

            Transparency = transparency;
            RefractiveIndex = refractiveIndex;
        }
    }
}
