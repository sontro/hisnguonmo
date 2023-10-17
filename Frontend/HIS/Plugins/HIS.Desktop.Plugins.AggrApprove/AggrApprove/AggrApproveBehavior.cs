using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.AggrApprove;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.SDO;
using HIS.Desktop.Plugins.AggrApprove.AggrApprove;

namespace Inventec.Desktop.Plugins.AggrApprove.AggrApprove
{
    public sealed class AggrApproveBehavior : Tool<IDesktopToolContext>, IAggrApprove
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        long expMestId = 0;
        public AggrApproveBehavior()
            : base()
        {
        }

        public AggrApproveBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IAggrApprove.Run()
        {
            object result = null;
            DelegateSelectData delegateSelectData = null;
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
                        else if (item is long)
                        {
                            expMestId = (long)item;
                        }
                        else if (item is DelegateSelectData)
                        {
                            delegateSelectData = (DelegateSelectData)item;
                        }
                    }
                    if (currentModule != null && expMestId > 0)
                    {
                        result = new frmAggrApprove(currentModule, expMestId);
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
