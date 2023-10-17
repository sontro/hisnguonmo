using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillOther.TransactionBillOther
{
    class TransactionBillOtherBehavior : Tool<IDesktopToolContext>, ITransactionBillOther
    {
        long? treatmentId = null;
        Inventec.Desktop.Common.Modules.Module Module;
        internal TransactionBillOtherBehavior()
            : base()
        {

        }
        internal TransactionBillOtherBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param)
            : base()
        {
            this.Module = module;
        }
        internal TransactionBillOtherBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, long data)
            : base()
        {
            this.Module = module;
            this.treatmentId = data;
        }

        object ITransactionBillOther.Run()
        {
            object result = null;
            try
            {
                if (this.treatmentId.HasValue && Module != null)
                {
                    result = new frmTransactionBillOther(Module, this.treatmentId.Value);
                }
                else
                {
                    result = new frmTransactionBillOther(Module);
                }
                if (result == null) throw new NullReferenceException(Inventec.Common.Logging.LogUtil.TraceData("treatmentId", treatmentId));
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
