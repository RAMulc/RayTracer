using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Write
    {
        public static void WritePpm(List<string> data)
        {
            System.IO.File.WriteAllLines(@"E:\CSharp\RayTracer\Image.ppm", data);
        }
    }
}
