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
using WindowsApplication4;
using WeifenLuo.WinFormsUI.Docking;

namespace perfectRAW
{
    /// <summary>
    /// Descripción breve de MainForm.
    /// </summary>
    unsafe public class MainForm : DockContent
    {
        private OpenFileDialog openFileDialog1;
        private Label label3;
        Dcraw dcraw = new Dcraw();
        private WindowsApplication4.pButton button3;
        private bool RevState = false;
        private GroupBox groupBox2;
        private Label FreeMem;
        private System.Windows.Forms.Timer Clock;
        private WindowsApplication4.pButton button1;
        private PictureBox pictureBox1;
        private bool InitCounter = false;
        private Label label2;
        private NumericUpDown numericUpDown3;
        private Label label7;
        private Label label1;
        private Label label8;
        private NumericUpDown numericUpDown4;
        private Label label10;
        private Label label9;
        private NumericUpDown numericUpDown5;
        private TrackBar trackBar1;
        private NumericUpDown numericUpDown2;
        private Label label4;
        private NumericUpDown numericUpDown1;
        private Label label5;
        private TrackBar trackBar2;
        private GroupBox groupBox1;
        private TextBox textBox1;
        private pButton pButton1;
        private bool Init = false;
        BitmapData data;        
        byte* ptr;
        int w, h, wI, hI;
        int extra;        
        int dragFromX;
        int dragFromY;        
        int pX=0;
        int pY=0;
        private const int WHEEL_DELTA = 120;
        float Zoom=1;
        bool bref = false;
        int state = 0;
        bool ICC = false;
        private Label label6;
        bool HideShow = false;
        float[] Zooms ={0.03125F, 0.0434782609F,0.0625F,0.0909090909F,0.125F,0.166666667F,0.250F,0.333333333F,0.5F,1,2,3,4,6,8};
        string[] ZoomsStr ={"1/32","1/23","1/16","1/11","1/8","1/6","1/4","1/3","1/2","1","2","3","4","6","8"};
        private Label label11;
        private NumericUpDown thresholdControl;
        private NumericUpDown medianControl;
        private Label label12;
        private Label label13;
        private ListBox listBox1;
        private Label label14;
        private ListBox listBox2;
        private NumericUpDown numericUpDown6;
        private NumericUpDown numericUpDown7;
        private NumericUpDown numericUpDown8;
        private NumericUpDown numericUpDown9;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label20;
        private ListBox listBox3;
        private Label levelEdgeStr;
        private TrackBar levelEdgeSld;
        private TrackBar levelCellSld;
        private NumericUpDown levelCellNum;
        int ZoomIndex = 9;

        // Código no portable a otras plataformas
        [DllImport("kernel32")]
        static extern void GlobalMemoryStatus(ref MEMORYSTATUS buf);

        [DllImport(@"colormng.dll", CharSet = CharSet.Ansi)]
        static extern void DrawImage(byte* buffer, byte* buffer2, int width, int height, int width2, int heigh2, int extra, int extra2, int x, int y, float z, byte color);

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
            if (Environment.GetCommandLineArgs().Length==1) MessageBox.Show("perfectRAW\nMódulo revelador\nVersión de prueba nº 10\n3 de junio de 2008 a las 21:15");
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new WindowsApplication4.pButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FreeMem = new System.Windows.Forms.Label();
            this.button1 = new WindowsApplication4.pButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pButton1 = new WindowsApplication4.pButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.thresholdControl = new System.Windows.Forms.NumericUpDown();
            this.medianControl = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label14 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown8 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown9 = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.levelEdgeStr = new System.Windows.Forms.Label();
            this.levelEdgeSld = new System.Windows.Forms.TrackBar();
            this.levelCellSld = new System.Windows.Forms.TrackBar();
            this.levelCellNum = new System.Windows.Forms.NumericUpDown();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.medianControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelEdgeSld)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelCellSld)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelCellNum)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Lime;
            this.label3.Location = new System.Drawing.Point(2, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "LISTO";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Cursor = System.Windows.Forms.Cursors.Default;
            this.button3.HaveState = false;
            this.button3.Location = new System.Drawing.Point(835, 659);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 20);
            this.button3.State = false;
            this.button3.TabIndex = 12;
            this.button3.TabStop = false;
            this.button3.Text = "GRABAR JPEG";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.FreeMem);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(798, -4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(153, 37);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            // 
            // FreeMem
            // 
            this.FreeMem.AutoSize = true;
            this.FreeMem.Location = new System.Drawing.Point(1, 25);
            this.FreeMem.Name = "FreeMem";
            this.FreeMem.Size = new System.Drawing.Size(78, 9);
            this.FreeMem.TabIndex = 27;
            this.FreeMem.Text = "FREE RAM: Mb";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Cursor = System.Windows.Forms.Cursors.Default;
            this.button1.HaveState = false;
            this.button1.Location = new System.Drawing.Point(722, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 32);
            this.button1.State = false;
            this.button1.TabIndex = 0;
            this.button1.TabStop = false;
            this.button1.Text = "REVELAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(-9, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1192, 440);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.WaitOnLoad = true;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_OnDoubleClick);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_OnMouseMove);
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_OnMouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_OnMouseUp);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 489);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 9);
            this.label2.TabIndex = 5;
            this.label2.Text = "Archivo RAW:";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown3.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.numericUpDown3.Location = new System.Drawing.Point(306, 580);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(39, 18);
            this.numericUpDown3.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(125, 584);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(175, 9);
            this.label7.TabIndex = 18;
            this.label7.Text = "Recuperación de altas luces";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(363, 588);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 9);
            this.label1.TabIndex = 20;
            this.label1.Text = "Nivel de saturación:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(363, 600);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 9);
            this.label8.TabIndex = 21;
            this.label8.Text = "(dcraw: )";
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown4.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.numericUpDown4.Location = new System.Drawing.Point(493, 589);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(59, 18);
            this.numericUpDown4.TabIndex = 22;
            this.numericUpDown4.TabStop = false;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(363, 613);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 9);
            this.label10.TabIndex = 23;
            this.label10.Text = "Nivel de negro:";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(363, 625);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 9);
            this.label9.TabIndex = 24;
            this.label9.Text = "(dcraw: )";
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown5.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.numericUpDown5.Location = new System.Drawing.Point(493, 614);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(59, 18);
            this.numericUpDown5.TabIndex = 25;
            this.numericUpDown5.TabStop = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(125, 10);
            this.trackBar1.Maximum = 8;
            this.trackBar1.Minimum = -8;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(90, 34);
            this.trackBar1.TabIndex = 16;
            this.trackBar1.TabStop = false;
            this.trackBar1.TickFrequency = 2;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DecimalPlaces = 2;
            this.numericUpDown2.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.numericUpDown2.Location = new System.Drawing.Point(80, 37);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(45, 18);
            this.numericUpDown2.TabIndex = 14;
            this.numericUpDown2.TabStop = false;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 9);
            this.label4.TabIndex = 15;
            this.label4.Text = "Preservar";
            this.label4.Click += new System.EventHandler(this.label4_Click);
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
            this.numericUpDown1.Location = new System.Drawing.Point(80, 15);
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
            this.numericUpDown1.Size = new System.Drawing.Size(45, 18);
            this.numericUpDown1.TabIndex = 8;
            this.numericUpDown1.TabStop = false;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 9);
            this.label5.TabIndex = 10;
            this.label5.Text = "Exposición";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(125, 36);
            this.trackBar2.Maximum = 12;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(90, 34);
            this.trackBar2.TabIndex = 17;
            this.trackBar2.TabStop = false;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.trackBar2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Location = new System.Drawing.Point(119, 507);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 71);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control de exposición";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.textBox1.Location = new System.Drawing.Point(3, 503);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(111, 18);
            this.textBox1.TabIndex = 4;
            this.textBox1.TabStop = false;
            this.textBox1.WordWrap = false;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // pButton1
            // 
            this.pButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pButton1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pButton1.HaveState = false;
            this.pButton1.Location = new System.Drawing.Point(4, 522);
            this.pButton1.Name = "pButton1";
            this.pButton1.Size = new System.Drawing.Size(109, 11);
            this.pButton1.State = false;
            this.pButton1.TabIndex = 29;
            this.pButton1.TabStop = false;
            this.pButton1.Text = "Browse";
            this.pButton1.UseVisualStyleBackColor = true;
            this.pButton1.Click += new System.EventHandler(this.pButton1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-1, -1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 9);
            this.label6.TabIndex = 30;
            this.label6.Text = "Zoom: ";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(125, 493);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(143, 9);
            this.label11.TabIndex = 18;
            this.label11.Text = "Reducción de ruido raw";
            // 
            // thresholdControl
            // 
            this.thresholdControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.thresholdControl.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.thresholdControl.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.thresholdControl.Location = new System.Drawing.Point(276, 490);
            this.thresholdControl.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.thresholdControl.Name = "thresholdControl";
            this.thresholdControl.Size = new System.Drawing.Size(45, 18);
            this.thresholdControl.TabIndex = 31;
            this.thresholdControl.TabStop = false;
            // 
            // medianControl
            // 
            this.medianControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.medianControl.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.medianControl.Location = new System.Drawing.Point(306, 662);
            this.medianControl.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.medianControl.Name = "medianControl";
            this.medianControl.Size = new System.Drawing.Size(39, 18);
            this.medianControl.TabIndex = 33;
            this.medianControl.TabStop = false;
            this.medianControl.ValueChanged += new System.EventHandler(this.medianControl_ValueChanged);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(125, 665);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(148, 9);
            this.label12.TabIndex = 32;
            this.label12.Text = "Pasos de filtro mediana";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(126, 606);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(166, 9);
            this.label13.TabIndex = 34;
            this.label13.Text = "Algoritmo de interpolación";
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 9;
            this.listBox1.Items.AddRange(new object[] {
            "BILINEAL",
            "VNG",
            "VNG-4",
            "PPG",
            "AHD",
            "CPM-4"});
            this.listBox1.Location = new System.Drawing.Point(306, 601);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(53, 58);
            this.listBox1.TabIndex = 35;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(352, 493);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(121, 9);
            this.label14.TabIndex = 36;
            this.label14.Text = "Balance de blancos";
            // 
            // listBox2
            // 
            this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox2.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 9;
            this.listBox2.Items.AddRange(new object[] {
            "CáMARA",
            "AUTO",
            "USUARIO"});
            this.listBox2.Location = new System.Drawing.Point(477, 491);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(53, 31);
            this.listBox2.TabIndex = 37;
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown6.DecimalPlaces = 2;
            this.numericUpDown6.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.numericUpDown6.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericUpDown6.Location = new System.Drawing.Point(541, 507);
            this.numericUpDown6.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown6.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            -2147483648});
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(45, 18);
            this.numericUpDown6.TabIndex = 38;
            this.numericUpDown6.TabStop = false;
            this.numericUpDown6.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown7
            // 
            this.numericUpDown7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown7.DecimalPlaces = 2;
            this.numericUpDown7.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.numericUpDown7.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericUpDown7.Location = new System.Drawing.Point(592, 507);
            this.numericUpDown7.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown7.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            -2147483648});
            this.numericUpDown7.Name = "numericUpDown7";
            this.numericUpDown7.Size = new System.Drawing.Size(45, 18);
            this.numericUpDown7.TabIndex = 39;
            this.numericUpDown7.TabStop = false;
            this.numericUpDown7.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown8
            // 
            this.numericUpDown8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown8.DecimalPlaces = 2;
            this.numericUpDown8.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.numericUpDown8.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericUpDown8.Location = new System.Drawing.Point(643, 507);
            this.numericUpDown8.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown8.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            -2147483648});
            this.numericUpDown8.Name = "numericUpDown8";
            this.numericUpDown8.Size = new System.Drawing.Size(45, 18);
            this.numericUpDown8.TabIndex = 40;
            this.numericUpDown8.TabStop = false;
            this.numericUpDown8.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown9
            // 
            this.numericUpDown9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown9.DecimalPlaces = 2;
            this.numericUpDown9.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.numericUpDown9.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericUpDown9.Location = new System.Drawing.Point(694, 507);
            this.numericUpDown9.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown9.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            -2147483648});
            this.numericUpDown9.Name = "numericUpDown9";
            this.numericUpDown9.Size = new System.Drawing.Size(45, 18);
            this.numericUpDown9.TabIndex = 41;
            this.numericUpDown9.TabStop = false;
            this.numericUpDown9.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Location = new System.Drawing.Point(544, 490);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 13);
            this.label15.TabIndex = 42;
            this.label15.Text = "CWB1";
            this.label15.Visible = false;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Location = new System.Drawing.Point(593, 490);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 13);
            this.label16.TabIndex = 43;
            this.label16.Text = "CWB1";
            this.label16.Visible = false;
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label17.Location = new System.Drawing.Point(644, 490);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 13);
            this.label17.TabIndex = 44;
            this.label17.Text = "CWB1";
            this.label17.Visible = false;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label18.Location = new System.Drawing.Point(695, 490);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 13);
            this.label18.TabIndex = 45;
            this.label18.Text = "CWB1";
            this.label18.Visible = false;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(352, 531);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(135, 9);
            this.label20.TabIndex = 46;
            this.label20.Text = "equilibrado de verdes";
            // 
            // listBox3
            // 
            this.listBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox3.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.listBox3.FormattingEnabled = true;
            this.listBox3.ItemHeight = 9;
            this.listBox3.Items.AddRange(new object[] {
            "Ninguno",
            "Equilibrado global",
            "Equilibrado global+local"});
            this.listBox3.Location = new System.Drawing.Point(493, 540);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(163, 31);
            this.listBox3.TabIndex = 47;
            this.listBox3.SelectedIndexChanged += new System.EventHandler(this.listBox3_SelectedIndexChanged);
            // 
            // levelEdgeStr
            // 
            this.levelEdgeStr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.levelEdgeStr.AutoSize = true;
            this.levelEdgeStr.Location = new System.Drawing.Point(352, 543);
            this.levelEdgeStr.Name = "levelEdgeStr";
            this.levelEdgeStr.Size = new System.Drawing.Size(112, 9);
            this.levelEdgeStr.TabIndex = 48;
            this.levelEdgeStr.Text = "preservar bordes";
            this.levelEdgeStr.Visible = false;
            // 
            // levelEdgeSld
            // 
            this.levelEdgeSld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.levelEdgeSld.Location = new System.Drawing.Point(397, 555);
            this.levelEdgeSld.Minimum = -10;
            this.levelEdgeSld.Name = "levelEdgeSld";
            this.levelEdgeSld.Size = new System.Drawing.Size(91, 34);
            this.levelEdgeSld.TabIndex = 18;
            this.levelEdgeSld.TabStop = false;
            this.levelEdgeSld.Visible = false;
            this.levelEdgeSld.Scroll += new System.EventHandler(this.levelEdgeSld_Scroll);
            // 
            // levelCellSld
            // 
            this.levelCellSld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.levelCellSld.Location = new System.Drawing.Point(397, 555);
            this.levelCellSld.Maximum = 16;
            this.levelCellSld.Minimum = 2;
            this.levelCellSld.Name = "levelCellSld";
            this.levelCellSld.Size = new System.Drawing.Size(91, 34);
            this.levelCellSld.TabIndex = 50;
            this.levelCellSld.TabStop = false;
            this.levelCellSld.Value = 8;
            this.levelCellSld.Visible = false;
            this.levelCellSld.Scroll += new System.EventHandler(this.levelCellSld_Scroll);
            // 
            // levelCellNum
            // 
            this.levelCellNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.levelCellNum.Font = new System.Drawing.Font("Victor\'s Pixel Font", 8F);
            this.levelCellNum.Location = new System.Drawing.Point(355, 556);
            this.levelCellNum.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.levelCellNum.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.levelCellNum.Name = "levelCellNum";
            this.levelCellNum.Size = new System.Drawing.Size(45, 18);
            this.levelCellNum.TabIndex = 51;
            this.levelCellNum.TabStop = false;
            this.levelCellNum.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.levelCellNum.Visible = false;
            this.levelCellNum.ValueChanged += new System.EventHandler(this.numericUpDown10_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 10);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(952, 681);
            this.Controls.Add(this.levelCellNum);
            this.Controls.Add(this.levelCellSld);
            this.Controls.Add(this.levelEdgeSld);
            this.Controls.Add(this.levelEdgeStr);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.numericUpDown9);
            this.Controls.Add(this.numericUpDown8);
            this.Controls.Add(this.numericUpDown7);
            this.Controls.Add(this.numericUpDown6);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.medianControl);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.thresholdControl);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.pButton1);
            this.Controls.Add(this.groupBox1);
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
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Font = new System.Drawing.Font("Victor\'s Pixel Font", 7F);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(960, 545);
            this.Name = "MainForm";
            this.TabText = "perfectRAW";
            this.Text = "perfectRAW";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_OnPaint);
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Resize += new System.EventHandler(this.Form1_OnResize);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.medianControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelEdgeSld)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelCellSld)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelCellNum)).EndInit();
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
            if (HideShow)
            {
                h = this.Height;
                pictureBox1.Top = 0;                
            }
            else
            {
                h = this.Height - 265;
                pictureBox1.Top = 35;             
            }            
            w = this.Width;
            pictureBox1.Width = w;
            pictureBox1.Left = 0;            
            pictureBox1.Height = h;                        
            pictureBox1.Focus();
            bref = true;
            Refresh();
            Invalidate();
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
                if (dcraw.first_time) label3.Text = "LEYENDO RAW";
                this.Cursor = Cursors.WaitCursor;
                label3.BackColor = Color.Red;
                Application.DoEvents();
                dcraw.GetInfo();
                label8.Text = "(dcraw: " + dcraw.sat_level.ToString() + ")";
                label9.Text = "(dcraw: " + dcraw.black_level.ToString() + ")";
                label15.Text = dcraw.cam_WB[0].ToString();
                label16.Text = dcraw.cam_WB[1].ToString();
                label17.Text = dcraw.cam_WB[2].ToString();
                label18.Text = dcraw.cam_WB[3].ToString();
                if (dcraw.first_time) numericUpDown4.Value = dcraw.sat_level;
                if (dcraw.first_time) numericUpDown5.Value = dcraw.black_level;
                //camera.Text = dcraw.info.camera_make + " " + dcraw.info.camera_model;
                dcraw.cancel = 0;
                dcraw.estado = -1;
                if (dcraw.first_time) label3.Text = "REVELANDO ETAPA 1";
                this.Cursor = Cursors.WaitCursor;
                label3.BackColor = Color.Red;
                Application.DoEvents();                
                dcraw.parameters.user_gamma = 1; // Sin aplicar gamma
                dcraw.parameters.output_color = 0; // Pedimos color RAW a dcraw.dll
                dcraw.parameters.exposure = (float)Math.Pow(2.0, (double)numericUpDown1.Value);
                dcraw.parameters.preserve = (float)numericUpDown2.Value;
                dcraw.parameters.highlight = (int)numericUpDown3.Value;
                dcraw.parameters.threshold = (int)thresholdControl.Value;
                dcraw.parameters.med_passes = (int)medianControl.Value;
                dcraw.parameters.level_greens = (int)listBox3.SelectedIndex;
                dcraw.parameters.level_cell = (int)levelCellNum.Value;
                if (listBox1.SelectedIndex < 2)
                {
                    dcraw.parameters.user_qual = (int)listBox1.SelectedIndex;
                    dcraw.parameters.four_color_rgb=0;
                }
                else
                {
                    if (listBox1.SelectedIndex == 2)
                    {
                        dcraw.parameters.user_qual = 1;
                        dcraw.parameters.four_color_rgb = 1;
                    }
                    else
                    {
                        if (listBox1.SelectedIndex < 5)
                        {
                            dcraw.parameters.user_qual = (int)listBox1.SelectedIndex - 1;
                            dcraw.parameters.four_color_rgb = 0;
                        }
                        else
                        {
                            dcraw.parameters.user_qual = (int)listBox1.SelectedIndex - 1;
                            dcraw.parameters.four_color_rgb = 1;
                        }
                    }
                }
                switch (listBox2.SelectedIndex)
                {
                    case 0:
                        dcraw.parameters.use_camera_wb = 1;
                        dcraw.parameters.use_auto_wb=0;
                        fixed (double* f = dcraw.parameters.aber)
                        {
                            *f = 1.0;
                            *(f + 1) = 1.0;
                            *(f + 2) = 1.0;
                            *(f + 3) = 1.0;
                        }
                        break;
                    case 1:
                        dcraw.parameters.use_camera_wb = 0;
                        dcraw.parameters.use_auto_wb = 1;
                        fixed (double* f = dcraw.parameters.aber)
                        {
                            *f = 1.0;
                            *(f + 1) = 1.0;
                            *(f + 2) = 1.0;
                            *(f + 3) = 1.0;
                        }
                        break;
                    case 2:
                        dcraw.parameters.use_camera_wb = 0;
                        dcraw.parameters.use_auto_wb = 0;
                        fixed(double* f=dcraw.parameters.aber){
                            *f = (double)numericUpDown6.Value;
                            *(f+1) = (double)numericUpDown7.Value;
                            *(f+2) = (double)numericUpDown8.Value;
                            *(f+3) = (double)numericUpDown9.Value;
                        }
                        break;
                }
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
                    data = dcraw.img.LockBits(new Rectangle(0, 0, dcraw.img.Width, dcraw.img.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    extra = data.Stride - data.Width * 3;
                    ptr = (byte*)(data.Scan0);
                    wI = data.Width;
                    hI = data.Height;
                    bref = true;
                    this.Invalidate();// pictureBox1.Image = (Image)dcraw.img;
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
            FreeMem.Text = "FREE MEM: " +FreeMb.ToString() + " Mb (" + TotalMb.ToString() + "%)";
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
            trackBar1.Value = (int)numericUpDown1.Value;
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

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            numericUpDown1.Value=(decimal)trackBar1.Value;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            numericUpDown2.Value = (decimal)trackBar2.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            trackBar2.Value = (int)numericUpDown2.Value;
        }

        private void pButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 1;
            listBox3.SelectedIndex = 0;
        }

        private void Form1_OnPaint(object sender, PaintEventArgs e)
        {
            if((bref)&&(dcraw.estado==6))
            {
                Bitmap bmp = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                BitmapData data1 = bmp.LockBits(new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                int extra1 = data1.Stride - data1.Width * 3;
                unsafe
                {
                    byte* ptrOut = (byte*)(data1.Scan0);                    
                    DrawImage(ptrOut, ptr, pictureBox1.Width, pictureBox1.Height, wI, hI, extra1, extra, pX, pY, Zoom, 128);
                }
                bmp.UnlockBits(data1);
                if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
                pictureBox1.Image = bmp;
                bref = false;
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                HideShow = !HideShow;
                ResizeViews();
            }
            base.OnKeyUp(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            pictureBox1.Focus();            

            if (e.Delta == WHEEL_DELTA)
            {
                MakeZoom(1,e.X,e.Y);
            }

            if (e.Delta == -WHEEL_DELTA)
            {
                MakeZoom(-1, e.X, e.Y);
            }
            Invalidate();
            base.OnMouseWheel(e);
        }

        private void MakeZoom(int ZoomQ, int eX, int eY)
        {
            int bX, bY;
            bool f = false;

            eX -= pictureBox1.Left;
            eY -= pictureBox1.Top;
            bX = (int)(pX + eX / Zoom);
            bY = (int)(pY + eY / Zoom);

            ZoomIndex += ZoomQ;
            if (ZoomIndex < 0)
            {
                ZoomIndex = 0;
                f = true;
            }
            if (ZoomIndex > 14)
            {
                ZoomIndex = 14;
                f = true;
            }            
            Zoom = Zooms[ZoomIndex];
            label6.Text = "Zoom: x" + ZoomsStr[ZoomIndex];

            if (!f)
            {
                pX = (int)(bX - eX/Zoom + 1);
                pY = (int)(bY - eY/Zoom + 1);
                if (pX > wI - (int)(w / Zoom)) pX = wI - (int)(w / Zoom);
                if (pY > hI - (int)(h / Zoom)) pY = hI - (int)(h / Zoom);
                if (pX < 0) pX = 0;
                if (pY < 0) pY = 0;
                bref = true;
            }
        }

        private void PictureBox1_OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                state = 1;
                dragFromX = pX + (int)(e.X / Zoom);
                dragFromY = pY + (int)(e.Y / Zoom);
                this.Cursor = Cursors.NoMove2D;
                Invalidate();
            }
        }

        private void PictureBox1_OnMouseMove(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            
            if (state == 1)
            {
                pX = dragFromX - (int)(e.X / Zoom);
                pY = dragFromY - (int)(e.Y / Zoom);
                if (pX < 0) pX = 0;
                if (pY < 0) pY = 0;
                if (pX > wI - (int)(w / Zoom)) pX = wI - (int)(w / Zoom);
                if (pY > hI - (int)(h / Zoom)) pY = hI - (int)(h / Zoom);
                bref = true;
                Refresh();
                Invalidate();
            }
        }

        private void PictureBox1_OnMouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && (state == 1))
            {
                state = 0;
                this.Cursor = Cursors.Hand;
                Invalidate();
            }
        }        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Focus();
        }

        private void pictureBox1_OnDoubleClick(object sender, EventArgs e)
        {
            pictureBox1.Focus();
            ZoomIndex = 9;
            MakeZoom(0, 0, 0);
        }

        private void medianControl_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox3.SelectedIndex>1){
                levelEdgeStr.Text = "Tamaño de celda";
                levelCellNum.Visible = false;
                levelCellSld.Visible = false;                
            }else{
                levelEdgeStr.Visible = false;
                levelCellNum.Visible = false;
                levelCellSld.Visible = false;
            }
        }

        private void levelEdgeNum_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void levelEdgeSld_Scroll(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown10_ValueChanged(object sender, EventArgs e)
        {
            levelCellSld.Value = (int)levelCellNum.Value;
        }

        private void levelCellSld_Scroll(object sender, EventArgs e)
        {
            levelCellNum.Value = (decimal)levelCellSld.Value;
        }

    }
}
