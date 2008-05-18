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
        private const int WHEEL_DELTA = 120;
        protected Bitmap img, imgOld;

        public Form2()
        {
            InitializeComponent();
        }
        ~Form2()
        {
            if (img != null) img.Dispose();
            if (imgOld != null) imgOld.Dispose();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            img = (Bitmap)Bitmap.FromFile(@"salón.jpg");
            imgOld= (Bitmap)Bitmap.FromFile(@"salón.jpg");
            //Make some changes to the original image to appreciate the split view working mode.
            perfectView2.OldBitmap = imgOld;
            BitmapData data = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            int extra = data.Stride - data.Width * 3;
            byte *ptr2bk = (byte*)(data.Scan0);
            int w = data.Width;
            int h = data.Height;
            unsafe
            {
                for (int i = 0; i < (w + extra) * h * 3; i++)
                {
                    if (ptr2bk[i] < 225) ptr2bk[i] += 30;
                }
            }
            img.UnlockBits(data);
            perfectView2.Bitmap = img;
            
            perfectView1.Bitmap = img;
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
            
            perfectView1.Width = sw / 2;
            perfectView2.Width = sw - sw / 2;
            perfectView2.Left = sw - perfectView2.Width;
            perfectView1.Height = perfectView2.Height = sh - 250;
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
            perfectView1.TestVelocidad();
        }

        private void showBurnedPixels_CheckedChanged(object sender, EventArgs e)
        {
            if (showBurnedPixels.CheckState == CheckState.Checked)
            {
                perfectView1.ShowBurnedPixels = true;
            }
            else
            {
                perfectView1.ShowBurnedPixels = false;
            }
        }

        private void rbVistaSimple_CheckedChanged(object sender, EventArgs e)
        {
            perfectView2.ViewMode = PerfectControls.PerfectViewMode.OneView;
        }

        private void rbVistaVertical_CheckedChanged(object sender, EventArgs e)
        {
            perfectView2.ViewMode = PerfectControls.PerfectViewMode.VerticalSplit;
        }

        private void rbVistaHorizontal_CheckedChanged(object sender, EventArgs e)
        {
            perfectView2.ViewMode = PerfectControls.PerfectViewMode.HorizontalSplit;
        }
    }
}