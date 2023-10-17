using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DiscountSereServ.Base
{
    class ResourceMessageLang
    {
        static System.Resources.ResourceManager languageMessage = new System.Resources.ResourceManager("HIS.Desktop.Plugins.DiscountSereServ.Resources.Message.Lang", System.Reflection.Assembly.GetExecutingAssembly());

        internal static string PhanTramChietKhauBeHonKhong
        {
            get
            {
                try
                {
                    return Inventec.Common.Resource.Get.Value("Plugins_DiscountSereServ__PhanTramChietKhauBeHonKhong", languageMessage, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Error(ex);
                }
                return "";
            }
        }

        internal static string PhanTramChietKhauLonHonMoTram
        {
            get
            {
                try
                {
                    return Inventec.Common.Resource.Get.Value("Plugins_DiscountSereServ__PhanTramChietKhauLonHonMoTram", languageMessage, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Error(ex);
                }
                return "";
            }
        }

        internal static string SoTienChietKhauBeHonKhong
        {
            get
            {
                try
                {
                    return Inventec.Common.Resource.Get.Value("Plugins_DiscountSereServ__SoTienChietKhauBeHonKhong", languageMessage, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Error(ex);
                }
                return "";
            }
        }

        internal static string SoTienChietKhauLonHonSoTienBenhNhanPhaiTra
        {
            get
            {
                try
                {
                    return Inventec.Common.Resource.Get.Value("Plugins_DiscountSereServ__SoTienChietKhauLonHonSoTienBenhNhanPhaiTra", languageMessage, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Error(ex);
                }
                return "";
            }
        }
    }
}
