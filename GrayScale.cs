using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApp1
{
    class GrayScale
    {
        public static Bitmap Averaging(Bitmap _source){
            Bitmap source = new Bitmap(_source);
            for (int i = 0; i < source.Height; i++) {
                for (int j = 0; j < source.Width; j++) {
                    Color pixelColor = source.GetPixel(j,i);
                    int avg = (pixelColor.R + pixelColor.G + pixelColor.B)/3;
                    pixelColor = Color.FromArgb(255,avg,avg,avg);
                    source.SetPixel(j, i, pixelColor);
                }
            }
            return source;
        }
        public static Bitmap Luminance(Bitmap _source)
        {
            Bitmap source = new Bitmap(_source);
            for (int i = 0; i < source.Height; i++)
            {
                for (int j = 0; j < source.Width; j++)
                {
                    Color pixelColor = source.GetPixel(j, i);
                    int gray = (int)(pixelColor.R*0.3 + pixelColor.G*0.59 + pixelColor.B*0.11);
                    pixelColor = Color.FromArgb(255, gray, gray, gray);
                    source.SetPixel(j, i, pixelColor);
                }
            }
            return source;
        }

        public static Bitmap Desaturation(Bitmap _source) {
            Bitmap source= new Bitmap(_source);
            for (int i = 0; i < source.Height; i++)
            {
                for (int j = 0; j < source.Width; j++)
                {
                    Color pixelColor = source.GetPixel(j, i);
                    int max = (int)Math.Max(pixelColor.R, (Math.Max(pixelColor.B, pixelColor.G)));
                    int min = (int)Math.Min(pixelColor.R, (Math.Min(pixelColor.B, pixelColor.G)));
                    int gray = (max + min) / 2;
                    pixelColor = Color.FromArgb(255, gray, gray, gray);
                    source.SetPixel(j, i, pixelColor);
                }
            }
            return source;
        }
    }
}
