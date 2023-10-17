using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisMedicalContractList.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCHisMedicalContractList { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCHisMedicalContractList = new ResourceManager("HIS.Desktop.Plugins.HisMedicalContractList.Resources.Lang", typeof(HIS.Desktop.Plugins.HisMedicalContractList.HisMedicalContractList.UCHisMedicalContractList).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
