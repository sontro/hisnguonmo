using HIS.Desktop.Common;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceHeinBHYT.ServiceHeinBHYT
{
    public sealed class ServiceHeinBHYTBehavior : Tool<IDesktopToolContext>, IServiceHeinBHYT
    {
        object[] entity;
        DelegateSelectData delegateSelect;
        Inventec.Desktop.Common.Modules.Module currentModule;
        public ServiceHeinBHYTBehavior()
            : base()
        {
        }

        public ServiceHeinBHYTBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IServiceHeinBHYT.Run()
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
                        if (item is DelegateSelectData)
                        {
                            delegateSelect = (DelegateSelectData)item;
                        }

                    }
                    if (currentModule != null && delegateSelect != null)
                    {
                        result = new FormHeinServiceBHYT(currentModule, delegateSelect);

                    }
                    if (currentModule != null && delegateSelect == null)
                    {
                        result = new FormHeinServiceBHYT(currentModule);
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
