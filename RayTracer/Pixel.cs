using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Pixel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color PixelColor { get; set; }

        public Pixel(int x, int y, Color c)
        {
            X = x;
            Y = y;
            PixelColor = c;
        }
    }
}
