using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionList.TransactionList
{
    class TransactionListBehavior : Tool<IDesktopToolContext>, ITransactionList
    {
        Inventec.Desktop.Common.Modules.Module Module;
        V_HIS_TREATMENT_FEE treatment = null;
        V_HIS_ACCOUNT_BOOK accountBook = null;

        internal TransactionListBehavior(Inventec.Desktop.Common.Modules.Module moduleData, CommonParam param)
            : base()
        {
            Module = moduleData;
        }

        internal TransactionListBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, V_HIS_TREATMENT_FEE data)
            : base()
        {
            Module = module;
            treatment = data;
        }

        internal TransactionListBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, V_HIS_ACCOUNT_BOOK data)
            : base()
        {
            Module = module;
            accountBook = data;
        }
        object ITransactionList.Run()
        {
            object result = null;
            try
            {

                if (treatment != null)
                {
                    result = new frmTransactionList(Module, treatment);
                }
                else if (accountBook != null)
                {
                    result = new frmTransactionList(Module, accountBook);
                }
                else
                {
                    result = new frmTransactionList(Module);
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
