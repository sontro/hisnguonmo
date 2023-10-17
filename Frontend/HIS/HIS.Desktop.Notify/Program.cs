using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Notify
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            try
            {
                string cmdLn = "";
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                log4net.Config.DOMConfigurator.Configure();
                //string cmdLn = "SdaBaseUri|http://sda.12c.vn|ApplicationCode|HIS";
                //foreach (string arg in Environment.GetCommandLineArgs())
                //{
                //    cmdLn += arg;
                //}
                //if (cmdLn.IndexOf('|') == -1)
                //{
                //    return;
                //}

                for (var i = 0; i < args.Length; i++)
                {
                    cmdLn += args[i];
                }
                Application.Run(new NotifyAlert(cmdLn));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

        }
    }
}
