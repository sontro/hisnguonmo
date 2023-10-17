using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.CareTypeAdd;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.SDO;
using HIS.Desktop.Plugins.CareTypeAdd;

namespace Inventec.Desktop.Plugins.CareTypeAdd.CareTypeAdd
{
    public sealed class CareTypeAddBehavior : Tool<IDesktopToolContext>, ICareTypeAdd
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        HIS.Desktop.Common.DelegateReturnSuccess refreshData;
        public CareTypeAddBehavior()
            : base()
        {
        }

        public CareTypeAddBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ICareTypeAdd.Run()
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
                        else if (item is HIS.Desktop.Common.DelegateReturnSuccess)
                        {
                            refreshData = (HIS.Desktop.Common.DelegateReturnSuccess)item;
                        }
                    }
                    if (currentModule != null)
                    {
                        result = new frmCareTypeAdd(currentModule, refreshData);
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
