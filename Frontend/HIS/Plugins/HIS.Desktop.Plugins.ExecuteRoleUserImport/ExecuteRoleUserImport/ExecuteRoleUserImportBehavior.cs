using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using HIS.Desktop.Common;
using Inventec.Core;

namespace HIS.Desktop.Plugins.ExecuteRoleUserImport.ExecuteRoleUserImport
{
    class ExecuteRoleUserImportBehavior : BusinessBase, IExecuteRoleUserImport
    {
        object[] entity;
        RefeshReference delegateRefresh;
        internal ExecuteRoleUserImportBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }
        object IExecuteRoleUserImport.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module module = null;
                if (entity.GetType() == typeof(object[]))
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            module = (Inventec.Desktop.Common.Modules.Module)item;
                        else if (item is RefeshReference)
                        {
                            delegateRefresh = (RefeshReference)item;
                        }
                    }
                }
                if (module != null && delegateRefresh != null)
                {
                    result = new frmExecuteRoleUserImport(module, delegateRefresh);
                }
                if (module != null)
                {
                    result = new frmExecuteRoleUserImport(module);
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
