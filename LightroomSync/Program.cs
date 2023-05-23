namespace LightroomSync
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            if (!Directory.Exists(Utils.GetWorkingDir()))
            {
                Directory.CreateDirectory(Utils.GetWorkingDir());
            }
            Directory.SetCurrentDirectory(Utils.GetWorkingDir());

            bool startMinimized = false;
            foreach (string argument in args)
            {
                if (argument == "tray")
                {
                    startMinimized = true;
                }
            }
            
            Application.Run(new Form1(startMinimized));
        }
    }
}