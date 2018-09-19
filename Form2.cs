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
    public partial class Form2 : Form
    {
        int sizeX,sizeY;
        public Form2()
        {
            InitializeComponent();
        }
        public void Show_image1(Bitmap image)
        {
            sizeX = image.Width;
            pictureBox1.Width = image.Width;
            pictureBox1.Height = image.Height;
            this.Width = image.Width + 80;
            this.Height = image.Height + 80;
            pictureBox1.Image = image;
        }
        public void Show_image2(Bitmap image)
        {
            pictureBox2.Width = image.Width;
            pictureBox2.Height = image.Height;
            this.Width = image.Width + 80;
            this.Height = image.Height + 80;
            pictureBox2.Location =new Point(sizeX+30,45);
            pictureBox2.Image = image;
        }
        public void Show_image3(Bitmap image)
        {
            pictureBox3.Width = image.Width;
            pictureBox3.Height = image.Height;
            this.Width = image.Width + 80;
            this.Height = image.Height + 80;
            pictureBox3.Location = new Point(sizeX*2 + 40, 45);
            pictureBox3.Image = image;
        }

      


        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
