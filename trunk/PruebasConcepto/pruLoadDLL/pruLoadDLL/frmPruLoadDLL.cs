using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace pruLoadDLL
{
    public partial class frmPruLoadDLL : Form
    {
        [DllImport("pruDLL.dll")]
        static extern int revelar();
        [DllImport("pruDLL2.dll")]
        static extern int revelar2();

        public frmPruLoadDLL()
        {
            InitializeComponent();
        }

        private void btnRevelar_Click(object sender, EventArgs e)
        {
            int i = revelar();
            this.txtResultado.AppendText(i + "\n");
            i = revelar2();
            this.txtResultado2.AppendText(i + "\n");


        }

         }
}