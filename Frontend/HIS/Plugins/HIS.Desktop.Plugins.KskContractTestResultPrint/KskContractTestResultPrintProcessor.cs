using HIS.Desktop.Plugins.KskContractTestResultPrint.KskContractTestResultPrint;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.KskContractTestResultPrint
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.KskContractTestResultPrint",
       "Nhập hao phí trả lại",
       "Common",
       25,
       "mobaImp.jpg",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true)
    ]
    public class KskContractTestResultPrintProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public KskContractTestResultPrintProcessor()
        {
            param = new CommonParam();
        }
        public KskContractTestResultPrintProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            CommonParam param = new CommonParam();
            object result = null;
            try
            {
                IKskContractTestResultPrint behavior = KskContractTestResultPrintFactory.MakeIMobaImpMestCreate(param, args);
                result = behavior != null ? (behavior.Run()) : null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
