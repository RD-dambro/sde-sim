using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDE_Sim
{
    static class MyProgram
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form Statistics = new SDESim();
            //Statistics.FormBorderStyle = FormBorderStyle.FixedDialog;
            Application.Run(Statistics);
        }

    }
}
