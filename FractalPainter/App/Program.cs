using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                
                var services = new ServiceCollection();

                // start here
                // services.AddSingleton<TInterface, TImplementation>();

                
                Application.Run(new MainForm());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}