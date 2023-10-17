using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentFundList.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmTreatmentList { get; set; }
        internal static ResourceManager LanguageFrmCancelTreatment { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmTreatmentList = new ResourceManager("HIS.Desktop.Plugins.TreatmentFundList.Resources.Lang", typeof(HIS.Desktop.Plugins.TreatmentFundList.frmTreatmentList).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            try
            {
                //LanguageFrmCancelTreatment = new ResourceManager("HIS.Desktop.Plugins.TreatmentFundList.Resources.Lang", typeof(HIS.Desktop.Plugins.TreatmentFundList.frmCancelTreatment).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
