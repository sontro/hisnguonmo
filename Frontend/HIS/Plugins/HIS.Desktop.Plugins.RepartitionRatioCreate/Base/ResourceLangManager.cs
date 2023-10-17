using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RepartitionRatioCreate.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmRepartitionRatioCreate { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmRepartitionRatioCreate = new ResourceManager("HIS.Desktop.Plugins.RepartitionRatioCreate.Resources.Lang", typeof(HIS.Desktop.Plugins.RepartitionRatioCreate.frmRepartitionRatioCreate).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
