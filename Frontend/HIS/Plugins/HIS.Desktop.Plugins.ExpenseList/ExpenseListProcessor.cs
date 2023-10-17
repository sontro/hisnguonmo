using HIS.Desktop.Plugins.ExpenseList.ExpenseList;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpenseList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ExpenseList",
        "Danh Sách Chi",
        "Common",
        52,
        "expenseList.png",
        "A",
        Module.MODULE_TYPE_ID__UC,
        true,
        true)]
    public class ExpenseListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExpenseListProcessor()
        {
            param = new CommonParam();
        }
        public ExpenseListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IExpenseList behavior = ExpenseListFactory.MakeIExpenseList(param, null);
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
