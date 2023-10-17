using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HIS.Desktop.Plugins.RequestForUsingAccountBook.RequestAccountBook
{
    class RequestAccountBookBehavior : Tool<DesktopToolContext>, IRequestAccountBook
    {
        object[] entity;

        public RequestAccountBookBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IRequestAccountBook.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    Inventec.Desktop.Common.Modules.Module currentModule;

                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                        {
                            currentModule = (Inventec.Desktop.Common.Modules.Module)item;
                        }
                    }

                    if (currentModule != null)
                    {
                        result = new FormRequestAccountBook(currentModule);
                    }
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
