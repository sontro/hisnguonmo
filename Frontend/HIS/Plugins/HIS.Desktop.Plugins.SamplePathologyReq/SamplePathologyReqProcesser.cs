using HIS.Desktop.Plugins.SamplePathologyReq.SamplePathologyReq;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SamplePathologyReq
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.SamplePathologyReq",
       "Lấy mẫu giải phẫu bệnh",
       "Danh mục",
       4,
       "mau-benh-pham.png",
       "A",
       Module.MODULE_TYPE_ID__UC,
       true,
       true)
    ]

    public class SamplePathologyReqProcesser : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public SamplePathologyReqProcesser()
        {
            param = new CommonParam();
        }
        public SamplePathologyReqProcesser(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                ISamplePathologyReq behavior = SamplePathologyReqFactory.MakeIControl(param, args);
                result = behavior != null ? (object)(behavior.Run()) : null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
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
