using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ApproveExpMestBCS;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.SDO;

namespace Inventec.Desktop.Plugins.ApproveExpMestBCS.ApproveExpMestBCS
{
    public sealed class ApproveExpMestBCSBehavior : Tool<IDesktopToolContext>, IApproveExpMestBCS
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module currentModule;
        long expMestId = 0;
        public ApproveExpMestBCSBehavior()
            : base()
        {
        }

        public ApproveExpMestBCSBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IApproveExpMestBCS.Run()
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
                        result = new frmApproveExpMestBCS(currentModule, expMestId, delegateSelectData);
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
