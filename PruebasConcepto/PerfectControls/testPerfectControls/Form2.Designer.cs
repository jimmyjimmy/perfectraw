namespace test
{
    partial class Form2
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTestSpeed = new System.Windows.Forms.Button();
            this.showBurnedPixels = new System.Windows.Forms.CheckBox();
            this.gbModoVista = new System.Windows.Forms.GroupBox();
            this.rbVistaHorizontal = new System.Windows.Forms.RadioButton();
            this.rbVistaVertical = new System.Windows.Forms.RadioButton();
            this.rbVistaSimple = new System.Windows.Forms.RadioButton();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.perfectView2 = new PerfectControls.PerfectView();
            this.perfectView1 = new PerfectControls.PerfectView();
            this.gbModoVista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.perfectView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.perfectView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            resources.ApplyResources(this.btnExit, "btnExit");
            this.btnExit.Name = "btnExit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnTestSpeed
            // 
            resources.ApplyResources(this.btnTestSpeed, "btnTestSpeed");
            this.btnTestSpeed.Name = "btnTestSpeed";
            this.btnTestSpeed.UseVisualStyleBackColor = true;
            this.btnTestSpeed.Click += new System.EventHandler(this.button2_Click);
            // 
            // showBurnedPixels
            // 
            resources.ApplyResources(this.showBurnedPixels, "showBurnedPixels");
            this.showBurnedPixels.Name = "showBurnedPixels";
            this.showBurnedPixels.UseVisualStyleBackColor = true;
            this.showBurnedPixels.CheckedChanged += new System.EventHandler(this.showBurnedPixels_CheckedChanged);
            // 
            // gbModoVista
            // 
            resources.ApplyResources(this.gbModoVista, "gbModoVista");
            this.gbModoVista.Controls.Add(this.rbVistaHorizontal);
            this.gbModoVista.Controls.Add(this.rbVistaVertical);
            this.gbModoVista.Controls.Add(this.rbVistaSimple);
            this.gbModoVista.Name = "gbModoVista";
            this.gbModoVista.TabStop = false;
            // 
            // rbVistaHorizontal
            // 
            resources.ApplyResources(this.rbVistaHorizontal, "rbVistaHorizontal");
            this.rbVistaHorizontal.Name = "rbVistaHorizontal";
            this.rbVistaHorizontal.UseVisualStyleBackColor = true;
            this.rbVistaHorizontal.CheckedChanged += new System.EventHandler(this.rbVistaHorizontal_CheckedChanged);
            // 
            // rbVistaVertical
            // 
            resources.ApplyResources(this.rbVistaVertical, "rbVistaVertical");
            this.rbVistaVertical.Checked = true;
            this.rbVistaVertical.Name = "rbVistaVertical";
            this.rbVistaVertical.TabStop = true;
            this.rbVistaVertical.UseVisualStyleBackColor = true;
            this.rbVistaVertical.CheckedChanged += new System.EventHandler(this.rbVistaVertical_CheckedChanged);
            // 
            // rbVistaSimple
            // 
            resources.ApplyResources(this.rbVistaSimple, "rbVistaSimple");
            this.rbVistaSimple.Name = "rbVistaSimple";
            this.rbVistaSimple.UseVisualStyleBackColor = true;
            this.rbVistaSimple.CheckedChanged += new System.EventHandler(this.rbVistaSimple_CheckedChanged);
            // 
            // btnOpenFile
            // 
            resources.ApplyResources(this.btnOpenFile, "btnOpenFile");
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // perfectView2
            // 
            this.perfectView2.Bitmap = null;
            this.perfectView2.Cursor = System.Windows.Forms.Cursors.SizeAll;
            resources.ApplyResources(this.perfectView2, "perfectView2");
            this.perfectView2.Name = "perfectView2";
            this.perfectView2.OldBitmap = null;
            this.perfectView2.ShowBurnedPixels = false;
            this.perfectView2.TabStop = false;
            this.perfectView2.ViewMode = PerfectControls.PerfectViewMode.VerticalSplit;
            this.perfectView2.Zoom = 1F;
            this.perfectView2.MouseHover += new System.EventHandler(this.PictureBox2_OnMouseHover);
            // 
            // perfectView1
            // 
            this.perfectView1.Bitmap = null;
            this.perfectView1.Cursor = System.Windows.Forms.Cursors.SizeAll;
            resources.ApplyResources(this.perfectView1, "perfectView1");
            this.perfectView1.Name = "perfectView1";
            this.perfectView1.OldBitmap = null;
            this.perfectView1.ShowBurnedPixels = false;
            this.perfectView1.TabStop = false;
            this.perfectView1.ViewMode = PerfectControls.PerfectViewMode.OneView;
            this.perfectView1.Zoom = 1F;
            this.perfectView1.MouseHover += new System.EventHandler(this.PictureBox1_OnMouseHover);
            // 
            // Form2
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.gbModoVista);
            this.Controls.Add(this.showBurnedPixels);
            this.Controls.Add(this.btnTestSpeed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.perfectView2);
            this.Controls.Add(this.perfectView1);
            this.Name = "Form2";
            this.Resize += new System.EventHandler(this.Form2_OnResize);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.gbModoVista.ResumeLayout(false);
            this.gbModoVista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.perfectView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.perfectView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PerfectControls.PerfectView perfectView1;
        private PerfectControls.PerfectView perfectView2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTestSpeed;
        private System.Windows.Forms.CheckBox showBurnedPixels;
        private System.Windows.Forms.GroupBox gbModoVista;
        private System.Windows.Forms.RadioButton rbVistaHorizontal;
        private System.Windows.Forms.RadioButton rbVistaVertical;
        private System.Windows.Forms.RadioButton rbVistaSimple;
        private System.Windows.Forms.Button btnOpenFile;

    }
}