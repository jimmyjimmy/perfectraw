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
        private string imgFileName;

        public Form2()
        {
            InitializeComponent();
        }
        ~Form2()
        {
            if (img != null) img.Dispose();
            if (imgOld != null) imgOld.Dispose();
        }
        private ushort CLIPF(double v)
        {
            if (v > 65535.0) v = 65535.0;
            if (v < 0) v = 0.0;
            return (ushort)v;
        }
        public void LoadImageFromFile(string imgFileName)
        {
            this.imgFileName = imgFileName;
            if( img != null) {
                perfectView1.Bitmap=null;
                perfectView2.Bitmap=null;
                img.Dispose();
                img=null;
            }
            if( imgOld != null ) {
                perfectView2.OldBitmap=null;
                perfectView2.OldBitmap= null;
                imgOld.Dispose();
                imgOld= null;
            }
            img = (Bitmap)Bitmap.FromFile(imgFileName);
            //Make some changes to the original image to appreciate the split view working mode.
            BitmapData data = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format48bppRgb);
            //Get number of extra dummy bytes in a row pointer.
            int extra = data.Stride - data.Width * 6;
            ushort* ptr2bk = (ushort*)(data.Scan0);
            int w = data.Width;
            int h = data.Height;
            int i, j;
            unsafe
            {
                for (i = 0; i < h; i++)
                {
                    //ptr2bk += extra;
                    for (j = 0; j < w; j++)
                    {
                        ptr2bk[0] = CLIPF(65535.0 * 2.8 * Math.Pow((double)ptr2bk[0] / 65535.0, 1 / 2.2));
                        ptr2bk[1] = CLIPF(65535.0 * 2.8 * Math.Pow((double)ptr2bk[1] / 65535.0, 1 / 2.2));
                        ptr2bk[2] = CLIPF(65535.0 * 2.8 * Math.Pow((double)ptr2bk[2] / 65535.0, 1 / 2.2));
                        ptr2bk += 3;
                    }
                }
            }
            img.UnlockBits(data);
            perfectView1.Bitmap = img;

            imgOld = (Bitmap)Bitmap.FromFile(@"imgPrueba.tif");
            data = imgOld.LockBits(new Rectangle(0, 0, imgOld.Width, imgOld.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format48bppRgb);
            //Get number of extra dummy bytes in a row pointer.
            extra = data.Stride - data.Width * 6;
            ptr2bk = (ushort*)(data.Scan0);
            w = data.Width;
            h = data.Height;
            unsafe
            {
                for (i = 0; i < h; i++)
                {
                    //ptr2bk += extra;
                    for (j = 0; j < w; j++)
                    {
                        ptr2bk[0] = CLIPF(65535.0 * 2.8 * Math.Pow((double)ptr2bk[0] / 65535.0, 1 / 1.8));
                        ptr2bk[1] = CLIPF(65535.0 * 2.8 * Math.Pow((double)ptr2bk[1] / 65535.0, 1 / 1.8));
                        ptr2bk[2] = CLIPF(65535.0 * 2.8 * Math.Pow((double)ptr2bk[2] / 65535.0, 1 / 1.8));
                        ptr2bk += 3;
                    }
                }
            }
            imgOld.UnlockBits(data);

            perfectView2.Bitmap = img;
            perfectView2.OldBitmap = imgOld;
            ResizeViews();            

        }
        private void Form2_Load(object sender, EventArgs e)
        {
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
            perfectView1.SpeedTest();
        }

        private void showBurnedPixels_CheckedChanged(object sender, EventArgs e)
        {
            if (showBurnedPixels.CheckState == CheckState.Checked)
            {
                perfectView1.ShowBurnedPixels = true;
                perfectView2.ShowBurnedPixels = true;
            }
            else
            {
                perfectView1.ShowBurnedPixels = false;
                perfectView2.ShowBurnedPixels = false;
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
        private void SelectImageToOpen()
        {
            OpenFileDialog dlgFichero = new OpenFileDialog();
            dlgFichero.ShowReadOnly = true;
            dlgFichero.Filter = "Imágenes Tiff 16bit (*.tif)|*.tif";
            if (dlgFichero.ShowDialog() != DialogResult.OK) Application.Exit();
            LoadImageFromFile(dlgFichero.FileName);
        }
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            SelectImageToOpen();
        }
    }
}