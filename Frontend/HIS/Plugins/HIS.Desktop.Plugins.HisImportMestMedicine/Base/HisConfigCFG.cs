using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisImportMestMedicine.Base
{
    class HisConfigCFG
    {
        private const string CONFIG_KEY__APPROVAL_OR_EXP_OR_IMP_LOGINNAME_OPTION = "HIS.Desktop.Plugins.HisExportMestMedicine.ApprovalOrExpOrImpLoginNameOption";

        public static string APPROVAL_OR_EXP_OR_IMP_LOGINNAME_OPTION;

        internal static void LoadConfig()
        {
            try
            {
                APPROVAL_OR_EXP_OR_IMP_LOGINNAME_OPTION = HisConfigs.Get<string>(CONFIG_KEY__APPROVAL_OR_EXP_OR_IMP_LOGINNAME_OPTION);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
