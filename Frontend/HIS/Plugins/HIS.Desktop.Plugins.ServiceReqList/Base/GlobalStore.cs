using Inventec.Desktop.Common.LanguageManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqList.Base
{
    class GlobalStore
    {
        internal static List<string> TypeFilters = new List<string>();

        internal void LoadTypeFilter()
        {
            try
            {
                TypeFilters = new List<string>();
                TypeFilters.Add(TOI_TAO);
                TypeFilters.Add(PHONG_CHI_DINH);
                TypeFilters.Add(KHOA_CHI_DINH);
                TypeFilters.Add(KHOA_THUC_HIEN);
                TypeFilters.Add(TAT_CA);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal string TOI_TAO
        {
            get
            {
                return Inventec.Common.Resource.Get.Value("frmServiceReqList.ToiTao", Resources.ResourceLanguageManager.LanguagefrmServiceReqList, LanguageManager.GetCulture());
            }
        }
        internal string PHONG_CHI_DINH
        {
            get
            {
                return Inventec.Common.Resource.Get.Value("frmServiceReqList.PhongChiDinh", Resources.ResourceLanguageManager.LanguagefrmServiceReqList, LanguageManager.GetCulture());
            }
        }
        internal string HO_SO_DIEU_TRI
        {
            get
            {
                return Inventec.Common.Resource.Get.Value("frmServiceReqList.HoSoDieuTri", Resources.ResourceLanguageManager.LanguagefrmServiceReqList, LanguageManager.GetCulture());
            }
        }
        internal string BENH_NHAN
        {
            get
            {
                return Inventec.Common.Resource.Get.Value("frmServiceReqList.BenhNhan", Resources.ResourceLanguageManager.LanguagefrmServiceReqList, LanguageManager.GetCulture());
            }
        }
        internal string TAT_CA
        {
            get
            {
                return Inventec.Common.Resource.Get.Value("frmServiceReqList.TatCa", Resources.ResourceLanguageManager.LanguagefrmServiceReqList, LanguageManager.GetCulture());
            }
        }
        internal string KHOA_CHI_DINH
        {
            get
            {
                return Inventec.Common.Resource.Get.Value("frmServiceReqList.KhoaChiDinh", Resources.ResourceLanguageManager.LanguagefrmServiceReqList, LanguageManager.GetCulture());
            }
        }
        internal string KHOA_THUC_HIEN
        {
            get
            {
                return Inventec.Common.Resource.Get.Value("frmServiceReqList.KhoaThucHien", Resources.ResourceLanguageManager.LanguagefrmServiceReqList, LanguageManager.GetCulture());
            }
        }

        internal const string HIS_SERVICE_REQ_GET = "/api/HisServiceReq/Get";
        internal const string HIS_SERE_SERV_GET = "api/HisSereServ/Get";
        internal const string HIS_SERE_SERV_GETVIEW_12 = "api/HisSereServ/GetView12";

        internal const short IS_TRUE = 1;
    }
}
