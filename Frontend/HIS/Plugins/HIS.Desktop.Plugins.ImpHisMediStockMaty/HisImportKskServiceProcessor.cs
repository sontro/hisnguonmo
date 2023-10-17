using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ImpHisMediStockMaty.Run;

namespace HIS.Desktop.Plugins.ImpHisMediStockMaty
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.ImpHisMediStockMaty",
           "Nhập khẩu Kho - loại vật tư",
           "Common",
           16,
           "kho.png",
           "E",
           Module.MODULE_TYPE_ID__FORM,
           true,
           true)
       ]
    public class ImpHisMediStockMatyProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ImpHisMediStockMatyProcessor()
        {
            param = new CommonParam();
        }
        public ImpHisMediStockMatyProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IImpHisMediStockMaty behavior = ImpHisMediStockMatyFactory.MakeIHisImport(param, args);
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
