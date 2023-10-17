using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MaterialType
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCMaterialType { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCMaterialType = new ResourceManager("HIS.Desktop.Plugins.MaterialType.Resources.Lang", typeof(HIS.Desktop.Plugins.MaterialType.MaterialTypeList.UCMaterialTypeList).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
