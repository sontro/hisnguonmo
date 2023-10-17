using HIS.Desktop.ADO;
using HIS.Desktop.Plugins.TransactionNumOrderUpdate;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionRepay.NumOrderUpdate
{
    class NumOrderUpdateBehavior : Tool<IDesktopToolContext>, INumOrderUpdate
    {
        Inventec.Desktop.Common.Modules.Module moduleData;
        V_HIS_TRANSACTION transaction;

        internal NumOrderUpdateBehavior()
            : base()
        {

        }

        internal NumOrderUpdateBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, V_HIS_TRANSACTION data)
            : base()
        {
            moduleData = module;
            transaction = data;
        }

        object INumOrderUpdate.Run()
        {
            object result = null;
            try
            {
                result = new frmUpdateNumOrder(moduleData, transaction);
                if (result == null) throw new NullReferenceException(LogUtil.TraceData("transaction", transaction));
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
