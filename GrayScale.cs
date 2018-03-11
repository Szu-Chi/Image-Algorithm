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
        public static Bitmap Averaging(Bitmap source){
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
    }
}
