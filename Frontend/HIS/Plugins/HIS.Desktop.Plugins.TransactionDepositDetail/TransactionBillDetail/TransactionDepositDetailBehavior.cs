using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionDepositDetail.TransactionDepositDetail
{
    class TransactionDepositDetailBehavior : Tool<IDesktopToolContext>, ITransactionDepositDetail
    {
        Inventec.Desktop.Common.Modules.Module Module;
        long billId = 0;
        public TransactionDepositDetailBehavior()
            : base()
        {
        }

        public TransactionDepositDetailBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, long data)
            : base()
        {
            this.billId = data;
            this.Module = module;
        }

        object ITransactionDepositDetail.Run()
        {
            try
            {
                return new frmTransactionDepositDetail(this.Module, this.billId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                //param.HasException = true;
                return null;
            }
        }
    }
}
