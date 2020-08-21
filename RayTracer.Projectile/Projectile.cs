using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Projectile
{
    public class Projectile
    {
        public Point Position { get; set; }
        public Vector Velocity { get; set; }

        public Projectile(Point p, Vector v)
        {
            Position = p;
            Velocity = v;
        }
    }
}
