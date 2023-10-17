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
    class TransactionDepositBehavior : Tool<IDesktopToolContext>, ITransactionDeposit
    {
        Inventec.Desktop.Common.Modules.Module Module;
        TransactionDepositADO ado;
        internal TransactionDepositBehavior()
            : base()
        {

        }

        internal TransactionDepositBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, TransactionDepositADO data)
            : base()
        {
            Module = module;
            ado = data;
        }

        object ITransactionDeposit.Run()
        {
            object result = null;
            try
            {
                result = new frmTransactionDeposit(Module, ado);
                if (result == null) throw new NullReferenceException(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => ado), ado));
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
