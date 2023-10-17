using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TYT.Desktop.Plugins.TytDeath.TytDeath;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;

namespace TYT.Desktop.Plugins.TytDeath
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
          "TYT.Desktop.Plugins.TytDeath",
          "Sổ bệnh nhân HIV",
          "Common",
          16,
          "xuat-kho.png",
          "E",
          Module.MODULE_TYPE_ID__UC,
          true,
          true)
       ]

    public class TytDeathProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public TytDeathProcessor()
        {
            param = new CommonParam();
        }
        public TytDeathProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                ITytDeath behavior = TytDeathFactory.MakeITytDeath(param, args);
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
