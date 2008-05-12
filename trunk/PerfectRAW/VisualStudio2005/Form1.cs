using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading;

namespace DCRAwGUI
{			    
    /// <summary>
	/// Descripción breve de Form1.
	/// </summary>
	unsafe public class Form1 : System.Windows.Forms.Form
	{        
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.PictureBox pictureBox1;
        private Label label1;
        private Label camera;
        private TextBox textBox1;
        private Label label2;
        private OpenFileDialog openFileDialog1;
        private Button button2;
        private Button button3;
        private Button button4;        

		/// <summary>
		/// Variable del diseñador requerida.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Necesario para admitir el Diseñador de Windows Forms
			//
			InitializeComponent();

			//
			// TODO: agregar código de constructor después de llamar a InitializeComponent
			//
		}

		/// <summary>
		/// Limpiar los recursos que se estén utilizando.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Código generado por el Diseñador de Windows Forms
		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.camera = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(12, 441);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 24);
            this.button1.TabIndex = 0;
            this.button1.Text = "REVELAR";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(570, 401);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 447);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cámara:";
            // 
            // camera
            // 
            this.camera.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.camera.AutoSize = true;
            this.camera.Location = new System.Drawing.Point(155, 447);
            this.camera.Name = "camera";
            this.camera.Size = new System.Drawing.Size(0, 13);
            this.camera.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(90, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(392, 20);
            this.textBox1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Archivo RAW:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(489, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 20);
            this.button2.TabIndex = 6;
            this.button2.Text = "Buscar...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(508, 442);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(427, 442);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(595, 477);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.camera);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "perfectRAW";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// Punto de entrada principal de la aplicación.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
            DCRAW_Thread dcraw= new DCRAW_Thread(textBox1.Text);
            Thread thread=new Thread(new ThreadStart(dcraw.Process));
            thread.Priority = ThreadPriority.Lowest;
            thread.Start();            
            do{
                Console.Write(".");
                Thread.Sleep(50);
            }while (thread.IsAlive);
            camera.Text = dcraw.info.camera_make + " " + dcraw.info.camera_model;
            pictureBox1.Image = (Image)dcraw.img;
		}

        private string GetFileName(){
            return textBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = openFileDialog1.FileName;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = @"c:\dcraw\raws\1.cr2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.SizeMode == PictureBoxSizeMode.Normal)
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            else
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Save("aaa.jpg", ImageFormat.Tiff);
        }
	}

    unsafe public class DCRAW_Thread
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

        [DllImport(@"dcraw.dll")]
        static extern int DCRAW_Init(string rawfile, int* Width, int* Height);

        [DllImport(@"dcraw.dll")]
        static extern void DCRAW_End();

        [DllImport(@"dcraw.dll")]
        static extern void DCRAW_GetInfo(ref IMAGE_INFO info);

        [DllImport(@"dcraw.dll")]
        static extern ushort* DCRAW_Process(int stage);

        private string _FileName;
        public IMAGE_INFO info;
        public Bitmap img;

        public DCRAW_Thread(string FileName)
        {
            this._FileName = FileName;
        }

        public void Process()
        {
            int status;
            int w, h;

            status = DCRAW_Init(this._FileName, &w, &h);
            if (status == 0)
            {
                DCRAW_GetInfo(ref info);
                img = new Bitmap(w, h);
                BitmapData data = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite, PixelFormat.Format48bppRgb);
                unsafe
                {
                    ushort *image = DCRAW_Process(1);

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
            DCRAW_End();
        }
    }
}
