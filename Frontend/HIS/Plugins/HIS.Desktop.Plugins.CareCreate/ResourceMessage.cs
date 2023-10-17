using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CareCreate
{
    class ResourceMessage
    {
        static System.Resources.ResourceManager languageMessage = new System.Resources.ResourceManager("HIS.Desktop.Plugins.CareCreate.Resources.Message.Lang", System.Reflection.Assembly.GetExecutingAssembly());

        internal static string ChamSoc_DuLieuChiTietKhongTheCungLoaiChamSoc
        {
            get
            {
                try
                {
                    return Inventec.Common.Resource.Get.Value("ChamSoc_DuLieuChiTietKhongTheCungLoaiChamSoc", languageMessage, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Warn(ex);
                }
                return "";
            }
        }
    }
}
