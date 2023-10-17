using HIS.Desktop.Common;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisIcdImport
{
    public sealed class HisImportIcdBehavior : Tool<IDesktopToolContext>, IHisImportIcd
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        RefeshReference delegateRefresh;
        public HisImportIcdBehavior()
            : base()
        {
        }

        public HisImportIcdBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisImportIcd.Run()
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
                        else if (item is RefeshReference)
                        {
                            delegateRefresh = (RefeshReference)item;
                        }
                    }
                    if (currentModule != null && delegateRefresh != null)
                    {
                        result = new frmIcdImport(currentModule, delegateRefresh);
                    }
                    else
                    {
                        result = new frmIcdImport(currentModule);
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
