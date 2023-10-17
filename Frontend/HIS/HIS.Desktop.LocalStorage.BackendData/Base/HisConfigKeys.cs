using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData
{
    class HisConfigKeys
    {
        //Cấu hình chế độ sử dụng api nén. Đặt 1 là bật, giá trị khác là tắt.
        public const string CONFIG_KEY__HIS_DESKTOP__IS_USE_ZIP = "HIS__CONFIG_KEY_IS_USE_ZIP_API";

        public const string CONFIG_KEY__HIS_IS_USE_REDIS_CACHE_SERVER = "HIS.Desktop.IsUseRedisCacheServer";

        public const string CONFIG_KEY__ASSIGN_SERVICE_ALLOW_SHOWING_ANAPATHOGY = "HIS.Desktop.Plugins.AssignService.AllowShowingAnapathology";
    }
}
