using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.AggrMobaImpMests;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using MOS.EFMODEL.DataModels;

namespace Inventec.Desktop.Plugins.AggrMobaImpMests.AggrMobaImpMests
{
    public sealed class AggrMobaImpMestsBehavior : Tool<IDesktopToolContext>, IAggrMobaImpMests
    {
        object[] entity;
        public AggrMobaImpMestsBehavior()
            : base()
        {
        }

        public AggrMobaImpMestsBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IAggrMobaImpMests.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                List<V_HIS_IMP_MEST_2> lstMobaImpMestChecks = new List<V_HIS_IMP_MEST_2>();
                HIS.Desktop.Common.RefeshReference refeshData = null;
                if (entity != null && entity.Count() > 0)
                {
                    for (int i = 0; i < entity.Count(); i++)
                    {
                        if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                        {
                            moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                        }
                        else if (entity[i] is List<V_HIS_IMP_MEST_2>)
                        {
                            lstMobaImpMestChecks = (List<V_HIS_IMP_MEST_2>)entity[i];
                        }
                        else if (entity[i] is HIS.Desktop.Common.RefeshReference)
                        {
                            refeshData = (HIS.Desktop.Common.RefeshReference)entity[i];
                        }
                    }
                    if (moduleData != null && lstMobaImpMestChecks != null && refeshData != null)
                    {
                        return new frmAggrMobaImpMests(moduleData, lstMobaImpMestChecks, refeshData);
                    }
                    else
                    {
                        return null;
                        Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData("Du lieu dau vao", lstMobaImpMestChecks));
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
