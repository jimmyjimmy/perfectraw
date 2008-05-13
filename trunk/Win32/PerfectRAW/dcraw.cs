using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading;

namespace perfectRAW
{
    unsafe public class Dcraw
    {    
        // Estructura para informar fuera de la DLL de la imagen
        // Falta DNG version y camera white balance coef.
        [StructLayout(LayoutKind.Sequential)]
        public struct IMAGE_INFO
        {
            public string timestamp;
            public string camera_make;
            public string camera_model;
            public string artist;
            public int iso_speed;
            public float shutter;
            public float aperture;
            public float focal_length;
            public float aspect_ratio;
            public int raw_width;
            public int raw_height;
            public int in_width;
            public int in_height;
            public int out_width;
            public int out_height;
            public int raw_colors;
            public string filter_pattern;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DLL_PARAMETERS
        {
            public float threshold;
            public fixed double aber[4];
            public int use_auto_wb;
            public int use_camera_wb;
            public fixed uint greybox[4];
            public int user_black;
            public int user_sat;
            public int test_pattern;
            public int level_greens;
            public int user_qual;
            public int four_color_rgb;
            public int med_passes;
            public int highlight;
            public int output_color;
            public int use_fuji_rotate;
            public float user_gamma;
        }

        [DllImport(@"dcraw.dll")]
        static extern void DCRAW_DefaultParameters(ref DLL_PARAMETERS p);

        [DllImport(@"dcraw.dll")]
        static extern int DCRAW_Init(string rawfile, int* Width, int* Height);

        [DllImport(@"dcraw.dll")]
        static extern void DCRAW_End();

        [DllImport(@"dcraw.dll")]
        static extern void DCRAW_GetInfo(ref IMAGE_INFO info);

        [DllImport(@"dcraw.dll")]
        static extern ushort* DCRAW_Process(ref DLL_PARAMETERS p);

        private string _FileName;
        public IMAGE_INFO info; // Esto hay que convertirlo en propiedad
        public Bitmap img;      // Esto hay que convertirlo en propiedad
        int status;
        int w, h;
        public DLL_PARAMETERS parameters;

        public Dcraw(string FileName)
        {
            int _w, _h;

            this._FileName = FileName;
            status = DCRAW_Init(this._FileName, &_w, &_h);
            w = _w;
            h = _h;
            DCRAW_DefaultParameters(ref parameters);
        }

        ~Dcraw()
        {
            DCRAW_End();
        }

        public void GetInfo()
        {
            DCRAW_GetInfo(ref info);
        }
        
        public void Process()
        {
            if (status == 0)
            {                
                img = new Bitmap(w, h);
                BitmapData data = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite, PixelFormat.Format48bppRgb);
                unsafe
                {
                    ushort* image = DCRAW_Process(ref parameters);

                    byte* ptr = (byte*)(data.Scan0);
                    for (int i = 0; i < data.Height; i++)
                    {
                        for (int j = 0; j < data.Width; j++)
                        {
                            ptr[0] = (byte)(image[2] >> 4 & 0xff);
                            ptr[1] = (byte)(image[2] >> 12);
                            ptr[2] = (byte)(image[1] >> 4 & 0xff);
                            ptr[3] = (byte)(image[1] >> 12);
                            ptr[4] = (byte)(image[0] >> 4 & 0xff);
                            ptr[5] = (byte)(image[0] >> 12);
                            image += 3;
                            ptr += 6;
                        }
                        ptr += data.Stride - data.Width * 6;
                    }
                }
                img.UnlockBits(data);
            }            
        }    
    }
}
