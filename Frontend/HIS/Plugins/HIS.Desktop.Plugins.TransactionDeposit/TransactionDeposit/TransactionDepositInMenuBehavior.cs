using HIS.Desktop.ADO;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionDeposit.TransactionDeposit
{
    class TransactionDepositInMenuBehavior : Tool<IDesktopToolContext>, ITransactionDeposit
    {
        Inventec.Desktop.Common.Modules.Module Module;
        internal TransactionDepositInMenuBehavior()
            : base()
        {

        }

        internal TransactionDepositInMenuBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param)
            : base()
        {
            Module = module;
        }

        object ITransactionDeposit.Run()
        {
            object result = null;
            try
            {
                result = new frmTransactionDeposit(Module);
                if (result == null) throw new NullReferenceException("result is null");
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
