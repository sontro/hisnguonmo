using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestChmsUpdate.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCExpMestChmsCreate { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCExpMestChmsCreate = new ResourceManager("HIS.Desktop.Plugins.ExpMestChmsUpdate.Resources.Lang", typeof(HIS.Desktop.Plugins.ExpMestChmsUpdate.frmExpMestChmsUpdate).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
