using Microsoft.VisualBasic.ApplicationServices;

namespace RunningAppCore
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params string[] arguments)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            var mainForm = new MainForm();
            SingleInstanceApplication.Run(mainForm, NewInstanceHandler);
        }

        public static void NewInstanceHandler(object sender, StartupNextInstanceEventArgs e)
        {
            var commandLineArgument = e.CommandLine[1];

            var application = (SingleInstanceApplication)sender;

            var mainForm = (MainForm)application.ApplicationContext.MainForm!;

            mainForm.Text = commandLineArgument;

            e.BringToForeground = false;
        }

        public class SingleInstanceApplication : WindowsFormsApplicationBase
        {
            private SingleInstanceApplication()
            {
                IsSingleInstance = true;
            }

            public static void Run(Form f, StartupNextInstanceEventHandler startupHandler)
            {
                SingleInstanceApplication app = new()
                {
                    MainForm = f
                };
                app.StartupNextInstance += startupHandler;
                app.Run(Environment.GetCommandLineArgs());
            }
        }
    }
}