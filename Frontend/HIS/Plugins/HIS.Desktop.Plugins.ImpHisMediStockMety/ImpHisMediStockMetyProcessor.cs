using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ImpHisMediStockMety.Run;

namespace HIS.Desktop.Plugins.ImpHisMediStockMety
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
           "HIS.Desktop.Plugins.ImpHisMediStockMety",
           "Nhập khẩu Kho - loại thuốc",
           "Common",
           16,
           "kho.png",
           "E",
           Module.MODULE_TYPE_ID__FORM,
           true,
           true)
       ]
    public class ImpHisMediStockMetyProcessor: ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ImpHisMediStockMetyProcessor()
        {
            param = new CommonParam();
        }
        public ImpHisMediStockMetyProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IImpHisMediStockMety behavior = ImpHisMediStockMetyFactory.MakeIHisImport(param, args);
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
