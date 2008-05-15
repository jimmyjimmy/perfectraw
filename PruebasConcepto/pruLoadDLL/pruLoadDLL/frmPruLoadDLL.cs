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
        [DllImport("pruDLL.dll",CharSet=CharSet.Ansi)]
        static extern int revelar();
        public frmPruLoadDLL()
        {
            InitializeComponent();
        }

        private void btnRevelar_Click(object sender, EventArgs e)
        {
            int i = revelar();
            this.txtResultado.Lines.SetValue(i,this.txtResultado.Lines.Length);

        }
    }
}