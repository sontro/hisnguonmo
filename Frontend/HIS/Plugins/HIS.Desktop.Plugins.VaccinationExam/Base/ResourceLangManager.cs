using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.VaccinationExam.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCVaccinationExam { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                //LanguageUCVaccinationExam = new ResourceManager("HIS.Desktop.Plugins.VaccinationExam.Resources.Lang", typeof(HIS.Desktop.Plugins.VaccinationExam.frmVaccinationExam).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
