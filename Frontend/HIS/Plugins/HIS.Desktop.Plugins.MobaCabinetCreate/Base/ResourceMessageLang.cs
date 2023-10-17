using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MobaCabinetCreate.Base
{
    class ResourceMessageLang
    {
        static System.Resources.ResourceManager languageMessage = new System.Resources.ResourceManager("HIS.Desktop.Plugins.MobaCabinetCreate.Resources.Message.Lang", System.Reflection.Assembly.GetExecutingAssembly());

        internal static string NguoiDungChuaChonThuocVatTu
        {
            get
            {
                try
                {
                    return Inventec.Common.Resource.Get.Value("Plugins_MobaImpMestCreate__NguoiDungChuaChonThuocVatTu", languageMessage, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());
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
                    return Inventec.Common.Resource.Get.Value("Plugins_MobaImpMestCreate__SoLuongThuHoiPhaiLonHonKhong", languageMessage, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());
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
                    return Inventec.Common.Resource.Get.Value("Plugins_MobaImpMestCreate__SoLuongThuHoiKhongDuocLonHonSoLuongKhaDung", languageMessage, Inventec.Desktop.Common.LanguageManager.LanguageManager.GetCulture());
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
