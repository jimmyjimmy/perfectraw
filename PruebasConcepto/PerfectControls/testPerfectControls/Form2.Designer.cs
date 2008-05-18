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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.showBurnedPixels = new System.Windows.Forms.CheckBox();
            this.perfectView2 = new PerfectControls.PerfectView();
            this.perfectView1 = new PerfectControls.PerfectView();
            ((System.ComponentModel.ISupportInitialize)(this.perfectView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.perfectView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(12, 565);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Cerrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 570);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(125, 566);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 22);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // showBurnedPixels
            // 
            this.showBurnedPixels.AutoSize = true;
            this.showBurnedPixels.Location = new System.Drawing.Point(228, 570);
            this.showBurnedPixels.Name = "showBurnedPixels";
            this.showBurnedPixels.Size = new System.Drawing.Size(142, 17);
            this.showBurnedPixels.TabIndex = 5;
            this.showBurnedPixels.Text = "Mostrar pixels quemados";
            this.showBurnedPixels.UseVisualStyleBackColor = true;
            this.showBurnedPixels.CheckedChanged += new System.EventHandler(this.showBurnedPixels_CheckedChanged);
            // 
            // perfectView2
            // 
            this.perfectView2.Bitmap = null;
            this.perfectView2.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.perfectView2.Location = new System.Drawing.Point(313, 33);
            this.perfectView2.Name = "perfectView2";
            this.perfectView2.OldBitmap = null;
            this.perfectView2.ShowBurnedPixels = false;
            this.perfectView2.Size = new System.Drawing.Size(217, 265);
            this.perfectView2.TabIndex = 1;
            this.perfectView2.TabStop = false;
            this.perfectView2.ViewMode = PerfectControls.PerfectViewMode.VerticalSplit;
            this.perfectView2.Zoom = 1F;
            this.perfectView2.MouseHover += new System.EventHandler(this.PictureBox2_OnMouseHover);
            // 
            // perfectView1
            // 
            this.perfectView1.Bitmap = null;
            this.perfectView1.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.perfectView1.Location = new System.Drawing.Point(0, 33);
            this.perfectView1.Name = "perfectView1";
            this.perfectView1.OldBitmap = null;
            this.perfectView1.ShowBurnedPixels = false;
            this.perfectView1.Size = new System.Drawing.Size(228, 265);
            this.perfectView1.TabIndex = 0;
            this.perfectView1.TabStop = false;
            this.perfectView1.ViewMode = PerfectControls.PerfectViewMode.OneView;
            this.perfectView1.Zoom = 1F;
            this.perfectView1.MouseHover += new System.EventHandler(this.PictureBox1_OnMouseHover);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.showBurnedPixels);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.perfectView2);
            this.Controls.Add(this.perfectView1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Resize += new System.EventHandler(this.Form2_OnResize);
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.perfectView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.perfectView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PerfectControls.PerfectView perfectView1;
        private PerfectControls.PerfectView perfectView2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox showBurnedPixels;

    }
}