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

    public partial class Form1 : Form
    {
        [DllImport(@"pruDLL.dll")]
        static extern int revelar();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int val , ret;
            try
            {
               // string str = this.valor.ToString();
               // val = int.Parse(str);
                ret = revelar();
                this.resultado.AppendText(ret.ToString()+"\n");
            }catch(Exception err)
            {
                this.mensajeError.Text = err.Message;
            }

        }

        private void resultado_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        
        }

    
    }
}