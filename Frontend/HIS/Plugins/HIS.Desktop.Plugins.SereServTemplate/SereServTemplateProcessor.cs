using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.SereServTemplate.SereServTemplate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace HIS.Desktop.Plugins.SereServTemplate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.SereServTemplate",
        "Danh mục mẫu xử lý dịch vụ",
        "Common",
        16,
        "dich-vu.png",
        "E",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)
    ]
    public class SereServTemplateProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public SereServTemplateProcessor()
        {
            param = new CommonParam();
        }
        public SereServTemplateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                ISereServTemplate behavior = SereServTemplateFactory.MakeISereServTemplate(param, args);
                result = behavior != null ? (behavior.Run()) : null;
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
            return true;
        }
    }
}
