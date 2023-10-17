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

namespace HIS.Desktop.Plugins.TransactionDebt.TransactionDebt
{
    class TransactionDebtBehavior : Tool<IDesktopToolContext>, ITransactionDebt
    {
        V_HIS_TREATMENT_FEE treatment = null;
        Inventec.Desktop.Common.Modules.Module Module;

        internal TransactionDebtBehavior()
            : base()
        {

        }
        internal TransactionDebtBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, V_HIS_TREATMENT_FEE data)
            : base()
        {
            this.Module = module;
            this.treatment = data;
        }
        internal TransactionDebtBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param)
            : base()
        {
            this.Module = module;
        }

        object ITransactionDebt.Run()
        {
            object result = null;
            try
            {
                if (treatment != null)
                {
                    result = new frmTransactionDebt(Module, treatment);
                    if (result == null) throw new NullReferenceException(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => treatment), treatment));
                }
                else
                {
                    result = new frmTransactionDebt(Module);
                    if (result == null) throw new NullReferenceException(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => treatment), treatment));
                }
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
