using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Transaction.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCTransaction { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCTransaction = new ResourceManager("HIS.Desktop.Plugins.Transaction.Resources.Lang", typeof(HIS.Desktop.Plugins.Transaction.UCTransaction).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
