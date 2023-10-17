
namespace HIS.Desktop.Plugins.AssignService
{
    internal class AppConfigKeys
    {
        //Cấu hình có lưu token phiên làm việc vào registry phục vụ cho việc đăng nhập lần đầu tiên, nếu lần sau mở phần mềm token vẫn còn hiệu lực thì sẽ tự động đăng nhập luôn
        public const string CONFIG_KEY__HIS_DESKTOP__IS_USE_REGISTRY_TOKEN = "CONFIG_KEY__HIS_DESKTOP__IS_USE_REGISTRY_TOKEN";
        //Cấu hình chế độ bặt tắt lưu dữ liệu cache về máy trạm. Đặt 1 là bật, giá trịkhác là tắt.
        public const string CONFIG_KEY__HIS_DESKTOP__IS_USE_CACHE_LOCAL = "CONFIG_KEY__HIS_DESKTOP_IS_USE_CACHE_LOCAL";

        
        #region Public key
        public const string CONFIG_KEY__MODULE_CHI_DINH_DICH_VU__AN_HIEN_CP_NGOAI_GOI = "CONFIG_KEY__MODULE_CHI_DINH_DICH_VU__AN_HIEN_CP_NGOAI_GOI";
        public const string CONFIG_KEY__MODULE_CHI_DINH_DICH_VU__AN_HIEN_HAO_PHI = "CONFIG_KEY__MODULE_CHI_DINH_DICH_VU__AN_HIEN_HAO_PHI";
        public const string CONFIG_KEY__MODULE_CHI_DINH_DICH_VU__AN_HIEN_GIA_GOI = "CONFIG_KEY__MODULE_CHI_DINH_DICH_VU__AN_HIEN_GIA_GOI";
        public const string CONFIG_KEY_HIS_DESKTOP_ASSIGN_SERVICE_CLOSED_FORM_AFTER_PRINT = "CONFIG_KEY_HIS_DESKTOP_ASSIGN_SERVICE_CLOSE_FORM_AFTER_PRINT";
        public const string CONFIG_KEY_CHOOSING_WHEN_SEARCH = "CONFIG_KEY_HIS_DESKTOP_ASSIGN_SERVICE_IS_NOT_AUTO_CHOOSING_WHEN_SEARCH";
        #endregion
    }
}
