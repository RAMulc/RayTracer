using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Projectile
{
    public class Environment
    {
        public Vector Gravity { get; set; }
        public Vector Wind { get; set; }

        public Environment(Vector g, Vector w)
        {
            Gravity = g;
            Wind = w;
        }
    }
}
