using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class BMPFile
    {
        /*BIT Map File Header*/
        private UInt16 ID;          //Identifier
        private UInt32 FSize;       // File Size
        private UInt64 Reserved;
        private UInt32 DataOffset;  //Bitmap Data offset

        /*DIB Header (Bitmap Information Header)*/
        private UInt32 DIBHeaderSize;
        private Int32  Width;
        private Int32  Height;
        private UInt16 PlaneNum;
        private UInt16 PerPixelNum;
        private UInt32 Compression;
        private UInt32 BitmapSize;
        private Int32  HResolution;
        private Int32  VResolution;
        private UInt32 UsedColors;
        private UInt32 ImportantColors;

        public Bitmap bitmap;
        public Int32 getWidth()
        {
            return Width;
        }
        public Int32 getHeight()
        {
            return Height;
        }
        public BMPFile(string fileName) {
            if (File.Exists(fileName)) {
                using (FileStream iStream = new FileStream(fileName, FileMode.Open)) {
                    BinaryReader reader = new BinaryReader(iStream);
                    /*
                     Bit Map File Header

                     Offset Hex |   Size(Bytes)  |   Purpose
                     ------------------------------------------------------------------------
                        0x00    |       2       |   The header file identify the BMP and DIB filr is 0x42 0x4d in hexadecimal, same as BM in ASCII
                        0x02    |       4       |   The size of the BMP file in bytes
                        0x06    |       2       |   Reserved; actual value depends on the application that created the images
                        0x08    |       2       |   Reserved; actual value depends on the application that created the images
                        0x0A    |       4       |   The offset, i.e. starting address, of the byte where the bitmap image data(pixel array) can be found.
                    */
                    iStream.Seek(0x00,SeekOrigin.Begin);
                    ID = reader.ReadUInt16();
                    FSize = reader.ReadUInt32();
                    Reserved = reader.ReadUInt32();
                    DataOffset = reader.ReadUInt32();
                    /*
                     DIB header(bitmap information header) 
                     Offset Hex |   Size(Bytes)  |   Purpose
                     ------------------------------------------------------------------------
                        0x0E    |       4       |   The size of this header (bytes)
                        0x12    |       4       |   The bitmap width in pixels (signed integer)
                        0x16    |       4       |   The bitmap height in pixels (signed integer)
                        0x1A    |       2       |   The number of color planes (must be 1)
                        0x1C    |       2       |   The number of bits per pixel
                        0x1E    |       4       |   The compression method being used
                        0x22    |       4       |   The image size. This is the size of the raw bitmap data
                        0x26    |       4       |   The horizontal resolution of the image. (pixel per metre, signed integer)
                        0x2A    |       4       |   The vertical resolution of the image. (pixel per metre, signed integer)
                        0x2E    |       4       |   The number of colors in the color palette, or 0 to default to 2^n.
                        0x32    |       4       |   The number of important colors used, or 0 when every color is important; generally ignored
                    */
                    iStream.Seek(0x0E, SeekOrigin.Begin);
                    DIBHeaderSize = reader.ReadUInt32();
                    Width = reader.ReadInt32();
                    Height = reader.ReadInt32();
                    PlaneNum = reader.ReadUInt16();
                    PerPixelNum = reader.ReadUInt16();
                    Compression = reader.ReadUInt32();
                    BitmapSize = reader.ReadUInt32();
                    HResolution = reader.ReadInt32();
                    VResolution = reader.ReadInt32();
                    UsedColors = reader.ReadUInt32();
                    ImportantColors = reader.ReadUInt32();

                    bitmap = new Bitmap(Width, Height);
                    Color c;
                    iStream.Seek(DataOffset, SeekOrigin.Begin);
                    for (int i = Height - 1; i >= 0; i--) {
                        for (int j = 0; j < Width; j++)
                        {
                            byte B = reader.ReadByte();
                            byte G = reader.ReadByte();
                            byte R = reader.ReadByte();
                            c = Color.FromArgb(255,R,G,B);
                            bitmap.SetPixel(j,i,c);
                        }
                    }
                }
            }
        }
        public void printHeader(Form1 form) {
            form.printOnList1("ID",ID);
            form.printOnList1("File Size",FSize);
            form.printOnList1("Reserved",Reserved);
            form.printOnList1("DataOffset",DataOffset);
            form.printOnList1("DIBHeaderSize",DIBHeaderSize);
            form.printOnList1("Width",Width);
            form.printOnList1("Height",Height);
            form.printOnList1("PlaneNum",PlaneNum);
            form.printOnList1("PerPixelNum",PerPixelNum); 
            form.printOnList1("Compression",Compression);
            form.printOnList1("BitmapSize",BitmapSize);
            form.printOnList1("HResolution",HResolution);
            form.printOnList1("VResolution",VResolution);
            form.printOnList1("UsedColors",UsedColors);
            form.printOnList1("ImportantColors" ,ImportantColors);
                                                                
        }
        public void Save(String FileName, Bitmap BitmapS) {
            
            //MessageBox.Show(FileName);
            using (FileStream oStream = new FileStream(FileName, FileMode.Create) )
            {
                BinaryWriter BW = new BinaryWriter(oStream);
                oStream.Seek(0x00, SeekOrigin.Begin);
                BW.Write(ID);
                BW.Write(FSize);
                BW.Write(Reserved);
                BW.Write(DataOffset);

                oStream.Seek(0x0E, SeekOrigin.Begin);
                BW.Write(DIBHeaderSize);
                BW.Write(Width);
                BW.Write(Height);
                BW.Write(PlaneNum);
                BW.Write(PerPixelNum);
                BW.Write(Compression);
                BW.Write(BitmapSize);
                BW.Write(HResolution);
                BW.Write(VResolution);
                BW.Write(UsedColors);
                BW.Write(ImportantColors);
                Color c;
                for (int i = Height - 1; i >= 0; i--) {
                    for (int j = 0; j < Width; j++)
                    {
                        c = BitmapS.GetPixel(j,i);
                        BW.Write(c.B);
                        BW.Write(c.G);
                        BW.Write(c.R);
                    }
                }
                BW.Write(0X00);
                oStream.Close();
            }
            
        }
    }                                                             
}
