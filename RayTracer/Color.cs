using F = RayTracer.Functions;

namespace RayTracer
{
    public class Color :RTuple
    {
        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }

        public Color(double red, double green, double blue) : base(red, green, blue, 0)
        { 
            Red = red;
            Green = green;
            Blue = blue;
        }

        public Color(RTuple rTuple) : base(rTuple.X, rTuple.Y, rTuple.Z, rTuple.W)
        {
            Red = rTuple.X;
            Green = rTuple.Y;
            Blue = rTuple.Z;
        }

        public Color HadamardProduct(Color c)
        {
            double red = Red * c.Red;
            double green = Green * c.Green;
            double blue = Blue * c.Blue;

            return new Color(red, green, blue);
        }

        public static Color operator *(Color c, double d)
        {
            return F.Multiply(c, d);
        }

        public static Color operator +(Color a, Color b)
        {
            return new Color(a.Red + b.Red, a.Green + b.Green, a.Blue + b.Blue);
        }

        public static bool IsEqual(Color a, Color b)
        {
            if (a.Blue == b.Blue && a.Green == b.Green && a.Red == b.Red)
                return true;
            return false;
        }
    }
}
