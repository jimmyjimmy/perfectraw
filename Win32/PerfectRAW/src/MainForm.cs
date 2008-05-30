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
using System.Diagnostics;

namespace perfectRAW
{
    /// <summary>
    /// Descripción breve de MainForm.
    /// </summary>
    unsafe public class MainForm : System.Windows.Forms.Form
    {
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
        private Label label7;
        private NumericUpDown numericUpDown3;
        private GroupBox groupBox1;
        private bool RevState=false;
        private Label label1;
        private Label label8;
        private NumericUpDown numericUpDown4;
        private NumericUpDown numericUpDown5;
        private Label label9;
        private Label label10;
        private GroupBox groupBox2;
        private Label FreeMem;
        private System.Windows.Forms.Timer Clock;
        private Button button1;
        private PictureBox pictureBox1;
        private bool InitCounter = false;
        private bool Init = false;

        // Código no portable a otras plataformas
        [DllImport("kernel32")]
        static extern void GlobalMemoryStatus(ref MEMORYSTATUS buf);

        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORYSTATUS
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public uint dwTotalPhys;
            public uint dwAvailPhys;
            public uint dwTotalPageFile;
            public uint dwAvailPageFile;
            public uint dwTotalVirtual;
            public uint dwAvailVirtual;
        }
        // Código no portable a otras plataformas

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
            if (Environment.GetCommandLineArgs().Length==1) MessageBox.Show("perfectRAW\nMódulo revelador\nVersión de prueba nº 9\n26 de mayo de 2008 a las 23:00");
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
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FreeMem = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.textBox1.Location = new System.Drawing.Point(98, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(228, 18);
            this.textBox1.TabIndex = 4;
            this.textBox1.WordWrap = false;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 9);
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
            this.button2.Location = new System.Drawing.Point(335, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 19);
            this.button2.TabIndex = 6;
            this.button2.Text = "Buscar...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Lime;
            this.label3.Location = new System.Drawing.Point(3, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "LISTO";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.numericUpDown1.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericUpDown1.Location = new System.Drawing.Point(83, 11);
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
            this.numericUpDown1.Size = new System.Drawing.Size(72, 18);
            this.numericUpDown1.TabIndex = 8;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 9);
            this.label5.TabIndex = 10;
            this.label5.Text = "Exposición:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(836, 659);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 20);
            this.button3.TabIndex = 12;
            this.button3.Text = "GRABAR JPEG";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DecimalPlaces = 2;
            this.numericUpDown2.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.numericUpDown2.Location = new System.Drawing.Point(367, 11);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(48, 18);
            this.numericUpDown2.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(158, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 9);
            this.label4.TabIndex = 15;
            this.label4.Text = "Diafragmas a preservar (0 = OFF):";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(110, 660);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(284, 9);
            this.label7.TabIndex = 18;
            this.label7.Text = "Modo de recuperación de luces altas (0 = OFF):";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown3.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.numericUpDown3.Location = new System.Drawing.Point(397, 657);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(61, 18);
            this.numericUpDown3.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Location = new System.Drawing.Point(100, 622);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(425, 34);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control de exposición";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(534, 628);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 9);
            this.label1.TabIndex = 20;
            this.label1.Text = "Nivel de saturación:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(534, 640);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 9);
            this.label8.TabIndex = 21;
            this.label8.Text = "(dcraw: )";
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown4.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.numericUpDown4.Location = new System.Drawing.Point(664, 629);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(59, 18);
            this.numericUpDown4.TabIndex = 22;
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown5.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.numericUpDown5.Location = new System.Drawing.Point(664, 654);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(59, 18);
            this.numericUpDown5.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(534, 665);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 9);
            this.label9.TabIndex = 24;
            this.label9.Text = "(dcraw: )";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(534, 653);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 9);
            this.label10.TabIndex = 23;
            this.label10.Text = "Nivel de negro:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.FreeMem);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(809, -4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(142, 34);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            // 
            // FreeMem
            // 
            this.FreeMem.AutoSize = true;
            this.FreeMem.Location = new System.Drawing.Point(1, 23);
            this.FreeMem.Name = "FreeMem";
            this.FreeMem.Size = new System.Drawing.Size(78, 9);
            this.FreeMem.TabIndex = 27;
            this.FreeMem.Text = "FREE RAM: Mb";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(11, 631);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 19);
            this.button1.TabIndex = 0;
            this.button1.Text = "REVELAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1192, 529);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 10);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(952, 681);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.numericUpDown5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.numericUpDown4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Victor\'s Pixel Font", 7F);
            this.MinimumSize = new System.Drawing.Size(960, 545);
            this.Name = "MainForm";
            this.Text = "perfectRAW";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Resize += new System.EventHandler(this.Form1_OnResize);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void Form1_OnResize(object sender, EventArgs e)
        {
            ResizeViews();
        }

        void ResizeViews()
        {
            pictureBox1.Width = this.Width;
            pictureBox1.Left = 0;
            pictureBox1.Height = this.Height - 250;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Revelar();
        }

        private void Revelar()
        {
            if (!RevState)
            {
                if (!File.Exists(textBox1.Text))
                {
                    MessageBox.Show("No se encuentra el archivo '" + textBox1.Text + "'. Inténtelo de nuevo\n");
                    textBox1.Focus();
                    return;
                }
                
                //String []argv={"dcraw.exe","-v","-q","0","-T","-4",@"c:\dcraw\raws\process.dng"};
                //Coffin(argv.Length, argv);
                if (!dcraw.first_time)
                {
                    RevState = true;
                    button1.Text = "CANCELAR";
                    Application.DoEvents();
                }else{
                    button1.Enabled = false;
                }

                // Process RAW                
                dcraw.GetInfo();
                label8.Text = "(dcraw: " + dcraw.sat_level.ToString() + ")";
                label9.Text = "(dcraw: " + dcraw.black_level.ToString() + ")";
                if (dcraw.first_time) numericUpDown4.Value = dcraw.sat_level;
                if (dcraw.first_time) numericUpDown5.Value = dcraw.black_level;
                //camera.Text = dcraw.info.camera_make + " " + dcraw.info.camera_model;
                dcraw.cancel = 0;
                dcraw.estado = -1;
                label3.Text = "REVELANDO";
                this.Cursor = Cursors.WaitCursor;
                label3.BackColor = Color.Red;
                Application.DoEvents();
                //dcraw.parameters.user_qual = 1;
                dcraw.parameters.user_gamma = 1; // Sin aplicar gamma
                dcraw.parameters.output_color = 0; // Pedimos color RAW a dcraw.dll
                dcraw.parameters.exposure = (float)Math.Pow(2.0, (double)numericUpDown1.Value);
                dcraw.parameters.preserve = (float)numericUpDown2.Value;
                dcraw.parameters.highlight = (int)numericUpDown3.Value;
                if (((int)numericUpDown4.Value) != dcraw.parameters.user_sat) dcraw.parameters.user_sat = (int)numericUpDown4.Value;
                if (((int)numericUpDown5.Value) != dcraw.parameters.user_black) dcraw.parameters.user_black = (int)numericUpDown5.Value;
                Thread thread1 = new Thread(new ThreadStart(dcraw.Process));
                thread1.Priority = ThreadPriority.Normal;                
                thread1.Start();
                do
                {
                    if (dcraw.estado > 0)
                    {
                        if (dcraw.cancel == 0) label3.Text = "REVELANDO ETAPA " + dcraw.estado.ToString(); else label3.Text = "CANCELANDO: " + dcraw.estado.ToString();
                    }
                    Application.DoEvents();
                    Thread.Sleep(100);
                } while (thread1.IsAlive);
                if (dcraw.estado == 6)
                {
                    if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
                    pictureBox1.Image = (Image)dcraw.img;
                }
                //Clipboard.Clear();
                //Application.DoEvents();
                //Clipboard.SetImage(pictureBox1.Image);

                label3.Text = "LISTO";
                this.Cursor = Cursors.Arrow;
                label3.BackColor = Color.Lime;
                button1.Text="REVELAR";                
                Application.DoEvents();
                RevState = false;
                button1.Enabled = true;
            }else{
                // Cancelar
                label3.BackColor = Color.Orange;
                label3.Text = "CANCELANDO";
                button1.Enabled = false;
                dcraw.cancel = 1;                
            }
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

        private void MainForm_Activated(object sender, EventArgs e)
        {
            string []args=Environment.GetCommandLineArgs();

            if (!Init)
            {
                Init = true;
                dcraw.SetRAWFile(textBox1.Text);
                ResizeViews();
                Clock = new System.Windows.Forms.Timer();
                Clock.Interval = 2000;
                Clock.Start();
                Clock.Tick += new EventHandler(CheckMemTimer);
                CheckMem();
                if (args.Length > 1)
                {
                    textBox1.Text = args[1];
                    Application.DoEvents();
                    Revelar();
                }                
            }
        }

        private void CheckMemTimer(object sender, EventArgs eArgs)
        {
            CheckMem();
        }

        private void CheckMem()
        {
            /* Versión portable 
            if (!InitCounter)
            {
                Thread t = new Thread(InitializeCounter);
                t.Priority = ThreadPriority.Lowest;                
                t.Start();
            }
            if (InitCounter)
            {
                int FreeMb = (int)(performanceCounter1.NextValue() / 1000000.0);
                FreeMem.Text = "FREE MEM: " + FreeMb.ToString() + " Mb";
            }*/
            // Versión no portable
            MEMORYSTATUS buf=new MEMORYSTATUS();
            GlobalMemoryStatus(ref buf);

            uint FreeMb = buf.dwAvailPhys/1000000;
            uint TotalMb = (uint)((float)buf.dwAvailPhys*100/(float)buf.dwTotalPhys);            
            FreeMem.Text = "FREEMEM " +FreeMb.ToString() + " Mb (" + TotalMb.ToString() + "%)";
        }

        private void InitializeCounter()
        {
            //((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).EndInit();
            InitCounter = true;            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dcraw.End();
            dcraw.SetRAWFile(textBox1.Text);
            dcraw.first_time = true;
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

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
