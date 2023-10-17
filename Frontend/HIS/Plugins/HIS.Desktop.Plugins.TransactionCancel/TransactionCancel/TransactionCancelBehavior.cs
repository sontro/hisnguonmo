using HIS.Desktop.Common;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionCancel.TransactionCancel
{
    class TransactionCancelBehavior : Tool<IDesktopToolContext>, ITransactionCancel
    {
        V_HIS_TRANSACTION transaction;
        long? transaction_id = null;
        DelegateSelectData refreshDelegate = null;
        Inventec.Desktop.Common.Modules.Module Module;
        V_HIS_EXP_MEST_2 expMest = null;
        internal TransactionCancelBehavior()
            : base()
        {

        }

        internal TransactionCancelBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, V_HIS_TRANSACTION data)
            : base()
        {
            this.Module = module;
            this.transaction = data;
        }

        internal TransactionCancelBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, V_HIS_TRANSACTION data, DelegateSelectData _selectData)
            : base()
        {
            this.Module = module;
            this.transaction = data;
            this.refreshDelegate = _selectData;
        }

        internal TransactionCancelBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, long data, V_HIS_EXP_MEST_2 expMestData, DelegateSelectData _selectData)
            : base()
        {
            this.Module = module;
            this.transaction_id = data;
            this.refreshDelegate = _selectData;
            this.expMest = expMestData;
        }

        object ITransactionCancel.Run()
        {
            object result = null;
            try
            {
                Inventec.Common.Logging.LogSystem.Debug("TransactionCancelBehavior transaction: " + Inventec.Common.Logging.LogUtil.TraceData("", transaction));
                Inventec.Common.Logging.LogSystem.Debug("TransactionCancelBehavior transaction_id: " + Inventec.Common.Logging.LogUtil.TraceData("", transaction_id));
                Inventec.Common.Logging.LogSystem.Debug("TransactionCancelBehavior this.expMest: " + Inventec.Common.Logging.LogUtil.TraceData("", this.expMest));
                if (transaction != null)
                {
                    if (this.refreshDelegate != null)
                        result = new frmTransactionCancel(Module, transaction, this.refreshDelegate);
                    else
                        result = new frmTransactionCancel(Module, transaction);
                }
                else if (transaction_id.HasValue)
                {
                    result = new frmTransactionCancel(Module, transaction_id.Value, this.expMest, refreshDelegate);
                }
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
