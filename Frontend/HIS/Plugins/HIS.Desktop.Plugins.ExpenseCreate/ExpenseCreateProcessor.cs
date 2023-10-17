using HIS.Desktop.Plugins.ExpenseCreate.ExpenseCreate;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpenseCreate
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ExpenseCreate",
        "Nhập khoản chi",
        "Common",
        52,
        "expenseList.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class ExpenseCreateProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExpenseCreateProcessor()
        {
            param = new CommonParam();
        }
        public ExpenseCreateProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                IExpenseCreate behavior = ExpenseCreateFactory.MakeIExpenseCreate(param, args);
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
