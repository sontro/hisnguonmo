using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DispenseMedicine.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCDispenseMedicine { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCDispenseMedicine = new ResourceManager("HIS.Desktop.Plugins.DispenseMedicine.Resources.Lang", typeof(HIS.Desktop.Plugins.DispenseMedicine.frmDispenseMedicine).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
