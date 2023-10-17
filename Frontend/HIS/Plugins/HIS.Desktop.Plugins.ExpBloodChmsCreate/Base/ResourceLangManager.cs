using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpBloodChmsCreate.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCExpMestChmsCreate { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCExpMestChmsCreate = new ResourceManager("HIS.Desktop.Plugins.ExpBloodChmsCreate.Resources.Lang", typeof(HIS.Desktop.Plugins.ExpBloodChmsCreate.UCExpBloodChmsCreate).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
