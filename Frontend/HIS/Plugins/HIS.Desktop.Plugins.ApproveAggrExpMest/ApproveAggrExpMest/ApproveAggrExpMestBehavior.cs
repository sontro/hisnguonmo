using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ApproveAggrExpMest.ApproveAggrExpMest;
using HIS.Desktop.ADO;

namespace HIS.Desktop.Plugins.ApproveAggrExpMest.ApproveAggrExpMest
{
    class ApproveAggrExpMestBehavior : BusinessBase, IApproveAggrExpMest
    {
        object[] entity;
        internal ApproveAggrExpMestBehavior(CommonParam param, object[] filter)
            : base()
        {
            entity = filter;
        }

        object IApproveAggrExpMest.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                long expMestId = 0;
                long expMestStt = 0;
                DelegateSelectData delegateSelectData = null;
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
                            if (entity[i] is ApproveAggrExpMestSDO)
                            {
                                expMestId = ((ApproveAggrExpMestSDO)entity[i]).expMestId;
                                expMestStt = ((ApproveAggrExpMestSDO)entity[i]).expMestStt;
                            }
                            if (entity[i] is DelegateSelectData)
                            {
                                delegateSelectData = (DelegateSelectData)entity[i];
                            }
                        }
                    }
                }

                return new frmApproveAggrExpMest(expMestId, moduleData, delegateSelectData, expMestStt);
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
