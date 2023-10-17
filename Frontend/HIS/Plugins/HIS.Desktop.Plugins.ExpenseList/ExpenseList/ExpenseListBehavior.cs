using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpenseList.ExpenseList
{
    class ExpenseListBehavior : Tool<IDesktopToolContext>, IExpenseList
    {
        object entity;
        public ExpenseListBehavior()
            : base()
        {
        }

        public ExpenseListBehavior(CommonParam param, object filter)
            : base()
        {
            this.entity = filter;
        }

        object IExpenseList.Run()
        {
            try
            {
                return new UCExpenseList();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
