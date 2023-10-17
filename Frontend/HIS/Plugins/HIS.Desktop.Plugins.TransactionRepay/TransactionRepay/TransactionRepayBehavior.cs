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

namespace HIS.Desktop.Plugins.TransactionRepay.TransactionRepay
{
    class TransactionRepayBehavior : Tool<IDesktopToolContext>, ITransactionRepay
    {
        Inventec.Desktop.Common.Modules.Module moduleData;
        TransactionRepayADO ado;

        internal TransactionRepayBehavior()
            : base()
        {

        }

        internal TransactionRepayBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, TransactionRepayADO data)
            : base()
        {
            moduleData = module;
            ado = data;
        }

        object ITransactionRepay.Run()
        {
            object result = null;
            try
            {
                result = new frmTransactionRepay(moduleData, ado);
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
