using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MobaExamPresCreate.Resource
{
    class ResourceMessageLang
    {
        static System.Resources.ResourceManager languageMessage = new System.Resources.ResourceManager("HIS.Desktop.Plugins.MobaExamPresCreate.Resource.Message", System.Reflection.Assembly.GetExecutingAssembly());

        internal static string NguoiDungChuaChonThuocVatTu
        {
            get
            {
                try
                {
                    return Inventec.Common.Resource.Get.Value("Plugins_MobaExamPresCreate__NguoiDungChuaChonThuocVatTu", languageMessage, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Warn(ex);
                }
                return "";
            }
        }
        internal static string SoLuongThuHoiPhaiLonHonKhong
        {
            get
            {
                try
                {
                    return Inventec.Common.Resource.Get.Value("Plugins_MobaExamPresCreate__SoLuongThuHoiPhaiLonHonKhong", languageMessage, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Warn(ex);
                }
                return "";
            }
        }
        internal static string SoLuongThuHoiKhongDuocLonHonSoLuongKhaDung
        {
            get
            {
                try
                {
                    return Inventec.Common.Resource.Get.Value("Plugins_MobaExamPresCreate__SoLuongThuHoiKhongDuocLonHonSoLuongKhaDung", languageMessage, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());
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
