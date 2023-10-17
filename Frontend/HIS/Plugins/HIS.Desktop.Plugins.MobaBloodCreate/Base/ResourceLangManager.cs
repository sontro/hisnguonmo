using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MobaBloodCreate.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmMobaImpMestCreate { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmMobaImpMestCreate = new ResourceManager("HIS.Desktop.Plugins.MobaBloodCreate.Resources.Lang", typeof(HIS.Desktop.Plugins.MobaBloodCreate.frmMobaBloodCreate).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
