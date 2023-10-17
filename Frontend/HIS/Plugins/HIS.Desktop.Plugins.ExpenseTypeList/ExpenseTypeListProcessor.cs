using HIS.Desktop.Plugins.ExpenseTypeList.ExpenseTypeList;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;
using Inventec.Desktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpenseTypeList
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
        "HIS.Desktop.Plugins.ExpenseTypeList",
        "Danh mục khoản kế toán",
        "Common",
        52,
        "3dline_32x32.png",
        "A",
        Module.MODULE_TYPE_ID__FORM,
        true,
        true)]
    public class ExpenseTypeListProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public ExpenseTypeListProcessor()
        {
            param = new CommonParam();
        }
        public ExpenseTypeListProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        object IDesktopRoot.Run(object[] args)
        {
            object result = null;
            try
            {
                IExpenseTypeList behavior = ExpenseTypeListFactory.MakeIExpenseTypeList(param, args);
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
