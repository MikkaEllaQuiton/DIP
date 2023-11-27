using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing
{
    public partial class Form1 : Form
    {
        Bitmap loaded;
        Bitmap processed;
        Bitmap imageB, imageA, colorgreen, resultImage;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loaded;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            Color pixel;
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    pixel = loaded.GetPixel(i, j);
                    processed.SetPixel(i, j, pixel);
                }
            }
            pictureBox2.Image = processed;
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox2.Image.Save(saveFileDialog1.FileName);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPEG Image|.jpg|PNG Image|.png|All Files|.";
            saveFileDialog1.ShowDialog();
        }

        private void greyscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            Color pixel;
            int gray;
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    pixel = loaded.GetPixel(i, j);
                    gray = ((pixel.R + pixel.G + pixel.B) / 3);
                    processed.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            pictureBox2.Image = processed;
        }

        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            Color pixel;
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    pixel = loaded.GetPixel(i, j);
                    processed.SetPixel(i, j, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                }
            }
            pictureBox2.Image = processed;
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            Color pixel;
            int gray;
            processed = new Bitmap(loaded.Width, loaded.Height);
            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    pixel = loaded.GetPixel(i, j);
                    gray = ((pixel.R + pixel.G + pixel.B) / 3);
                    processed.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            Color sample;
            int[] hisdata = new int[256];
            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    sample = processed.GetPixel(i, j);
                    hisdata[sample.R]++;
                }
            }
            Bitmap mydata = new Bitmap(265, 800);
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 800; j++)
                {
                    mydata.SetPixel(i, j, Color.White);
                }
            }

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < Math.Min(hisdata[i] / 5, 800); j++)
                {
                    mydata.SetPixel(i, j, Color.Black);
                }
            }

            pictureBox2.Image = mydata;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            processed = new Bitmap(loaded.Width, loaded.Height);

            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    Color pixel = loaded.GetPixel(i, j);
                    int sepiaR = (int)(0.393 * pixel.R + 0.769 * pixel.G + 0.189 * pixel.B);
                    int sepiaG = (int)(0.349 * pixel.R + 0.686 * pixel.G + 0.168 * pixel.B);
                    int sepiaB = (int)(0.272 * pixel.R + 0.534 * pixel.G + 0.131 * pixel.B);
               
                    sepiaR = Math.Max(0, Math.Min(sepiaR, 255));
                    sepiaG = Math.Max(0, Math.Min(sepiaG, 255));
                    sepiaB = Math.Max(0, Math.Min(sepiaB, 255));

                    processed.SetPixel(i, j, Color.FromArgb(sepiaR, sepiaG, sepiaB));
                }
            }

            pictureBox2.Image = processed;
        }
        

       

        private void button1_Click_1(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            openFileDialog3.ShowDialog();
        }

        private void openFileDialog3_FileOk_1(object sender, CancelEventArgs e)
        {
            imageA = new Bitmap(openFileDialog3.FileName);
            processed = new Bitmap(openFileDialog3.FileName);
            pictureBox2.Image = imageA;
        }

        private void openFileDialog2_FileOk_1(object sender, CancelEventArgs e)
        {
            imageB = new Bitmap(openFileDialog2.FileName);
            loaded = new Bitmap(openFileDialog2.FileName);
            pictureBox1.Image = imageB;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Color mygreen = imageB.GetPixel(0, 0);
            int greygreen = (mygreen.R + mygreen.G + mygreen.B) / 3;
            int threshold = 5;
            resultImage = new Bitmap(imageB.Width, imageB.Height);

            for (int x = 0; x < imageB.Width; x++)
            {
                for (int y = 0; y < imageA.Height; y++)
                {
                    Color pixel = imageB.GetPixel(x, y);
                    Color backpixel = imageA.GetPixel(x, y);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    int subtractValue = Math.Abs(grey - greygreen);

                    if (subtractValue > threshold)
                    {
                        resultImage.SetPixel(x, y, pixel);
                    }

                    else
                    {
                        resultImage.SetPixel(x, y, backpixel);
                    }
                }
            }

            pictureBox3.Image = resultImage;
        }
       
    }
}
