using HIS.Desktop.Plugins.ServiceReqUpdateSampleType.UpdateSampleType;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqUpdateSampleType
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
         "HIS.Desktop.Plugins.ServiceReqUpdateSampleType",
         "Cập nhật loại bệnh phẩm",
         "Common",
         14,
         "",
         "A",
         Module.MODULE_TYPE_ID__FORM,
         true,
         true
         )
      ]
    public class ServiceReqUpdateSampleTypeProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ServiceReqUpdateSampleTypeProcessor()
        {
            param = new CommonParam();
        }

        public ServiceReqUpdateSampleTypeProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IUpdateSampleType behavior = UpdateSampleTypeFactory.MakeIControl(param, args);
                result = behavior != null ? (behavior.Run()) : null;
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
            bool result = true;
            try
            {
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
