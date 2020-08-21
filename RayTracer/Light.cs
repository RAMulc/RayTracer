using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Light
    {
        public Color Intensity { get; set; }
        public Point Position { get; set; }

        public Light(Point position, Color color)
        {
            Position = position;
            Intensity = color;
        }
    }
}
