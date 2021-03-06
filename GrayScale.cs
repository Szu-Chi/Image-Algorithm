﻿using System;
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
        public static Bitmap Luminance(Bitmap source)
        {
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
    }
}
