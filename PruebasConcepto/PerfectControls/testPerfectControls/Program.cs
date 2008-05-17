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
            Application.Run(new Form2());
        }
    }
}