using Inventec.Core;
using HIS.Desktop.Common;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Common.Modules;
using HIS.Desktop.LocalStorage.LocalData;
using Inventec.Common.LocalStorage.SdaConfig;
using LIS.Desktop.Plugins.LisSample.LisSample;

namespace LIS.Desktop.Plugins.LisSample
{
 
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "LIS.Desktop.Plugins.LisSample",
       "Tạo mẫu bệnh phẩm",
       "Common",
       14,
       "mau-benh-pham.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true
       )
    ]
     public class LisSampleProcessor : ModuleBase, IDesktopRoot
    {
       CommonParam param;
        public LisSampleProcessor()
        {
            param = new CommonParam();
        }
        public LisSampleProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }
        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;

                //args là tham số chứa các đối tượng bên ngoài truyền vào

                if (args != null && args.Count() > 0)
                {
                    //Đoạn xử lý phân tích mảng đối tượng truyền vào để lấy ra dữ liệu cần
                    for (int i = 0; i < args.Count(); i++)
                    {
                        //Kiểm tra nếu đối tượng truyền vào có kiểu phù hợp thì lấy giá trị ra để sử dụng
                        if (args[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)args[i];
                        }

                    }

                    result = new frmLisSample(moduleData);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        /// <summary>
        /// Ham tra ve trang thai cua module la enable hay disable
        /// Ghi de gia tri khac theo nghiep vu tung module
        /// </summary>
        /// <returns>true/false</returns>
        public override bool IsEnable()
        {
            bool result = false;
            try
            {
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }

            return result;
        }
    }
}
