using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core.Tools;
using HIS.Desktop.Plugins.HisImportEmpUser.ImportEmpUser;
using  Inventec.Desktop.Core;
using HIS.Desktop.Plugins.HisImportEmpUser.ImportEmpUser.Run;
using HIS.Desktop.Plugins.HisImportEmpUser.HisImportEmpUser;

namespace HIS.Desktop.Plugins.HisImportEmpUser.ImportEmpUser
{
    public sealed class HisImportEmpUserBehavior : Tool<IDesktopToolContext> ,  IHisImportEmpUser
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        HIS.Desktop.Common.RefeshReference delegateRefresh;
        public HisImportEmpUserBehavior() : base ()
    {
    
    }
        public HisImportEmpUserBehavior(Inventec.Core.CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisImportEmpUser.Run()

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
                        else if (item is HIS.Desktop.Common.RefeshReference)
                        {
                            delegateRefresh = (HIS.Desktop.Common.RefeshReference)item;
                        }
                    }
                    if (currentModule != null && delegateRefresh != null)
                    {
                        result = new frmHisImportEmpUser(currentModule, delegateRefresh);
                    }
                    else
                    {
                        result = new frmHisImportEmpUser(currentModule);
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
