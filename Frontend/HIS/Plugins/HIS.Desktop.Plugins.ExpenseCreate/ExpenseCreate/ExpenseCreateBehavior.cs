using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpenseCreate.ExpenseCreate
{
    class ExpenseCreateBehavior : Tool<IDesktopToolContext>, IExpenseCreate
    {
        Inventec.Desktop.Common.Modules.Module Module;
        public ExpenseCreateBehavior()
            : base()
        {
        }

        public ExpenseCreateBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param)
            : base()
        {
            this.Module = module;
        }

        object IExpenseCreate.Run()
        {
            try
            {
                return new frmExpenseCreate(this.Module);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
