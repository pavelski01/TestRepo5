using Microsoft.VisualBasic.ApplicationServices;
using RunningApp;
using System;
using System.Windows.Forms;

static class Program
{
    [STAThread]
    static void Main(params string[] arguments)
    {
        var mainForm = new MainForm();
        SingleInstanceApplication.Run(mainForm, NewInstanceHandler);
    }

    public static void NewInstanceHandler(object sender, StartupNextInstanceEventArgs e)
    {
        var commandLineArgument = e.CommandLine[1];

        var application = (SingleInstanceApplication)sender;

        var mainForm = (MainForm)application.ApplicationContext.MainForm;

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
            SingleInstanceApplication app = new SingleInstanceApplication();
            app.MainForm = f;
            app.StartupNextInstance += startupHandler;
            app.Run(Environment.GetCommandLineArgs());
        }
    }
}