using HIS.Desktop.Plugins.InvoiceBook;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventec.Desktop.Plugins.InvoiceBook.InvoiceBook
{
    public sealed class InvoiceBookBehavior : Tool<IDesktopToolContext>, IInvoiceBook
    {
        object[] entity;
        public InvoiceBookBehavior()
            : base()
        {
        }

        public InvoiceBookBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IInvoiceBook.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module Module = null;

                for (int i = 0; i < entity.Count(); i++)
                {
                    if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                    {
                        Module = (Inventec.Desktop.Common.Modules.Module)entity[i];
                    }
                }

                return new UCInvoiceBook(Module);
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
