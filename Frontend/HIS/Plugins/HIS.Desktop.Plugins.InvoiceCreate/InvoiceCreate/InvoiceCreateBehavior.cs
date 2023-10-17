using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InvoiceCreate.InvoiceCreate
{
    class InvoiceCreateBehavior : Tool<IDesktopToolContext>, IInvoiceCreate
    {
        Inventec.Desktop.Common.Modules.Module Module;
        internal InvoiceCreateBehavior()
            : base()
        {

        }

        internal InvoiceCreateBehavior(Inventec.Desktop.Common.Modules.Module moduleData, CommonParam param)
            : base()
        {
            Module = moduleData;
        }

        object IInvoiceCreate.Run()
        {
            object result = null;
            try
            {
                result = new frmInvoiceCreate(Module);
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
