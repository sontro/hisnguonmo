using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using SDA.Desktop.Plugins.SdaLanguage.SdaLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.Desktop.Plugins.SdaLanguage
{
    class HisSdaLanguageProcessor
    {
        [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "SDA.Desktop.Plugins.SdaLanguage",
           "Danh mục",
           "Bussiness",
           4,
           "quy-tai-chinh.png",
           "A",
           Module.MODULE_TYPE_ID__FORM,
           true,
           true)
        ]
        public class SdaLanguageProcessor : ModuleBase, IDesktopRoot
        {
            CommonParam param;
            public SdaLanguageProcessor()
            {
                param = new CommonParam();
            }
            public SdaLanguageProcessor(CommonParam paramBusiness)
            {
                param = (paramBusiness != null ? paramBusiness : new CommonParam());
            }

            public object Run(object[] args)
            {
                object result = null;
                try
                {
                    ISdaLanguage behavior = SdaLanguageFactory.MakeIControl(param, args);
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
}