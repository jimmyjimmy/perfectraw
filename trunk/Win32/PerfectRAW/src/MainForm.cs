using System;
using System.IO;
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
    /// <summary>
    /// Descripción breve de MainForm.
    /// </summary>
    unsafe public class MainForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Label camera;
        private TextBox textBox1;
        private Label label2;
        private OpenFileDialog openFileDialog1;
        private Button button2;
        private Label label3;
        private NumericUpDown numericUpDown1;
        private Label label5;
        Dcraw dcraw = new Dcraw();
        private Button button3;
        private NumericUpDown numericUpDown2;
        private Label label4;
        private Label label6;
        private Label label7;
        private NumericUpDown numericUpDown3;
        private GroupBox groupBox1;

        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public MainForm()
        {
            //
            // Necesario para admitir el Diseñador de Windows Forms
            //
            InitializeComponent();

            //
            // TODO: agregar código de constructor después de llamar a InitializeComponent
            //            
            MessageBox.Show("perfectRAW\nMódulo revelador\nVersión de prueba nº 2\n22 de mayo de 2008 a las 14:45");
        }

        ~MainForm()
        {
            dcraw.End();
        }
        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
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
            this.camera = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 685);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 50);
            this.button1.TabIndex = 0;
            this.button1.Text = "REVELAR";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1005, 642);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // camera
            // 
            this.camera.AutoSize = true;
            this.camera.Location = new System.Drawing.Point(452, 720);
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
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(721, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Estado...";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericUpDown1.Location = new System.Drawing.Point(76, 18);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(47, 20);
            this.numericUpDown1.TabIndex = 8;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Exposición:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button3.Location = new System.Drawing.Point(907, 7);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(97, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "GRABAR JPEG";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(366, 17);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(28, 20);
            this.numericUpDown2.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(133, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(231, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Modo de preservación de luces altas (0 = OFF):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(600, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Estado de perfectRAW:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(516, 705);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(232, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Modo de recuperación de luces altas (0 = OFF):";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(750, 702);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(29, 20);
            this.numericUpDown3.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Location = new System.Drawing.Point(103, 686);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(407, 47);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control de exposición";
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.camera);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "perfectRAW";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        [DllImport(@"dcraw.dll")]
        static extern unsafe int Coffin(int argc, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 0)] string[] argv);

        [StructLayout(LayoutKind.Sequential,CharSet=CharSet.Ansi)]
        public struct TEST
        {
            public int a;
            public String b;
        }

        [DllImport(@"dcraw.dll",CharSet=CharSet.Ansi)]
        static extern void test(ref TEST t);
        TEST t;

        private void button1_Click(object sender, System.EventArgs e)
        {            
            /*test(ref t);
            MessageBox.Show(t.a.ToString()+" "+t.b);*/

            if (!File.Exists(textBox1.Text))
            {
                MessageBox.Show("No se encuentra el archivo '" + textBox1.Text + "'. Inténtelo de nuevo\n");
                textBox1.Focus();
                return;
            }

            //String []argv={"dcraw.exe","-v","-q","0","-T","-4",@"c:\dcraw\raws\process.dng"};
            //Coffin(argv.Length, argv);
            button1.Enabled = false;

            // Process RAW
            label3.Text = "Obteniendo información del RAW...";
            dcraw.GetInfo();
            camera.Text = dcraw.info.camera_make + " " + dcraw.info.camera_model;

            label3.Text = "Revelando...";
            //dcraw.parameters.user_qual = 1;
            dcraw.parameters.user_gamma = 1; // Sin aplicar gamma
            dcraw.parameters.output_color = 1; // Convertimos a sRGB
            dcraw.parameters.exposure = (float)Math.Pow(2.0, (double)numericUpDown1.Value);
            dcraw.parameters.exposure_mode =(int)numericUpDown2.Value;
            dcraw.parameters.highlight = (int)numericUpDown3.Value;
            Thread thread1 = new Thread(new ThreadStart(dcraw.Process));
            thread1.Priority = ThreadPriority.Normal;
            thread1.Start();
            do
            {
                Application.DoEvents();
                Thread.Sleep(100);
            } while (thread1.IsAlive);
            if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
            pictureBox1.Image = (Image)dcraw.img;
            //Clipboard.Clear();
            //Application.DoEvents();
            //Clipboard.SetImage(pictureBox1.Image);
            
            label3.Text = "Terminado...";
            button1.Enabled = true;
        }

        private string GetFileName()
        {
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = @"C:\test\a_1.dng";
            dcraw.SetRAWFile(textBox1.Text);
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dcraw.End();
            dcraw.SetRAWFile(textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveJPGWithCompressionSetting(pictureBox1.Image, textBox1.Text + ".jpg", 96);
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        private void SaveJPGWithCompressionSetting(Image image, string szFileName, long lCompression)
        {
            EncoderParameters eps = new EncoderParameters(1);
            eps.Param[0] = new EncoderParameter(Encoder.Quality, lCompression);
            ImageCodecInfo ici = GetEncoderInfo("image/jpeg");
            image.Save(szFileName, ici, eps);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
