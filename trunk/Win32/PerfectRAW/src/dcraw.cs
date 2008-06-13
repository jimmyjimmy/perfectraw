using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;
using System.Xml;

namespace perfectRAW
{
    unsafe public class Dcraw
    {
        // Estructura para informar fuera de la DLL de la imagen
        // Falta DNG version y camera white balance coef.
        [StructLayout(LayoutKind.Sequential)]
        public struct IMAGE_INFO
        {
            public StringBuilder timestamp;
            public StringBuilder camera_make;
            public StringBuilder camera_model;
            public StringBuilder artist;
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
            public StringBuilder filter_pattern;
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
            public float level_edge;
            public int level_cell;
            public int user_qual;
            public int four_color_rgb;
            public int med_passes;
            public int highlight;
            public int output_color;
            public int use_fuji_rotate;
            public float user_gamma;
            public float exposure;
            public float preserve;
        }

        [DllImport(@"dcraw.dll")]
        static extern void DCRAW_DefaultParameters(ref DLL_PARAMETERS P);

        [DllImport(@"dcraw.dll")]
        static extern int DCRAW_Init(string rawfile, ref int width, ref int height, ref int sat_level, ref int black_level, float[] cam_WB, float[,] cam_RGB);

        [DllImport(@"dcraw.dll")]
        static extern void DCRAW_GetInfo(ref IMAGE_INFO info);

        [DllImport(@"dcraw.dll")]
        static extern ushort* DCRAW_Process(ref DLL_PARAMETERS p, ref int cancel, ref int estado);

        [DllImport(@"dcraw.dll")]
        static extern void DCRAW_SaveTIFF16(string filename, int colorspace, float gamma);
        
        [DllImport(@"dcraw.dll")]
        static extern void DCRAW_End();

        [DllImport(@"colormng.dll",CharSet=CharSet.Ansi)]
        static extern void Convert48RGBto24BGR(ushort* buffer16, byte* buffer8, int width, int height, int extra, float[,] cam_RGB, int colorspace, float gamma, string inProfileFile, string outProfileFile);

        public IMAGE_INFO info; // Esto hay que convertirlo en propiedad
        public DLL_PARAMETERS parameters;
        public Bitmap img;      // Esto hay que convertirlo en propiedad
        public int cancel;
        public int estado;
        int status;
        public int w, h, sat_level, black_level;        
        string _FileName;
        bool init = false;
        public bool first_time = true;
        public ushort* image;
        public float[] cam_WB = { 1F, 1F, 1F, 1F };
        float[,] cam_RGB = { { 0.0F, 0.0F, 0.0F, 0.0F }, { 0.0F, 0.0F, 0.0F, 0.0F }, { 0.0F, 0.0F, 0.0F, 0.0F } };

        public Dcraw()
        {
        }

        ~Dcraw()
        {
            DCRAW_End();
        }

        public int Init()
        {
            int _w = 0, _h = 0, _sat_level = 0, _black_level=0;
            float[] _cam_WB = {0.0F,0.0F,0.0F,0.0F};
            float[,] _cam_RGB = { { 0.0F, 0.0F, 0.0F, 0.0F }, { 0.0F, 0.0F, 0.0F, 0.0F }, { 0.0F, 0.0F, 0.0F, 0.0F } };

            info = new IMAGE_INFO();
            parameters = new DLL_PARAMETERS();
            status = DCRAW_Init(_FileName, ref _w, ref _h, ref _sat_level, ref _black_level, _cam_WB, _cam_RGB);
            w = _w;
            h = _h;
            sat_level = _sat_level;
            black_level = _black_level;
            for (int i = 0; i < 4; i++)
            {
                cam_WB[i] = _cam_WB[i];
                for (int j = 0; j < 3; j++) cam_RGB[j,i] = _cam_RGB[j,i];                   
            }
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

        public void SaveTIFF(string filename, int colorspace, float gamma)
        {
            DCRAW_SaveTIFF16(filename, colorspace, gamma);
        }

        public void Process()
        {
            XmlDocument config = new XmlDocument();
            config.Load(Application.StartupPath + "\\config.xml");
            
            string InProfile;
            string OutProfile;
            InProfile=config.GetElementsByTagName("InputProfile").Item(0).InnerText.ToString();
            OutProfile = config.GetElementsByTagName("OutputProfile").Item(0).InnerText.ToString();
            
            estado = -1;
            cancel = 0;
            if (!init) Init();
            img = new Bitmap(w, h,PixelFormat.Format24bppRgb);
            int extra;            
            BitmapData data = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                image = DCRAW_Process(ref parameters,ref cancel, ref estado);
                if (estado == 6)
                {
                    byte* ptr = (byte*)(data.Scan0);
                    extra = data.Stride - data.Width * 3;
                    Convert48RGBto24BGR(image, ptr, data.Width, data.Height, extra, cam_RGB, 1, 0F, InProfile, OutProfile);
                }
            }
            img.UnlockBits(data);
            first_time = false;
        }
    }
}