using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ImportDepartment;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.Common;

namespace Inventec.Desktop.Plugins.ImportDepartment.Run
{
    public sealed class ImportDepartmentBehavior : Tool<IDesktopToolContext>, IImportDepartment
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        RefeshReference delegateRefresh;
        public ImportDepartmentBehavior()
            : base()
        {
        }

        public ImportDepartmentBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IImportDepartment.Run()
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
                        result = new frmImportDepartment(currentModule, delegateRefresh);
                    }
                    else
                    {
                        result = new frmImportDepartment(currentModule);
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
