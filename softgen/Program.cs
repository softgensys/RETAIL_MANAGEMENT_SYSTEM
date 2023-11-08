namespace softgen
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            frmWelscr frmWelscr = new frmWelscr();
            if (frmWelscr.openyn == false)
            {
                Application.Run(new MainForm());
            }
            else
            {
                
            Application.Run(new frmWelscr());
            }
        }
    }
}