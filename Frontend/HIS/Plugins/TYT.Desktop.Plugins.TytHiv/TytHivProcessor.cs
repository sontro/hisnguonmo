using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TYT.Desktop.Plugins.TytHiv.TytHiv;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace TYT.Desktop.Plugins.TytHiv
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
          "TYT.Desktop.Plugins.TytHiv",
          "Sổ bệnh nhân HIV",
          "Common",
          16,
          "xuat-kho.png",
          "E",
          Module.MODULE_TYPE_ID__UC,
          true,
          true)
       ]

    public class TytHivProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TytHivProcessor()
        {
            param = new CommonParam();
        }
        public TytHivProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                ITytHiv behavior = TytHivFactory.MakeITytHiv(param, args);
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
