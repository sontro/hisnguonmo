using TYT.Desktop.Plugins.TYTMalaria.TYTMalaria;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TYT.Desktop.Plugins.TYTMalaria
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "TYT.Desktop.Plugins.TYTMalaria",
        "Sốt rét",
        "Common",
        62,
        "bidList.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class TYTMalariaProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TYTMalariaProcessor()
        {
            param = new CommonParam();
        }
        public TYTMalariaProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                ITYTMalaria behavior = TYTFetusBornFactory.MakeITYTFetusExam(param, args);
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
            return false;
        }
    }
}
