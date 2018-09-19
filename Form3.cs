using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Form3 : Form
    {
        int sizeX,XX,YY;

        Bitmap B1, B2, B3,P1,P2,P3;


        Form2 fr2 = new Form2();
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        public void form3_Show_image(Bitmap image)
        {
            sizeX = image.Width;
            pictureBox1.Width = image.Width;
            pictureBox1.Height = image.Height;
            this.Width = image.Width + 80;
            this.Height = image.Height + 80;
            pictureBox1.Image = image;
        }

        public void form3_Show_image1(Bitmap image)
        {
            pictureBox2.Width = image.Width;
            pictureBox2.Height = image.Height;
            this.Width = image.Width + 80;
            this.Height = image.Height + 80;
            pictureBox2.Location = new Point(image.Width + 30, 40);
            pictureBox2.Image = image;
        }

        public void form3_Show_image2(Bitmap image)
        {
            pictureBox3.Width = image.Width;
            pictureBox3.Height = image.Height;
            this.Width = image.Width + 80;
            this.Height = image.Height + 80;
            pictureBox3.Location = new Point(image.Width *2+ 30, 40);
            pictureBox3.Image = image;
            
        }

        private void form3_pictureBox1_Down(object sender,System.Windows.Forms.MouseEventArgs e)
        {
            B1 = new Bitmap(pictureBox1.Image);
            B2 = new Bitmap(pictureBox2.Image);
            B3 = new Bitmap(pictureBox3.Image);
            P1 = new Bitmap(128, 128);
            P2 = new Bitmap(128, 128);
            P3 = new Bitmap(128, 128);


            XX = e.X - 32;
            YY = e.Y - 32;

            try
            {
                for (int i = 0; i < 128; i++)
                {
                    for (int j = 0; j < 128; j++)
                    {
                        Color color1 = B1.GetPixel(XX, YY);
                        P1.SetPixel(i, j, Color.FromArgb((int)(color1.R), (int)(color1.G), (int)(color1.B)));
                        if (i == 0 && j == 0)
                            Console.WriteLine("改變[" + XX + "," + YY + "]  color R=" + color1.R + "color G = " + color1.G + "color B = " + color1.B);
                        Color color2 = B2.GetPixel(XX, YY);
                        P2.SetPixel(i, j, Color.FromArgb((int)(color2.R), (int)(color2.G), (int)(color2.B)));
                        if (i == 0 && j == 0)
                            Console.WriteLine("改變[" + XX + "," + YY + "]  color R=" + color2.R + "color G = " + color2.G + "color B = " + color2.B);
                        Color color3 = B3.GetPixel(XX, YY);
                        P3.SetPixel(i, j, Color.FromArgb((int)(color3.R), (int)(color3.G), (int)(color3.B)));
                        if (i == 0 && j == 0)
                            Console.WriteLine("改變[" + XX + "," + YY + "]  color R=" + color3.R + "color G = " + color3.G + "color B = " + color3.B);

                        if (j % 2 == 0)
                        {
                            YY++;
                        }
                    }
                    if (i % 2 == 0)
                    {
                        XX++;
                    }
                    YY = e.Y - 32;
                }

                fr2.Show_image1(P1);
                fr2.Show_image2(P2);
                fr2.Show_image3(P3);
                fr2.Show();


            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("出現問題");
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Console.WriteLine("出現問題");
            }


        }


    }
}
