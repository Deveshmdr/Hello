using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]



        static void MyMethod()
        {
            Console.WriteLine("Program has just been Executed.");
            MessageBox.Show("Press Ok to Continue...!!!");
        }
        static void ByMethod(string fname)
        {
            Console.WriteLine(fname + " is the creator of this application " );
        }
        static void Main()
        {
            ByMethod("Devesh Manandhar");
            MyMethod();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
           

        }
    }
}
