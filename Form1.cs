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
    public partial class Form1 : Form
    {

        Form2 fr2 = new Form2();
        Form3 fr3 = new Form3();
  
        Image image;
        double[,] OR, OG, OB; //原圖
        Bitmap OP,RP;

        bool standard = false;
        Bitmap P1, P2, P3;
        

        int XX = 0, YY = 0;
        Bitmap B , F;
        

        double X, Y;


        double[,] FinalPR, FinalPG, FinalPB;
        double[,] firstPR, firstPG, firstPB;

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        double totalR = 0.0;
        double totalG = 0.0;
        double totalB = 0.0;

        double[,] secondPR, secondPG, secondPB;
        double[,] thirdPR, thirdPG, thirdPB;
        double[,] forthPR, forthPG, forthPB;

        double[,] Gauss;
        double total;





        public Form1()
        {
            
            InitializeComponent();     
        }
        
       
        private void Gauss_()
        {
            
            Gauss = new double[5, 5];
            int x = 0;
            int y = 0;
            try {
                for (int i = -2; i < 3; i++)
                {
                    for (int j = -2; j < 3; j++)
                    {
                        Gauss[x, y] = (1.0 / (2.0 * Math.PI * Math.Pow(double.Parse(textBox1.Text), 2))) * Math.Exp((-Math.Pow(i, 2) - Math.Pow(j, 2)) / (2.0 * Math.Pow(double.Parse(textBox1.Text), 2)));
                        total += Gauss[x, y];
                        y++;
                    }
                    x++;
                    y = 0;
                }
            }catch(System.FormatException)
            {
                
                string message = "輸入不正確";
                string caption = "警告";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);       
                Console.WriteLine("輸入不正確");
                
               
            }
        }





        private void button1_Click(object sender, EventArgs e)
        {
            string filename;
            OpenFileDialog openfile = new OpenFileDialog();
            if (openfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = openfile.FileName;
                image = Image.FromFile(filename);
                pictureBox1.Height = image.Height;
                pictureBox1.Width = image.Width;
                pictureBox1.Image = image;
                pictureBox2.Height = image.Height;
                pictureBox2.Width = image.Width;
                
                // panel1.Width = image.Width * 2;
                this.Height = image.Height + 150;
                this.Width = image.Width + 100;
            }

        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            
            X = 0.02;
            Y = 0.05;

            OP = new Bitmap(image);
            RP = new Bitmap(image);
            P1 = new Bitmap(image);
            P2 = new Bitmap(image);
            P3 = new Bitmap(image); 




            OR = new double[OP.Width, OP.Height];
            OG = new double[OP.Width, OP.Height];
            OB = new double[OP.Width, OP.Height];

            FinalPR = new double[OP.Width, OP.Height];
            FinalPG = new double[OP.Width, OP.Height];
            FinalPB = new double[OP.Width, OP.Height];

            firstPR = new double[OP.Width, OP.Height];
            firstPG = new double[OP.Width, OP.Height];
            firstPB = new double[OP.Width, OP.Height];

            secondPR = new double[OP.Width, OP.Height];
            secondPG = new double[OP.Width, OP.Height];
            secondPB = new double[OP.Width, OP.Height];
            

            thirdPR = new double[OP.Width, OP.Height];
            thirdPG = new double[OP.Width, OP.Height];
            thirdPB = new double[OP.Width, OP.Height];

            forthPR = new double[OP.Width, OP.Height];
            forthPG = new double[OP.Width, OP.Height];
            forthPB = new double[OP.Width, OP.Height];


            

            //--------初始化---------
            for (int i = 0; i < OP.Width; i++)
            {
                for (int j = 0; j < OP.Height; j++)
                {
                    Color color = OP.GetPixel(i, j);

                    OR[i, j] = color.R;
                    OG[i, j] = color.G;
                    OB[i, j] = color.B;

                    FinalPR[i, j] = color.R;
                    FinalPG[i, j] = color.G;
                    FinalPB[i, j] = color.B;

                    firstPR[i, j] = color.R;
                    firstPG[i, j] = color.G;
                    firstPB[i, j] = color.B;

                    secondPR[i, j] = color.R;
                    secondPG[i, j] = color.G;
                    secondPB[i, j] = color.B;

                    thirdPR[i, j] = color.R;
                    thirdPG[i, j] = color.G;
                    thirdPB[i, j] = color.B;

                    forthPR[i, j] = color.R;
                    forthPG[i, j] = color.G;
                    forthPB[i, j] = color.B;
                }
            }

            //---------------------gauss呼叫----------------------------

            Gauss_();


            //---------------------secondP做濾波------------------------ 
            
          for (int k = 0; k < 200; k++)
          {
                if (standard == false)
                {

                    for (int i = 2; i < OP.Width - 2; i++)
                    {
                        for (int j = 2; j < OP.Height - 2; j++)
                        {


                            secondPR[i, j] = (secondPR[i - 2, j - 2] * Gauss[0, 0] + secondPR[i - 1, j - 2] * Gauss[1, 0] + secondPR[i, j - 2] * Gauss[2, 0] + secondPR[i + 1, j - 2] * Gauss[3, 0] + secondPR[i + 2, j - 2] * Gauss[4, 0] +
                                              secondPR[i - 2, j - 1] * Gauss[0, 1] + secondPR[i - 1, j - 1] * Gauss[1, 1] + secondPR[i, j - 1] * Gauss[2, 1] + secondPR[i + 1, j - 1] * Gauss[3, 1] + secondPR[i + 2, j - 1] * Gauss[4, 1] +
                                              secondPR[i - 2, j] * Gauss[0, 2] + secondPR[i - 1, j] * Gauss[1, 2] + secondPR[i, j] * Gauss[2, 2] + secondPR[i + 1, j] * Gauss[3, 2] + secondPR[i + 2, j] * Gauss[4, 2] +
                                              secondPR[i - 2, j + 1] * Gauss[0, 3] + secondPR[i - 1, j + 1] * Gauss[1, 3] + secondPR[i, j + 1] * Gauss[2, 3] + secondPR[i + 1, j + 1] * Gauss[3, 3] + secondPR[i + 2, j + 1] * Gauss[4, 3] +
                                              secondPR[i - 2, j + 2] * Gauss[0, 4] + secondPR[i - 1, j + 2] * Gauss[1, 4] + secondPR[i, j + 2] * Gauss[2, 4] + secondPR[i + 1, j + 2] * Gauss[3, 4] + secondPR[i + 2, j + 2] * Gauss[4, 4]
                                              ) / total;

                            secondPG[i, j] = (secondPG[i - 2, j - 2] * Gauss[0, 0] + secondPG[i - 1, j - 2] * Gauss[1, 0] + secondPG[i, j - 2] * Gauss[2, 0] + secondPG[i + 1, j - 2] * Gauss[3, 0] + secondPG[i + 2, j - 2] * Gauss[4, 0] +
                                              secondPG[i - 2, j - 1] * Gauss[0, 1] + secondPG[i - 1, j - 1] * Gauss[1, 1] + secondPG[i, j - 1] * Gauss[2, 1] + secondPG[i + 1, j - 1] * Gauss[3, 1] + secondPG[i + 2, j - 1] * Gauss[4, 1] +
                                              secondPG[i - 2, j] * Gauss[0, 2] + secondPG[i - 1, j] * Gauss[1, 2] + secondPG[i, j] * Gauss[2, 2] + secondPG[i + 1, j] * Gauss[3, 2] + secondPG[i + 2, j] * Gauss[4, 2] +
                                              secondPG[i - 2, j + 1] * Gauss[0, 3] + secondPG[i - 1, j + 1] * Gauss[1, 3] + secondPG[i, j + 1] * Gauss[2, 3] + secondPG[i + 1, j + 1] * Gauss[3, 3] + secondPG[i + 2, j + 1] * Gauss[4, 3] +
                                              secondPG[i - 2, j + 2] * Gauss[0, 4] + secondPG[i - 1, j + 2] * Gauss[1, 4] + secondPG[i, j + 2] * Gauss[2, 4] + secondPG[i + 1, j + 2] * Gauss[3, 4] + secondPG[i + 2, j + 2] * Gauss[4, 4]
                                              ) / total;

                            secondPB[i, j] = (secondPB[i - 2, j - 2] * Gauss[0, 0] + secondPB[i - 1, j - 2] * Gauss[1, 0] + secondPB[i, j - 2] * Gauss[2, 0] + secondPB[i + 1, j - 2] * Gauss[3, 0] + secondPB[i + 2, j - 2] * Gauss[4, 0] +
                                              secondPB[i - 2, j - 1] * Gauss[0, 1] + secondPB[i - 1, j - 1] * Gauss[1, 1] + secondPB[i, j - 1] * Gauss[2, 1] + secondPB[i + 1, j - 1] * Gauss[3, 1] + secondPB[i + 2, j - 1] * Gauss[4, 1] +
                                              secondPB[i - 2, j] * Gauss[0, 2] + secondPB[i - 1, j] * Gauss[1, 2] + secondPB[i, j] * Gauss[2, 2] + secondPB[i + 1, j] * Gauss[3, 2] + secondPB[i + 2, j] * Gauss[4, 2] +
                                              secondPB[i - 2, j + 1] * Gauss[0, 3] + secondPB[i - 1, j + 1] * Gauss[1, 3] + secondPB[i, j + 1] * Gauss[2, 3] + secondPB[i + 1, j + 1] * Gauss[3, 3] + secondPB[i + 2, j + 1] * Gauss[4, 3] +
                                              secondPB[i - 2, j + 2] * Gauss[0, 4] + secondPB[i - 1, j + 2] * Gauss[1, 4] + secondPB[i, j + 2] * Gauss[2, 4] + secondPB[i + 1, j + 2] * Gauss[3, 4] + secondPB[i + 2, j + 2] * Gauss[4, 4]
                                              ) / total;
                            // RP.SetPixel(i, j, Color.FromArgb((int)(secondPR[i, j]), (int)(secondPG[i, j]), (int)(secondPB[i, j])));
                        }
                    }



                    for (int i = 1; i < OP.Width - 1; i = i + 2)
                    {
                        for (int j = 1; j < OP.Height - 1; j = j + 2)
                        {
                            secondPR[i + 1, j] = secondPR[i, j];
                            secondPR[i, j + 1] = secondPR[i, j];
                            secondPR[i + 1, j + 1] = secondPR[i, j];

                            secondPG[i + 1, j] = secondPG[i, j];
                            secondPG[i, j + 1] = secondPG[i, j];
                            secondPG[i + 1, j + 1] = secondPG[i, j];


                            secondPB[i + 1, j] = secondPB[i, j];
                            secondPB[i, j + 1] = secondPB[i, j];
                            secondPB[i + 1, j + 1] = secondPB[i, j];


                        }

                    }


                    for (int i = 1; i < OP.Width - 1; i++)
                    {
                        for (int j = 1; j < OP.Height - 1; j++)
                        {


                            secondPR[i, j] = (secondPR[i - 1, j - 1] + secondPR[i, j - 1] * 2 + secondPR[i + 1, j - 1] +
                                              secondPR[i - 1, j] * 2 + secondPR[i, j] * 4 + secondPR[i + 1, j] * 2 +
                                              secondPR[i - 1, j + 1] + secondPR[i, j + 1] * 2 + secondPR[i + 1, j + 1]) / 16;

                            secondPG[i, j] = (secondPG[i - 1, j - 1] + secondPG[i, j - 1] * 2 + secondPG[i + 1, j - 1] +
                                              secondPG[i - 1, j] * 2 + secondPG[i, j] * 4 + secondPG[i + 1, j] * 2 +
                                              secondPG[i - 1, j + 1] + secondPG[i, j + 1] * 2 + secondPG[i + 1, j + 1]) / 16;

                            secondPB[i, j] = (secondPB[i - 1, j - 1] + secondPB[i, j - 1] * 2 + secondPB[i + 1, j - 1] +
                                              secondPB[i - 1, j] * 2 + secondPB[i, j] * 4 + secondPB[i + 1, j] * 2 +
                                              secondPB[i - 1, j + 1] + secondPB[i, j + 1] * 2 + secondPB[i + 1, j + 1]) / 16;

                            //RP.SetPixel(i, j, Color.FromArgb((int)(secondPR[i, j]), (int)(secondPG[i, j]), (int)(secondPB[i, j])));
                        }
                    }

                    //pictureBox2.Image = RP;

                    //------------------------------濾波後做二階導數-----------------------------------------


                    for (int i = 1; i < OP.Width - 1; i++)
                    {
                        for (int j = 1; j < OP.Height - 1; j++)
                        {


                            thirdPR[i, j] = (thirdPR[i - 1, j - 1] + thirdPR[i, j - 1] * 2 + thirdPR[i + 1, j - 1] +
                                              thirdPR[i - 1, j] * 2 + thirdPR[i, j] * 4 + thirdPR[i + 1, j] * 2 +
                                              thirdPR[i - 1, j + 1] + thirdPR[i, j + 1] * 2 + thirdPR[i + 1, j + 1]) / 16;

                            thirdPG[i, j] = (thirdPG[i - 1, j - 1] + thirdPG[i, j - 1] * 2 + thirdPG[i + 1, j - 1] +
                                              thirdPG[i - 1, j] * 2 + thirdPG[i, j] * 4 + thirdPG[i + 1, j] * 2 +
                                              thirdPG[i - 1, j + 1] + thirdPG[i, j + 1] * 2 + thirdPG[i + 1, j + 1]) / 16;

                            thirdPB[i, j] = (thirdPB[i - 1, j - 1] + thirdPB[i, j - 1] * 2 + thirdPB[i + 1, j - 1] +
                                              thirdPB[i - 1, j] * 2 + thirdPB[i, j] * 4 + thirdPB[i + 1, j] * 2 +
                                              thirdPB[i - 1, j + 1] + thirdPB[i, j + 1] * 2 + thirdPB[i + 1, j + 1]) / 16;


                        }
                    }





                    for (int i = 1; i < OP.Width - 1; i++)
                    {
                        for (int j = 1; j < OP.Height - 1; j++)
                        {



                            thirdPR[i, j] = thirdPR[i - 1, j - 1] * -1 + thirdPR[i, j - 1] * -1 + thirdPR[i + 1, j - 1] * -1 +
                                            thirdPR[i - 1, j] * -1 + thirdPR[i, j] * 8 + thirdPR[i + 1, j] * -1 +
                                            thirdPR[i - 1, j + 1] * -1 + thirdPR[i, j + 1] * -1 + thirdPR[i + 1, j + 1] * -1;

                            thirdPG[i, j] = thirdPG[i - 1, j - 1] * -1 + thirdPG[i, j - 1] * -1 + thirdPG[i + 1, j - 1] * -1 +
                                           thirdPG[i - 1, j] * -1 + thirdPG[i, j] * 8 + thirdPG[i + 1, j] * -1 +
                                           thirdPG[i - 1, j + 1] * -1 + thirdPG[i, j + 1] * -1 + thirdPG[i + 1, j + 1] * -1;

                            thirdPB[i, j] = thirdPB[i - 1, j - 1] * -1 + thirdPB[i, j - 1] * -1 + thirdPB[i + 1, j - 1] * -1 +
                                           thirdPB[i - 1, j] * -1 + thirdPB[i, j] * 8 + thirdPB[i + 1, j] * -1 +
                                           thirdPB[i - 1, j + 1] * -1 + thirdPB[i, j + 1] * -1 + thirdPB[i + 1, j + 1] * -1;

                            if (thirdPR[i, j] <= 0)
                            {
                                thirdPR[i, j] = 0;
                            }
                            if (thirdPG[i, j] <= 0)
                            {
                                thirdPG[i, j] = 0;
                            }
                            if (thirdPB[i, j] <= 0)
                            {
                                thirdPB[i, j] = 0;
                            }
                            if (thirdPR[i, j] >= 255)
                            {
                                thirdPR[i, j] = 255;
                            }
                            if (thirdPG[i, j] >= 255)
                            {
                                thirdPG[i, j] = 255;
                            }
                            if (thirdPB[i, j] >= 255)
                            {
                                thirdPB[i, j] = 255;
                            }

                            // RP.SetPixel(i, j, Color.FromArgb((int)(thirdPR[i, j]), (int)(thirdPG[i, j]), (int)(thirdPB[i, j])));
                        }
                    }

                    //----------------------------------做二階導數--------------------------------------
                    for (int i = 1; i < OP.Width - 1; i++)
                    {
                        for (int j = 1; j < OP.Height - 1; j++)
                        {


                            forthPR[i, j] = forthPR[i - 1, j - 1] * -1 + forthPR[i, j - 1] * -1 + forthPR[i + 1, j - 1] * -1 +
                            forthPR[i - 1, j] * -1 + forthPR[i, j] * 8 + forthPR[i + 1, j] * -1 +
                            forthPR[i - 1, j + 1] * -1 + forthPR[i, j + 1] * -1 + forthPR[i + 1, j + 1] * -1;

                            forthPG[i, j] = forthPG[i - 1, j - 1] * -1 + forthPG[i, j - 1] * -1 + forthPG[i + 1, j - 1] * -1 +
                            forthPG[i - 1, j] * -1 + forthPG[i, j] * 8 + forthPG[i + 1, j] * -1 +
                            forthPG[i - 1, j + 1] * -1 + forthPG[i, j + 1] * -1 + forthPG[i + 1, j + 1] * -1;

                            forthPB[i, j] = forthPB[i - 1, j - 1] * -1 + forthPB[i, j - 1] * -1 + forthPB[i + 1, j - 1] * -1 +
                            forthPB[i - 1, j] * -1 + forthPB[i, j] * 8 + forthPB[i + 1, j] * -1 +
                            forthPB[i - 1, j + 1] * -1 + forthPB[i, j + 1] * -1 + forthPB[i + 1, j + 1] * -1;


                            if (forthPR[i, j] <= 0)
                            {
                                forthPR[i, j] = 0;
                            }
                            if (forthPG[i, j] <= 0)
                            {
                                forthPG[i, j] = 0;
                            }
                            if (forthPB[i, j] <= 0)
                            {
                                forthPB[i, j] = 0;
                            }
                            if (forthPR[i, j] >= 255)
                            {
                                forthPR[i, j] = 255;
                            }
                            if (forthPG[i, j] >= 255)
                            {
                                forthPG[i, j] = 255;
                            }
                            if (forthPB[i, j] >= 255)
                            {
                                forthPB[i, j] = 255;
                            }
                            //  RP.SetPixel(i, j, Color.FromArgb((int)(forthPR[i, j]), (int)(forthPG[i, j]), (int)(forthPB[i, j])));
                        }
                    }

                    //-----------------------------------------------------------------------------


                    for (int i = 1; i < OP.Width - 1; i++)
                    {
                        for (int j = 1; j < OP.Height - 1; j++)
                        {

                            FinalPR[i, j] = firstPR[i, j] + X * (OR[i, j] - secondPR[i, j] + Y * (thirdPR[i, j] - forthPR[i, j]));
                            FinalPG[i, j] = firstPG[i, j] + X * (OG[i, j] - secondPG[i, j] + Y * (thirdPG[i, j] - forthPG[i, j]));
                            FinalPB[i, j] = firstPB[i, j] + X * (OB[i, j] - secondPB[i, j] + Y * (thirdPB[i, j] - forthPB[i, j]));


                            if (FinalPR[i, j] <= 0)
                            {
                                FinalPR[i, j] = 0;
                            }
                            if (FinalPG[i, j] <= 0)
                            {
                                FinalPG[i, j] = 0;
                            }
                            if (FinalPB[i, j] <= 0)
                            {
                                FinalPB[i, j] = 0;
                            }
                            if (FinalPR[i, j] >= 255)
                            {
                                FinalPR[i, j] = 255;
                            }
                            if (FinalPG[i, j] >= 255)
                            {
                                FinalPG[i, j] = 255;
                            }
                            if (FinalPB[i, j] >= 255)
                            {
                                FinalPB[i, j] = 255;
                            }

                            RP.SetPixel(i, j, Color.FromArgb((int)(FinalPR[i, j]), (int)(FinalPG[i, j]), (int)(FinalPB[i, j])));


                            if (k < 2)
                            {
                                P1.SetPixel(i, j, Color.FromArgb((int)(FinalPR[i, j]), (int)(FinalPG[i, j]), (int)(FinalPB[i, j])));
                            }
                            if (k < 50)
                            {
                                P2.SetPixel(i, j, Color.FromArgb((int)(FinalPR[i, j]), (int)(FinalPG[i, j]), (int)(FinalPB[i, j])));
                            }
                            if (k < 100)
                            {
                                P3.SetPixel(i, j, Color.FromArgb((int)(FinalPR[i, j]), (int)(FinalPG[i, j]), (int)(FinalPB[i, j])));
                            }

                        }
                    }


                    Console.WriteLine(k);

                    /*
                    if (k == 1)
                    {

                        fr3.form3_Show_image(P1);
                        Console.WriteLine("Don!    fr3.form3_Show_image(RP);");
                    }
                    if (k == 2)
                    {

                        fr3.form3_Show_image1(P2);
                        Console.WriteLine("Don!    fr3.form3_Show_image1(RP);");
                    }
                    if (k == 3)
                    {

                        fr3.form3_Show_image2(P3);
                        Console.WriteLine("Don!    fr3.form3_Show_image2(RP);");
                    }*/

                    fr3.form3_Show_image(P1);
                    fr3.form3_Show_image1(P2);
                    fr3.form3_Show_image2(P3);




                    


                    for (int i = 0; i < OP.Width; i++)
                    {
                        for (int j = 0; j < OP.Height; j++)
                        {
                            totalR += Math.Abs( firstPR[i, j] - FinalPR[i, j]);
                            totalG += Math.Abs(firstPG[i, j] - FinalPG[i, j]);
                            totalB += Math.Abs(firstPB[i, j] - FinalPB[i, j]);
                        }
                    }

                    totalR = totalR / (OP.Width * OP.Height);
                    totalG = totalG / (OP.Width * OP.Height);
                    totalB = totalB / (OP.Width * OP.Height);
                    
                    //Console.WriteLine("totalR =" + totalR);
                   // Console.WriteLine("totalG =" + totalG);
                    //Console.WriteLine("totalB =" + totalB);
                    
                    if (totalR < 0.1 && totalG < 0.1 && totalB < 0.1)
                    {
                        standard = true;
                        Console.WriteLine("change");
                    }





                    for (int i = 0; i < OP.Width; i++)
                    {
                        for (int j = 0; j < OP.Height; j++)
                        {
                            firstPR[i, j] = FinalPR[i, j];
                            firstPG[i, j] = FinalPG[i, j];
                            firstPB[i, j] = FinalPB[i, j];

                            secondPR[i, j] = FinalPR[i, j];
                            secondPG[i, j] = FinalPG[i, j];
                            secondPB[i, j] = FinalPB[i, j];
                            /*
                            thirdPR[i, j] = FinalPR[i, j];
                            thirdPG[i, j] = FinalPG[i, j];
                            thirdPB[i, j] = FinalPB[i, j];

                            forthPR[i, j] = FinalPR[i, j];
                            forthPG[i, j] = FinalPG[i, j];
                            forthPB[i, j] = FinalPB[i, j];*/
                        }
                    }





                }



                pictureBox2.Image = RP;
            }
            fr3.Show();
            Console.WriteLine("totalR =" + totalR);
            Console.WriteLine("totalG =" + totalG);
            Console.WriteLine("totalB =" + totalB);
}

        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            B = OP;
            F = RP;

            Bitmap NP = new Bitmap(128, 128);
            Bitmap FP = new Bitmap(128, 128);


          


            Console.WriteLine("滑鼠點擊X = " + e.X);
            Console.WriteLine("滑鼠點擊Y = " + e.Y);

            XX = e.X-32 ;
            YY = e.Y-32 ;




            try {
                for (int i = 0; i < 128; i++)
                {
                    for (int j = 0; j < 128; j++)
                    {
                        Color color1 = B.GetPixel(XX, YY);
                        if(i==0&&j==0)
                        Console.WriteLine("改變[" + XX + "," + YY + "]  color R=" + color1.R + "color G = " + color1.G + "color B = " + color1.B);
                        NP.SetPixel(i, j, Color.FromArgb((int)(color1.R), (int)(color1.G), (int)(color1.B)));
                        Color color2 = F.GetPixel(XX, YY);
                        if (i == 0 && j == 0)
                        Console.WriteLine("改變[" + XX + "," + YY + "]  color R=" + color2.R + "color G = " + color2.G + "color B = " + color2.B);
                        FP.SetPixel(i, j, Color.FromArgb((int)(color2.R), (int)(color2.G), (int)(color2.B)));
                        if (j % 2 == 0)
                        {
                            YY++;
                        }
                    }
                    if(i%2 == 0)
                    {
                        XX++;
                    }
                    YY = e.Y-32 ;
                }
                
            }
            catch(System.NullReferenceException)
            {
                Console.WriteLine("出現問題");
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Console.WriteLine("出現問題");
            }


            fr2.Show_image1(NP);
            fr2.Show_image2(FP);

            
            fr2.Show();
           
           

            Console.WriteLine("Don!");

        }


        private void Form2_formClosed()
        {

        }

        



    }
}
