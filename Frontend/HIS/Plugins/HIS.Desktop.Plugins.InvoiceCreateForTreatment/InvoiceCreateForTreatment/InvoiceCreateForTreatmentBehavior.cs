using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InvoiceCreateForTreatment.InvoiceCreateForTreatment
{
    class InvoiceCreateForTreatmentBehavior : Tool<IDesktopToolContext>, IInvoiceCreateForTreatment
    {
        long treatmentId;
        Inventec.Desktop.Common.Modules.Module Module;
        internal InvoiceCreateForTreatmentBehavior()
            : base()
        {

        }

        internal InvoiceCreateForTreatmentBehavior(Inventec.Desktop.Common.Modules.Module moduleData, CommonParam param, long data)
            : base()
        {
            Module = moduleData;
            treatmentId = data;
        }

        object IInvoiceCreateForTreatment.Run()
        {
            object result = null;
            try
            {
                result = new frmInvoiceCreateForTreatment(Module, treatmentId);
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
