using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace test
{
    public unsafe partial class Form2 : Form
    {
        [DllImport(@"PerfectImageDLL.dll")]
        static extern void FastDrawImage(byte* buffer, byte* buffer2, int width, int height, int width2, int heigh2, int extra, int extra2, int x, int y, float z, byte color);
        private const int WHEEL_DELTA = 120;

        public Form2()
        {
            InitializeComponent();
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            this.pictureBox1.Bitmap = (Bitmap)Bitmap.FromFile(@"salón.jpg");
            this.pictureBox2.Bitmap = (Bitmap)Bitmap.FromFile(@"salón.jpg");
           
            ResizeViews();            
        }                        

        private void Form2_OnResize(object sender, EventArgs e)
        {
            ResizeViews();            
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            Control ctrlOnFocus = this.GetChildAtPoint(e.Location);
            if (!(ctrlOnFocus is PerfectControls.PerfectView))return;
         
            PerfectControls.PerfectView pv= (PerfectControls.PerfectView)ctrlOnFocus;
            if (e.Delta == WHEEL_DELTA)
            {
                pv.MakeZoom(2);
            }

            if (e.Delta == -WHEEL_DELTA)
            {
                pv.MakeZoom(1 / 2F);
            }
            Refresh();
            base.OnMouseWheel(e);
        }

        private void ResizeViews()
        {
            int sw = this.Width;
            int sh = this.Height;
            
            pictureBox1.Width = sw / 2;
            pictureBox2.Width = sw - sw / 2;
            pictureBox2.Left = sw - pictureBox2.Width;
            pictureBox1.Height = pictureBox2.Height = sh - 250;
        }

        private void PictureBox1_OnMouseHover(object sender, EventArgs e)
        {
            label1.Text = "0";
        }

        private void PictureBox2_OnMouseHover(object sender, EventArgs e)
        {
            label1.Text = "1";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Tests de velocidad
            pictureBox1.TestVelocidad();
        }
    }
}