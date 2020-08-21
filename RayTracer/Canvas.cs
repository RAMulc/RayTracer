using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Canvas
    {
        public static int Width { get; set; }
        public static int Height { get; set; }

        public Pixel[,] Pixels;

        public Canvas(int width, int height)
        {
            Width = width;
            Height = height;
            Pixels = new Pixel[Width, Height];
            InitializeCanvas();
        }

        public void WritePixel(Pixel pixel)
        {
            if (pixel.X < Width && pixel.Y < Height)
                Pixels[pixel.X, pixel.Y] = pixel;
        }

        public void InitializeCanvas()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                    Pixels[j, i] = new Pixel(j, i, new Color(1,1,1));
            }
        }

        public List<string> CanvasToPpmString()
        {
            List<string> ppmString = new List<string>
            {
                "P3",
                Width.ToString() + " " + Height.ToString(),
                "255"
            };

            for (int i = 0; i < Height; i++)
            {
                string newLine = String.Empty;
                for (int j = 0; j < Width; j++)
                {
                    int red = SetColorValue(Pixels[j, i].PixelColor.Red);
                    int green = SetColorValue(Pixels[j, i].PixelColor.Green);
                    int blue = SetColorValue(Pixels[j, i].PixelColor.Blue);

                    if (AddToString(ref newLine, red.ToString(), 70))
                    {
                        ppmString.Add(newLine.Trim());
                        newLine = red.ToString();
                    }
                    if (AddToString(ref newLine, green.ToString(), 70))
                    {
                        ppmString.Add(newLine.Trim());
                        newLine = green.ToString();
                    }
                    if (AddToString(ref newLine, blue.ToString(), 70))
                    {
                        ppmString.Add(newLine.Trim());
                        newLine = blue.ToString();
                    }
                }
                ppmString.Add(newLine.Trim());
            }
            ppmString.Add(" ");
            return ppmString;
        }

        private bool AddToString(ref string a, string b, int len)
        {
            if ((a.Length + b.Length) >= len)
                return true;
            else
                a = a + " " + b;
            return false;
        }

        private int SetColorValue(double c)
        {
            int col = (int) (c * 255);
            if (col < 0)
                return 0;
            else if (col > 255)
                return 255;
            else
                return col;
        }

        public void WritePpm()
        {
            Write.WritePpm(CanvasToPpmString());
        }
    }
}
