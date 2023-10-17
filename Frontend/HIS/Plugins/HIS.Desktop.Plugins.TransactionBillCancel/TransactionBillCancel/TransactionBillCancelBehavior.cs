using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillCancel.TransactionBillCancel
{
    class TransactionBillCancelBehavior : Tool<IDesktopToolContext>, ITransactionBillCancel
    {
        V_HIS_TRANSACTION transaction;
        Inventec.Desktop.Common.Modules.Module Module;
        internal TransactionBillCancelBehavior()
            : base()
        {

        }

        internal TransactionBillCancelBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, V_HIS_TRANSACTION data)
            : base()
        {
            this.Module = module;
            this.transaction = data;
        }

        object ITransactionBillCancel.Run()
        {
            object result = null;
            try
            {
                result = new frmTransactionBillCancel(Module, transaction);
                if (result == null) throw new NullReferenceException(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => Module), Module));
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
