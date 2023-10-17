using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpenseTypeList.ExpenseTypeList
{
    class ExpenseTypeListBehavior : Tool<IDesktopToolContext>, IExpenseTypeList
    {
        Inventec.Desktop.Common.Modules.Module Module;
        public ExpenseTypeListBehavior()
            : base()
        {
        }

        public ExpenseTypeListBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param)
            : base()
        {
            this.Module = module;
        }

        object IExpenseTypeList.Run()
        {
            try
            {

                return new frmExpenseTypeList(Module);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
