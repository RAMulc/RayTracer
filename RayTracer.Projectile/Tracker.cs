using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F = RayTracer.Functions;

namespace RayTracer.Projectile
{
    public class Tracker
    {
        public static Projectile Tick(Environment env, Projectile proj)
        {
            var pos = F.Add(proj.Position, proj.Velocity);
            var vel = F.Add(F.Add(proj.Velocity, env.Gravity), env.Wind);

            return new Projectile(new Point(pos), new Vector(vel));
        }
    }
}
