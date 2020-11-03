using NLog;
using System;
using System.Windows.Forms;

namespace ChessClock.UI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new ChessClockApplicationContext());
            }
            catch (Exception e)
            {
                LogManager.GetCurrentClassLogger().Error(e, "Uncaught exception");
                MessageBox.Show(e.Message, "Exception has occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
