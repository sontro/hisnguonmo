using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ApproveAggrImpMest.ApproveAggrImpMest;
using HIS.Desktop.ADO;

namespace HIS.Desktop.Plugins.ApproveAggrImpMest.ApproveAggrImpMest
{
    class ApproveAggrImpMestBehavior : BusinessBase, IApproveAggrImpMest
    {
        object[] entity;
        internal ApproveAggrImpMestBehavior(CommonParam param, object[] filter)
            : base()
        {
            entity = filter;
        }

        object IApproveAggrImpMest.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                long impMestId = 0;
                DelegateRefreshData delegateRefreshData = null;
                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {
                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                            else if (entity[i] is long)
                            {
                                impMestId = (long)entity[i];
                            }
                            else if (entity[i] is DelegateRefreshData)
                            {
                                delegateRefreshData = (DelegateRefreshData)entity[i];
                            }
                        }
                    }
                }
                return new frmApproveAggrImpMest(impMestId, moduleData, delegateRefreshData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
