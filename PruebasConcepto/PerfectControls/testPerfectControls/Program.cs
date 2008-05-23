using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace test
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Form1 frm = new Form1();
            //frm.Show();

            // Exit application
            //Application.Exit();
            MessageBox.Show("PerfectRaw GUI Test: v 0.7");

            Application.Run(new Form2());
        }
    }
}