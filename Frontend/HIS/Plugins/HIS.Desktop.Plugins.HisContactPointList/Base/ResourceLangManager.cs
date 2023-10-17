using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisContactPointList.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCHisContactPointList { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCHisContactPointList = new ResourceManager("HIS.Desktop.Plugins.HisContactPointList.Resources.Lang", typeof(HIS.Desktop.Plugins.HisContactPointList.UCHisContactPointList).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
