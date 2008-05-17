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
    public unsafe partial class Form1 : Form
    {
        [DllImport(@"PerfectImageDLL.dll")]
        static extern void FastDrawImage(byte * buffer, byte* buffer2, int width, int height, int width2, int heigh2, int extra, int extra2, int x, int y, float z, byte color);

        int []state=new int[2];
        int []dragFromX=new int[2];
        int []dragFromY=new int[2];
        int view=1;
        int []pX = new int[2];
        int []pY = new int[2];
        private const int WHEEL_DELTA = 120;
        float []Zoom = new float[2];        
        Bitmap img1 = (Bitmap)Image.FromFile(@"salón.jpg");
        Bitmap img2 = (Bitmap)Image.FromFile(@"salón.jpg");
        BitmapData data21;
        BitmapData data22;
        byte* ptr2bk1;
        byte* ptr2bk2;
        int []w=new int[2];
        int []h=new int[2];
        int w21, h21;
        int w22, h22;
        int extra21;
        int extra22;
        bool flag=false;
        bool []bref=new bool[2];

        public Form1()
        {
            InitializeComponent();
            Zoom[0] = Zoom[1] = 1;
            state[0] = state[1] = 0;
            pX[0] = pX[1] = pY[0] = pY[1] = 0;
        }

        ~Form1()
        {
            while (flag) ;
            img1.UnlockBits(data21);
            img2.UnlockBits(data22);
        }

        private void Form1_Load(object sender, EventArgs e)
        {                        
            data21 = img1.LockBits(new Rectangle(0, 0, img1.Width, img1.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            extra21 = data21.Stride - data21.Width * 3;
            ptr2bk1 = (byte*)(data21.Scan0);
            w21 = data21.Width;
            h21 = data21.Height;

            data22 = img2.LockBits(new Rectangle(0, 0, img2.Width, img2.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            extra22 = data22.Stride - data22.Width * 3;
            ptr2bk2 = (byte*)(data22.Scan0);
            w22 = data22.Width;
            h22 = data22.Height;
            
            ResizeViews();            
        }                        

        private void Form1_OnResize(object sender, EventArgs e)
        {
            ResizeViews();            
        }

        private void ResizeViews()
        {
            int sw = this.Width;
            int sh = this.Height;
            
            pictureBox1.Width = sw / 2;
            pictureBox2.Width = sw - sw / 2;
            pictureBox2.Left = sw - pictureBox2.Width;
            pictureBox1.Height = pictureBox2.Height = sh - 250;
            w[0] = sw / 2;
            w[1] = sw - sw / 2;
            h[0] =h[1]= sh - 250;
            bref[0] = bref[1] = true;
            this.Invalidate();            
        }

        private void PictureBox1_OnMouseHover(object sender, EventArgs e)
        {
            view = 0;
            label1.Text = "0";
        }

        private void PictureBox2_OnMouseHover(object sender, EventArgs e)
        {
            view = 1;
            label1.Text = "1";
        }

        private void PictureBox1_OnMouseDown(object sender, MouseEventArgs e)
        {
            view = 0;
            if (e.Button == MouseButtons.Left)
            {
                state[view] = 1;
                dragFromX[view] = pX[view] + (int)(e.X / Zoom[view]);
                dragFromY[view] = pY[view] + (int)(e.Y / Zoom[view]);
                this.Cursor = Cursors.NoMove2D;                
            }
        }

        private void PictureBox1_OnMouseMove(object sender, MouseEventArgs e)
        {            
            PictureBox p = (PictureBox)sender;

            view = 0;
            if (state[view] == 1)
            {
                pX[view] = dragFromX[view] - (int)(e.X / Zoom[view]);
                pY[view] = dragFromY[view] - (int)(e.Y / Zoom[view]);
                if (pX[view] < 0) pX[view] = 0;
                if (pY[view] < 0) pY[view] = 0;
                if (pX[view] > w21 - (int)(w[view] / Zoom[view])) pX[view] = w21 - (int)(w[view] / Zoom[view]);
                if (pY[view] > h21 - (int)(h[view] / Zoom[view])) pY[view] = h21 - (int)(h[view] / Zoom[view]);
                bref[view] = true;
                this.Refresh();
            }
        }

        private void PictureBox1_OnMouseUp(object sender, MouseEventArgs e)
        {
            view = 0;
            if ((e.Button == MouseButtons.Left) && (state[view] == 1))
            {
                state[view] = 0;
                this.Cursor = Cursors.Hand;
            }
        }

        private void PictureBox2_OnMouseDown(object sender, MouseEventArgs e)
        {
            view = 1;
            if (e.Button == MouseButtons.Left)
            {
                state[view] = 1;
                dragFromX[view] = pX[view] + (int)(e.X / Zoom[view]);
                dragFromY[view] = pY[view] + (int)(e.Y / Zoom[view]);
                this.Cursor = Cursors.NoMove2D;
            }            
        }

        private void PictureBox2_OnMouseMove(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;

            view = 1;
            if (state[view] == 1)
            {
                pX[view] = dragFromX[view] - (int)(e.X / Zoom[view]);
                pY[view] = dragFromY[view] - (int)(e.Y / Zoom[view]);
                if (pX[view] < 0) pX[view] = 0;
                if (pY[view] < 0) pY[view] = 0;
                if (pX[view] > w22 - (int)(w[view] / Zoom[view])) pX[view] = w22 - (int)(w[view] / Zoom[view]);
                if (pY[view] > h22 - (int)(h[view] / Zoom[view])) pY[view] = h22 - (int)(h[view] / Zoom[view]);
                bref[view] = true;
                this.Refresh();
            }
        }

        private void PictureBox2_OnMouseUp(object sender, MouseEventArgs e)
        {
            view = 1;
            if ((e.Button == MouseButtons.Left) && (state[view] == 1))
            {
                state[view] = 0;
                this.Cursor = Cursors.Hand;
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {            

            if (e.Delta == WHEEL_DELTA)
            {
                MakeZoom(2);
            }

            if (e.Delta == -WHEEL_DELTA)
            {
                MakeZoom(1/2F);
            }
            Refresh();
            base.OnMouseWheel(e);
        }

        private void MakeZoom(float ZoomQ)
        {            
            int bX, bY;
            int w2, h2;
            if (view == 0)
            {
                w2 = w21;
                h2 = h21;
            }
            else
            {
                w2 = w22;
                h2 = h22;
            }
            bX = (int)(pX[view]+ w[view] / (2*Zoom[view]));
            bY = (int)(pY[view]+ h[view] / (2*Zoom[view]) );
            
            Zoom[view] *= ZoomQ;
            if (Zoom[view] > 8) Zoom[view] = 8;
            if (Zoom[view] < 1/ 32F) Zoom[view] = 1 / 32F;

            pX[view] = (int)(bX - w[view] / (2 * Zoom[view]) + 1);
            pY[view] = (int)(bY - h[view] / (2 * Zoom[view]) + 1);
            if (pX[view] < 0) pX[view] = 0;
            if (pY[view] < 0) pY[view] = 0;
            if (pX[view] > w2 - (int)(w[view] / Zoom[view])) pX[view] = w2 - (int)(w[view] / Zoom[view]);
            if (pY[view] > h2 - (int)(h[view] / Zoom[view])) pY[view] = h2 - (int)(h[view] / Zoom[view]);
            bref[view] = true;
        }

        private void Form1_OnPaint(object sender, PaintEventArgs e)
        {
            // La DLL no es reentrante, así que hay que evitar que colapsen las llamadas a ella
            // aunque sí debería serlo, ¿no?
            if (flag == false)
            {
                flag = true;
                
                int aview = 0;
                if (bref[aview])
                {                    
                    Bitmap bmp = new Bitmap(w[aview], h[aview], System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    BitmapData data1 = bmp.LockBits(new Rectangle(0, 0, w[aview], h[aview]), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    int extra = data1.Stride - data1.Width * 3;
                    unsafe
                    {
                        byte* ptr = (byte*)(data1.Scan0);
                        byte* ptr2 = ptr2bk1;
                        FastDrawImage(ptr, ptr2, w[aview], h[aview], w21, h21, extra, extra21, pX[aview], pY[aview], Zoom[aview], 128);
                    }
                    bmp.UnlockBits(data1);                    
                    pictureBox1.Image = bmp;                    
                    bref[aview] = false;
                }

                aview = 1;
                if (bref[aview])
                {
                    Bitmap bmp = new Bitmap(w[aview], h[aview], System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    BitmapData data1 = bmp.LockBits(new Rectangle(0, 0, w[aview], h[aview]), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    int extra = data1.Stride - data1.Width * 3;
                    unsafe
                    {
                        byte* ptr = (byte*)(data1.Scan0);
                        byte* ptr2 = ptr2bk2;
                        FastDrawImage(ptr, ptr2, w[aview], h[aview], w22, h22, extra, extra22, pX[aview], pY[aview], Zoom[aview], 128);
                    }
                    bmp.UnlockBits(data1);
                    pictureBox2.Image = bmp;
                    bref[aview] = false;
                }
                flag = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            while (flag) ;
            img1.UnlockBits(data21);
            img2.UnlockBits(data22);
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Tests de velocidad
            int aview = 1;

            Bitmap bmp = new Bitmap(w[aview], h[aview], System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData data1 = bmp.LockBits(new Rectangle(0, 0, w[aview], h[aview]), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            int extra = data1.Stride - data1.Width * 3;

            long t1, t2;
            float t;
            t1 = Environment.TickCount;
            for (int i = 0; i < 1000; i++)
            {
                unsafe
                {
                    byte* ptr = (byte*)(data1.Scan0);
                    byte* ptr2 = ptr2bk1;
                    FastDrawImage(ptr, ptr2, w[aview], h[aview], w21, h21, extra, extra21, pX[aview], pY[aview], 1/32F, 128);
                }
            }
            bmp.UnlockBits(data1);
            pictureBox1.Image = bmp;  
            t2 = Environment.TickCount;
            t = (t2 - t1) / 1000.0F;
            MessageBox.Show("1000 búcles + 1 volcado ("+(w21.ToString())+","+(h21.ToString())+"): " + t.ToString()+" segundos.");
        }
    }
}
