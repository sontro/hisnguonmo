using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExecuteRoom.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCExecuteRoom { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCExecuteRoom = new ResourceManager("HIS.Desktop.Plugins.ExecuteRoom.Resources.Lang", typeof(HIS.Desktop.Plugins.ExecuteRoom.UCExecuteRoom).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
