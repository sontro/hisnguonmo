using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.Branch
{
    public class BranchWorker
    {
        private const string SOFTWARE_FOLDER = "SOFTWARE";
        private const string COMPANY_FOLDER = "INVENTEC";
        private static readonly string APP_FOLDER = ConfigurationManager.AppSettings["Inventec.Desktop.ApplicationCode"];

        private static RegistryKey appFolder = Registry.CurrentUser.CreateSubKey(SOFTWARE_FOLDER).CreateSubKey(COMPANY_FOLDER).CreateSubKey(APP_FOLDER);


        private static string CurrentStartupPath
        {
            get
            {
                string fullLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
                return System.IO.Path.GetDirectoryName(fullLocation);
            }
        }

        private static long? CurrentBranchId;

        public static void SetBranchId(long branchId)
        {
            try
            {
                CurrentBranchId = branchId;

                appFolder.SetValue(CurrentStartupPath, branchId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public static long GetBranchId()
        {
            if (!CurrentBranchId.HasValue || CurrentBranchId.Value == 0)
            {
                try
                {
                    CurrentBranchId = Inventec.Common.TypeConvert.Parse.ToInt64((appFolder.GetValue(CurrentStartupPath) ?? 0).ToString());
                }
                catch (Exception ex)
                {
                    CurrentBranchId = 0;
                    Inventec.Common.Logging.LogSystem.Debug(ex);
                }
            }

            return CurrentBranchId.Value;
        }
    }
}
