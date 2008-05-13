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
    /// <summary>
    /// Descripción breve de MainForm.
    /// </summary>
    unsafe public class MainForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Label label1;
        private Label camera;
        private TextBox textBox1;
        private Label label2;
        private OpenFileDialog openFileDialog1;
        private Button button2;

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
            this.label1 = new System.Windows.Forms.Label();
            this.camera = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 441);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 24);
            this.button1.TabIndex = 0;
            this.button1.Text = "REVELAR";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(570, 401);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(103, 447);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cámara:";
            // 
            // camera
            // 
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
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(595, 477);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.camera);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Name = "MainForm";
            this.Text = "perfectRAW";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void button1_Click(object sender, System.EventArgs e)
        {
            Dcraw dcraw = new Dcraw(textBox1.Text);                        
            
            // Process RAW
            dcraw.GetInfo();
            camera.Text = dcraw.info.camera_make + " " + dcraw.info.camera_model;
            
            dcraw.parameters.user_qual = 1;
            Thread thread1 = new Thread(new ThreadStart(dcraw.Process));
            thread1.Priority = ThreadPriority.BelowNormal;
            thread1.Start();
            do
            {
                Application.DoEvents();
                Thread.Sleep(100);
            } while (thread1.IsAlive);
            pictureBox1.Image = (Image)dcraw.img;
            Application.DoEvents();

            // Change parameters and reprocess RAW            
            dcraw.parameters.use_auto_wb = 1;
            Thread thread2 = new Thread(new ThreadStart(dcraw.Process));
            thread2.Priority = ThreadPriority.BelowNormal;
            thread2.Start();
            do
            {
                Application.DoEvents();
                Thread.Sleep(100);
            } while (thread2.IsAlive);            
            pictureBox1.Image = (Image)dcraw.img;
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
            textBox1.Text = @"c:\dcraw\raws\1.cr2";
        }
    }
}
