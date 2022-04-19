using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Thread thread1 = new Thread(() =>
            {
                Application.Run(new ATM());
            });
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.Start();

            Thread thread2 = new Thread(() =>
            {
                Application.Run(new ATM());
            });
            thread2.SetApartmentState(ApartmentState.STA);
            thread2.Start();
        }
    }
}
