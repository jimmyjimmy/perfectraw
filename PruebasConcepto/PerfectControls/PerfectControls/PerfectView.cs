﻿using System;
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
    public enum PerfectViewMode
    {
        OneView,
        VerticalSplit,
        HorizontalSplit
    }

    public unsafe partial class PerfectView : PictureBox
    {
        [DllImport(@"PerfectImageDLL.dll")]
        private static extern void FastDrawImage16MarkBP(byte *outputImg, ushort *inputImg, 
            int outImgWidth, int outImgHeight, int inImgWidth, int inImgHeight, 
            int outImgRowPadding, int inImgRowPadding, int x, int y, float z, 
            byte fillColor);
        [DllImport(@"PerfectImageDLL.dll")]
        private static extern void FastDrawImage16(byte* outputImg, ushort* inputImg,
            int outImgWidth, int outImgHeight, int inImgWidth, int inImgHeight,
            int outImgRowPadding, int inImgRowPadding, int x, int y, float z,
            byte fillColor);

        [DllImport(@"PerfectImageDLL.dll")]
        private static extern void FastDrawImage16VSplit(byte *outputImg, ushort *inputLeftImg, ushort *inputRightImg,
            int outImgWidth, int outImgHeight, int inImgWidth, int inImgHeight,
            int outImgRowPadding, int inImgRowPadding, int x, int y, float z,
            byte fillColor);
        [DllImport(@"PerfectImageDLL.dll")]
        private static extern void FastDrawImage16VSplitMarkBP(byte* outputImg, ushort* inputLeftImg, ushort* inputRightImg,
            int outImgWidth, int outImgHeight, int inImgWidth, int inImgHeight,
            int outImgRowPadding, int inImgRowPadding, int x, int y, float z,
            byte fillColor);
        [DllImport(@"PerfectImageDLL.dll")]
        private static extern void FastDrawImage16HSplit(byte* outputImg, ushort* inputTopImg, ushort* inputBottomImg,
            int outImgWidth, int outImgHeight, int inImgWidth, int inImgHeight,
            int outImgRowPadding, int inImgRowPadding, int x, int y, float z,
            byte fillColor);
        [DllImport(@"PerfectImageDLL.dll")]
        private static extern void FastDrawImage16HSplitMarkBP(byte* outputImg, ushort* inputTopImg, ushort* inputBottomImg,
            int outImgWidth, int outImgHeight, int inImgWidth, int inImgHeight,
            int outImgRowPadding, int inImgRowPadding, int x, int y, float z,
            byte fillColor);


        //Bitmap de la imagen original y de la previa (Old de un anterior revelado)
        Bitmap img,imgOld;
        float zoom;
        int stateDragging, pX, pY;
        int dragFromX, dragFromY;
        int wDispImg, hDispImg;
        bool bref;
        int showBurnedPixelsFlash;
        Timer timer;
        PerfectViewMode viewMode;

        public PerfectView()
        {
            InitializeComponent();
            zoom = 1;
            stateDragging=0;
            pX=pY = 0;
        }
        ~PerfectView()
        {
        }

        //Convertir Image en privada. La propiedad Image quedará sustituída
        //por Bitmap, ya que PerfecView está diseñada sólo para manejar imágenes
        //bitmap y no metafiles.
        private new Image Image;

        //Bitmap que almacena la imagen completa (antes de reescalar o recortar)
        //Puede ser estipulado también desde fichero utilizando 
        [Category("Appearance")]
        public Bitmap Bitmap
        {
            set
            {
                LoadBitmap(value);
            }
            get
            {
                return img;
            }
        }
        //Bitmap procedente de un revelado anterior y que se podrá mostrar en imagen partida.
        //Si no se establece sólo se muestra siempre la imagen Bitmap.
        [Category("Appearance")]
        public Bitmap OldBitmap
        {
            set
            {
                LoadOldBitmap(value);
            }
            get
            {
                return imgOld;
            }
        }
        [Category("Behavior")]
        public bool ShowBurnedPixels
        {
            set
            {
                if (value)
                {
                    showBurnedPixelsFlash = 1;
                    timer = new Timer();
                    timer.Interval = 250;
                    timer.Tick += new System.EventHandler(TimerTick);
                    timer.Enabled = true;
                    bref = true;
                    this.Refresh();
                }
                else
                {
                    showBurnedPixelsFlash = 0;
                    if (timer == null) return;
                    timer.Enabled = false;
                    timer.Dispose();
                    timer = null;
                    bref = true;
                    this.Refresh();
                    
                }
            }
            get
            {
                return showBurnedPixelsFlash!=0;
            }
        }
        void TimerTick(object sender, EventArgs e)
        {
            // Cambiamos este flag para que parpadee (entre -1 y 1)
            showBurnedPixelsFlash = -showBurnedPixelsFlash;
            bref = true;
            this.Refresh();
        }

        [Category("Behavior")]
        public PerfectViewMode ViewMode
        {
            set
            {
                viewMode = value;
                bref = true;
                this.Refresh();
            }
            get
            {
                return viewMode;
            }
        }
        private void LoadBitmap(Bitmap bitmap)
        {
            //During control initialization we may receive null bitmaps (if they are not set
            //in control properties window. Do nothing in that case.
            if (bitmap == null) return;
            img = bitmap;
            wDispImg = img.Width;
            hDispImg = img.Height;
        }
        private void LoadOldBitmap(Bitmap bitmap)
        {
            //During control initialization we may receive null bitmaps (if they are not set
            //in control properties window. Do nothing in that case.
            if (bitmap == null) return;
            imgOld = bitmap;
        }

        [Category("Appearance")]
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
                if (pX > wDispImg - (int)(Width / zoom)) pX = wDispImg - (int)(Width / zoom);
                if (pY > hDispImg - (int)(Height / zoom)) pY = hDispImg - (int)(Height / zoom);
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

        //Propiedad para establecer u obtener el path de la imagen.
        //Si se establece se elimina el Bitmap que pudiera existir previamente.
        //Posteriormente habrá que hacer un Load para cargar la imagen.
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
            string path = ImageLocation;
            if ( (path != "") && (path != null))
            {
                LoadBitmap((Bitmap)Image.FromFile(path));
            }
            base.Load();
        }
        public new void Load(string path)
        {
            ImageLocation=path;
            this.Load();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                stateDragging = 1;
                dragFromX = pX + (int)(e.X / zoom);
                dragFromY = pY + (int)(e.Y / zoom);
                this.Cursor = Cursors.NoMove2D;
            }
            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (stateDragging == 1)
            {
                pX = dragFromX - (int)(e.X / zoom);
                pY = dragFromY - (int)(e.Y / zoom);
                if (pX < 0) pX = 0;
                if (pY < 0) pY = 0;
                if (pX > wDispImg - (int)(Width / zoom)) pX = wDispImg - (int)(Width / zoom);
                if (pY > hDispImg - (int)(Height / zoom)) pY = hDispImg - (int)(Height / zoom);
                bref = true;
                this.Refresh();
            }
            base.OnMouseMove(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && (stateDragging == 1))
            {
                stateDragging = 0;
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
            //Comprobar que se halla establecido el bitmap de imagen y si la imagen ha cambiado.
            //Si no es así, volver.
            if ((img == null) || !bref)
            {
                base.OnPaint(pe);
                return;
            }
            Bitmap bmp = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData dataBmp = bmp.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, 
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //Calc number of extra dummy byte in a row pointer of the image.
            byte* ptrBmp = (byte*)(dataBmp.Scan0);
            int extBmp = dataBmp.Stride - dataBmp.Width * 3;

            BitmapData dataImg = img.LockBits(new Rectangle(0, 0, img.Width, img.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format48bppRgb);
            //Calc number of extra dummy byte in a row pointer of the image.
            int extra = dataImg.Stride - dataImg.Width * 6;
            ushort *ptr2bk = (ushort*)(dataImg.Scan0);

            BitmapData dataImgOld;
            ushort* ptr2bkOld;

            switch (viewMode)
            {
                case PerfectViewMode.OneView:
                    unsafe
                    {
                        if(showBurnedPixelsFlash==1)
                            FastDrawImage16MarkBP(ptrBmp, ptr2bk, Width, Height, wDispImg, hDispImg,
                                    extBmp, extra, pX, pY, zoom, 128);
                        else
                            FastDrawImage16((byte*)dataBmp.Scan0, ptr2bk, Width, Height, wDispImg, hDispImg,
                                    extBmp, extra, pX, pY, zoom, 128);
                    }
                    break;
                case PerfectViewMode.VerticalSplit:
                    dataImgOld = imgOld.LockBits(new Rectangle(0, 0, imgOld.Width, imgOld.Height),
                       ImageLockMode.ReadOnly,
                       System.Drawing.Imaging.PixelFormat.Format48bppRgb);
                    ptr2bkOld = (ushort*)(dataImgOld.Scan0);
                    unsafe
                    {
                        if (showBurnedPixelsFlash == 1)
                            FastDrawImage16VSplitMarkBP(ptrBmp, ptr2bk, ptr2bkOld, Width, Height, wDispImg, hDispImg,
                                extBmp, extra, pX, pY, zoom, 128);
                        else
                            FastDrawImage16VSplit(ptrBmp, ptr2bk, ptr2bkOld, Width, Height, wDispImg, hDispImg,
                                extBmp, extra, pX, pY, zoom, 128);
                    }
                    imgOld.UnlockBits(dataImgOld);
                    break;
                case PerfectViewMode.HorizontalSplit:
                    dataImgOld = imgOld.LockBits(new Rectangle(0, 0, imgOld.Width, imgOld.Height),
                       ImageLockMode.ReadOnly,
                       System.Drawing.Imaging.PixelFormat.Format48bppRgb);
                    ptr2bkOld = (ushort*)(dataImgOld.Scan0);
                    unsafe
                    {
                        if( showBurnedPixelsFlash==1)
                            FastDrawImage16HSplitMarkBP(ptrBmp, ptr2bk, ptr2bkOld, Width, Height, wDispImg, hDispImg,
                                extBmp, extra, pX, pY, zoom, 128);
                        else
                            FastDrawImage16HSplit(ptrBmp, ptr2bk, ptr2bkOld, Width, Height, wDispImg, hDispImg,
                                extBmp, extra, pX, pY, zoom, 128);
                    }
                    imgOld.UnlockBits(dataImgOld);
                    break;
            }
            img.UnlockBits(dataImg);
            bmp.UnlockBits(dataBmp);
            
            //Dibujar las máscaras de selección de objetos o Histograma.
            Graphics gc = Graphics.FromImage(bmp);
            Matrix transMat = new Matrix();
            transMat.Translate(-pX * zoom, -pY* zoom);
            transMat.Scale(zoom, zoom);
            gc.Transform = transMat;
            DrawSelection(gc);
            gc.Dispose();
            transMat.Dispose();
             

            //Change image in PictureBox with generated image.
            //PictureBox is supposed to make the dispose of the image
            //when changed.
            base.Image = bmp;
            bref = false;
            base.OnPaint(pe);
        }
        public void SpeedTest()
        {
            // Tests de velocidad
            BitmapData dataImg = img.LockBits(new Rectangle(0, 0, img.Width, img.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format48bppRgb);
            //Calc number of extra dummy byte in a row pointer of the image.
            int extra = dataImg.Stride - dataImg.Width * 6;
            ushort* ptr2bk = (ushort*)(dataImg.Scan0);

            Bitmap bmp = new Bitmap(Width,Height,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData data1 = bmp.LockBits(new Rectangle(0, 0, Width, Height),
                ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //Calc number of extra dummy byte in a row pointer of the image.
            int ext = data1.Stride - data1.Width * 3;

            long t1, t2;
            float t;
            t1 = Environment.TickCount;
            for (int i = 0; i < 1000; i++)
            {
                unsafe
                {
                    byte* ptr = (byte*)(data1.Scan0);
                    FastDrawImage16MarkBP(ptr, ptr2bk, Width, Height,wDispImg ,hDispImg,
                          ext, extra, pX, pY, 1 / 32F, 128);
                }
            }
            img.UnlockBits(dataImg);
            bmp.UnlockBits(data1);
            this.Image = bmp;
            t2 = Environment.TickCount;
            t = (t2 - t1) / 1000.0F;
            MessageBox.Show("1000 búcles + 1 volcado (" + (wDispImg.ToString()) + "," + (hDispImg.ToString()) + "): " + t.ToString() + " segundos.");
        }
        protected void DrawSelection(Graphics gc)
        {
            /*
            SolidBrush br = new SolidBrush(Color.FromArgb(70, Color.Aquamarine));

            gc.FillEllipse(br, wDispImg/4, hDispImg/4, wDispImg/2, hDispImg/2);
            br.Dispose();
             * */
            return;
        }

     }
}