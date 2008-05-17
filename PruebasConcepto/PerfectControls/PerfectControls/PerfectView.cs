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

namespace PerfectControls
{
    public unsafe partial class PerfectView : PictureBox
    {
        //Incremento de la rueda necesario para producir un incremento
        //o reducción de zoom.
        Bitmap img;
        BitmapData dataImg;
        float zoom;
        int state, pX, pY;
        int dragFromX, dragFromY;
        byte* ptr2bk;
        int wDataImg, hDataImg;
        int extra;
        bool bref;
        [DllImport(@"PerfectImageDLL.dll")]
        static extern void FastDrawImage(byte* buffer, byte* buffer2, int width, int height, int width2, int heigh2, int extra, int extra2, int x, int y, float z, byte color);

        public PerfectView()
        {
            InitializeComponent();
            zoom = 1;
            state=0;
            pX=pY = 0;
        }
        ~PerfectView()
        {
            img.UnlockBits(dataImg);
            //Una vez hecho el unlock no debería de volver a utilizarse imgData.
            //Asegurarse de que se produzca una excepción si se utiliza.
            dataImg = null;
        }

        //Convertir Image en privada. La propiedad Image quedará sustituída
        //por Bitmap, ya que PerfecView está diseñada sólo para manejar imágenes
        //bitmap y no metafiles.
        private new Image Image;

        //Bitmap que almacena la imagen completa (antes de reescalar o recortar)
        //Puede ser estipulado también desde fichero utilizando 
        public Bitmap Bitmap
        {
            set
            {
                img = value;
                //¿No debería ser PixelFormat.Format48bppRgb o bien img.PixelFormat?
                dataImg = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), 
                    ImageLockMode.ReadOnly, 
                    System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                //Si el PixelFormat no es de 8bits no se debería de multiplicar por 3
                //sino por el número de bytes por pixels de la imagen img.PixelFormat/8
                extra = dataImg.Stride - dataImg.Width * 3;
                ptr2bk = (byte*)(dataImg.Scan0);
                wDataImg = dataImg.Width;
                hDataImg = dataImg.Height;

            }
            get
            {
                return img;
            }
        }
        public float Zoom
        {
            set
            {
                zoom = value;
                int bX, bY;
                bX = (int)(pX + Width / (2 * zoom));
                bY = (int)(pY + Height / (2 * zoom));

                //No permitir zooms mayores de ocho ni menores de 1/32
                if (zoom > 8) zoom = 8;
                if (zoom < 1 / 32F) zoom = 1 / 32F;

                pX = (int)(bX - Width / (2 * zoom) + 1);
                pY = (int)(bY - Height / (2 * zoom) + 1);
                if (pX < 0) pX = 0;
                if (pY < 0) pY = 0;
                if (pX > wDataImg - (int)(Width / zoom)) pX = wDataImg - (int)(Width / zoom);
                if (pY > hDataImg - (int)(Height / zoom)) pY = hDataImg - (int)(Height / zoom);
                bref = true;
            }
            get
            {
                return zoom;
            }

        }

        //Multiplica el factor de zoom actual por la cantidad que se pasa.
        //Devuelve el zoom actual una vez efectuado.
        public float MakeZoom(float zoomQ)
        {

            Zoom= zoom *zoomQ;
            return zoom;
        }
        public new string ImageLocation
        {
            set 
            {
                base.ImageLocation = value;
            }
            get
            {
                return base.ImageLocation;
            }
        }

        public new void Load()
        {
            base.Load();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                state = 1;
                dragFromX = pX + (int)(e.X / zoom);
                dragFromY = pY + (int)(e.Y / zoom);
                this.Cursor = Cursors.NoMove2D;
            }
            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (state == 1)
            {
                pX = dragFromX - (int)(e.X / zoom);
                pY = dragFromY - (int)(e.Y / zoom);
                if (pX < 0) pX = 0;
                if (pY < 0) pY = 0;
                if (pX > wDataImg - (int)(Width / zoom)) pX = wDataImg - (int)(Width / zoom);
                if (pY > hDataImg - (int)(Height / zoom)) pY = hDataImg - (int)(Height / zoom);
                bref = true;
                this.Refresh();
            }
            base.OnMouseMove(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && (state == 1))
            {
                state = 0;
                this.Cursor = Cursors.Hand;
            }
            base.OnMouseUp(e);
        }
 
        protected override void OnResize(EventArgs e)
        {
            bref = true;
            Invalidate();            
            base.OnResize(e);
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            //Comprobar que se halla establecido el bitmap de imagen. Si no es así, volver.
            if (img == null)
            {
                base.OnPaint(pe);
                return;
            }
            if (bref)
            {
                Bitmap bmp = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                BitmapData data1 = bmp.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, 
                    System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                int ext = data1.Stride - data1.Width * 3;
                unsafe
                {
                    FastDrawImage((byte *)data1.Scan0, ptr2bk, Width, Height, wDataImg,hDataImg,
                        ext, extra, pX, pY, zoom, 128);
                }
                bmp.UnlockBits(data1);
                base.Image = bmp;
                bref = false;
            }
            base.OnPaint(pe);
        }
        public void TestVelocidad()
        {
            // Tests de velocidad

            Bitmap bmp = new Bitmap(Width,Height,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData data1 = bmp.LockBits(new Rectangle(0, 0, Width, Height),
                ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            int extra1 = data1.Stride - data1.Width * 3;

            long t1, t2;
            float t;
            t1 = Environment.TickCount;
            for (int i = 0; i < 1000; i++)
            {
                unsafe
                {
                    byte* ptr = (byte*)(data1.Scan0);
                    FastDrawImage(ptr, ptr2bk, Width, Height,wDataImg ,hDataImg,
                         extra1, extra, pX, pY, 1 / 32F, 128);
                }
            }
            bmp.UnlockBits(data1);
            this.Image = bmp;
            t2 = Environment.TickCount;
            t = (t2 - t1) / 1000.0F;
            MessageBox.Show("1000 búcles + 1 volcado (" + (wDataImg.ToString()) + "," + (hDataImg.ToString()) + "): " + t.ToString() + " segundos.");
        }

     }
}