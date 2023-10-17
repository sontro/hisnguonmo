using Inventec.Common.Logging;
using System;
using System.Resources;

namespace HIS.Desktop.Base
{
    class ResouceManager
    {
        internal static void InitResourceLanguageManager()
        {
            try
            {
                HIS.Desktop.Resources.ResourceLanguageManager.LanguageFrmLogin = new ResourceManager("HIS.Desktop.Resources.Lang", typeof(HIS.Desktop.Modules.Login.frmLogin).Assembly);
                HIS.Desktop.Resources.ResourceLanguageManager.LanguageFrmMain = new ResourceManager("HIS.Desktop.Resources.Lang", typeof(HIS.Desktop.Modules.Main.frmMain).Assembly);

                //Inventec.UC.ListReports.Base.ResouceManager.ResourceLanguageManager();
                //Inventec.UC.ListReportType.Base.ResouceManager.ResourceLanguageManager();
                //His.UC.UCHein.Base.ResouceManager.ResourceLanguageManager();
                //HIS.UC.FormType.Base.ResouceManager.ResourceLanguageManager();
                //His.UC.CreateReport.Base.ResouceManager.ResourceLanguageManager();
            }
            catch (Exception ex)
            {
                LogSystem.Warn(ex);
            }
        }
    }
}
