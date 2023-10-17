using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.Desktop.Plugins.BloodTypeInStock.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCBloodTypeInStock { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCBloodTypeInStock = new ResourceManager("HIS.Desktop.Plugins.BloodTypeInStock.Resources.Lang", typeof(HIS.Desktop.Plugins.BloodTypeInStock.UCBloodTypeInStock).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
