using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Common.Logging;

namespace EMR.Desktop.Plugins.EmrFlow.Validate
{
    class ResourcesMassage
    {
        internal static System.Resources.ResourceManager langueMessage = new System.Resources.ResourceManager("EMR.Desktop.Plugins.EmrFlow.Resources.Message.Lang", System.Reflection.Assembly.GetCallingAssembly());
        
        internal static string TruongDuLieuVuotQuaMaxLength
        {
            get
            {
                try
                {
                    return Inventec.Common.Resource.Get.Value("EmrFlow_TruongDuLieuVuotQuaMaxLength", langueMessage, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());
                }
                catch (Exception ex)
                {

                    LogSystem.Warn(ex);
                }
                return "";
            }
        }

     
    }
}
