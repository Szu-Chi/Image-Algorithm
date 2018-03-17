using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        BMPFile myBMP;
        Bitmap BitmapE;
        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.Columns.Add("Variable", 149, HorizontalAlignment.Left);
            listView1.Columns.Add("Value", 130, HorizontalAlignment.Left);
            myBMP = new BMPFile("defaultIamge.bmp");
            myBMP.printHeader(this);
            pictureBox1.Image = myBMP.bitmap;
        }

        public void printOnList1<T>(String A,T B ) {
            var item1 = new ListViewItem(new[] { A, String.Format("0x{0:X}", B) });
            listView1.Items.Add(item1);
        }

        private void averagingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BitmapE = GrayScale.Averaging(myBMP.bitmap);
            pictureBox1.Image = BitmapE;
        }

        private void luminanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BitmapE = GrayScale.Luminance(myBMP.bitmap);
            pictureBox1.Image = BitmapE;
        }

        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "BMP files (*.bmp)|*.bmp|All files (*.*)|*.*";
            file.FilterIndex = 1;
            file.Multiselect = true;
            if (file.ShowDialog() == DialogResult.OK)
            {
                string sFileName = file.FileName;
                string[] arrAllFiles = file.FileNames; //used when Multiselect = true           
                myBMP = new BMPFile(sFileName);
                myBMP.printHeader(this);
                pictureBox1.Image = myBMP.bitmap;
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "BMP files (*.bmp)|*.bmp|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                myBMP.Save(saveFileDialog1.FileName, BitmapE);
            }
        }
    }
}
