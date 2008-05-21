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
            public String timestamp;
            public String camera_make;
            public String camera_model;
            public String artist;
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
            public String filter_pattern;
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
            public float exposure;
            public float exposure_mode;
        }

        [DllImport(@"dcraw.dll")]
        static extern void DCRAW_DefaultParameters(ref DLL_PARAMETERS p);

        [DllImport(@"dcraw.dll")]
        static extern int DCRAW_Init(string rawfile, int* Width, int* Height);

        [DllImport(@"dcraw.dll")]
        static extern void DCRAW_GetInfo(ref IMAGE_INFO info);

        [DllImport(@"dcraw.dll")]
        static extern ushort* DCRAW_Process(ref DLL_PARAMETERS p);

        [DllImport(@"dcraw.dll")]
        static extern void DCRAW_End();

        [DllImport(@"image.dll")]
        static extern void Convert48RGBto24BGR(ushort* buffer16, byte* buffer8, int width, int height, int extra);

        public IMAGE_INFO info; // Esto hay que convertirlo en propiedad
        public DLL_PARAMETERS parameters;
        public Bitmap img;      // Esto hay que convertirlo en propiedad
        int status;
        int w, h;        
        string _FileName;
        bool init = false;

        public Dcraw()
        {
        }

        ~Dcraw()
        {
            DCRAW_End();
        }

        public int Init()
        {
            int _w, _h;
            info = new IMAGE_INFO();
            parameters = new DLL_PARAMETERS();
            status = DCRAW_Init(_FileName, &_w, &_h);
            w = _w;
            h = _h;            
            DCRAW_DefaultParameters(ref parameters);
            init = true;            
            return status;
        }

        public void SetRAWFile(string FileName)
        {
            _FileName = FileName;
        }

        public void End()
        {
            DCRAW_End();
            init = false;
        }

        public void GetInfo()
        {
            if (!init) Init();            
            //DCRAW_GetInfo(ref info);

        }

        public void Process()
        {
            if (!init) Init();
            img = new Bitmap(w, h);
            int extra;
            //BitmapData data = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite, PixelFormat.Format48bppRgb);
            BitmapData data = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                ushort* image = DCRAW_Process(ref parameters);

                byte* ptr = (byte*)(data.Scan0);
                extra = data.Stride - data.Width * 3;
                Convert48RGBto24BGR(image, ptr, data.Width, data.Height, extra);
            }
            img.UnlockBits(data);
        }
    }
}
