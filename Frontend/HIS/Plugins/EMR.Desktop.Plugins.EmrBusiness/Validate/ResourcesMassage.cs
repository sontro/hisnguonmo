using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Common.Logging;

namespace EMR.Desktop.Plugins.EmrBusiness.Validate
{
    class ResourcesMassage
    {
        internal static System.Resources.ResourceManager langueMessage = new System.Resources.ResourceManager("EMR.Desktop.Plugins.EmrBusiness.Resources.Message.Lang", System.Reflection.Assembly.GetExecutingAssembly());
        internal static string MaNghiepVuVuaQuaMaxLangth
        {
            get
            {
                try
                {
                    return Inventec.Common.Resource.Get.Value("EmrBusiness_MaNghiepVuVuaQuaMaxLangth", langueMessage, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());
                }
                catch (Exception ex)
                {
                    LogSystem.Warn(ex);
                    
                }
                return "";
            }
        }
        internal static string TenNghiepVuVuotQuaMaxLangth
        {
            get
            {
                try
                {
                    return Inventec.Common.Resource.Get.Value("EmrBusiness_TenNghiepVuVuotQuaMaxLangth", langueMessage, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());
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
