using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HIS.Desktop.Plugins.EInvoiceCreate.EInvoiceCreate
{
    class EInvoiceCreateBehavior : Tool<IDesktopToolContext>, IEInvoiceCreate
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        public EInvoiceCreateBehavior()
            : base()
        {
        }

        public EInvoiceCreateBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IEInvoiceCreate.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                    }

                    result = new FormEInvoiceCreate(currentModule);
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
